using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

class Program
{
    static void ProcessMessage(object parm)
    {
        try
        {
            TcpClient client = parm as TcpClient;
            if (client == null)
                throw new InvalidOperationException("Invalid client connection.");

            // Buffer for reading data
            byte[] bytes = new byte[256];
            string data = string.Empty;

            // Get a stream object for reading and writing
            NetworkStream stream = client.GetStream();

            // Loop to receive all the data sent by the client.
            while (true)
            {
                int count = stream.Read(bytes, 0, bytes.Length);
                if (count == 0) break; // End of data.

                // Translate data bytes to an ASCII string.
                data = Encoding.ASCII.GetString(bytes, 0, count);
                Console.WriteLine($"Received: {data} at {DateTime.Now:t}");

                // Process the data sent by the client.
                data = data.ToUpper();
                byte[] msg = Encoding.ASCII.GetBytes(data);

                // Send back a response.
                stream.Write(msg, 0, msg.Length);
                Console.WriteLine($"Sent: {data}");
            }

            // Shutdown and end connection
            client.Close();
        }
        catch (SocketException se)
        {
            Console.WriteLine($"SocketException: {se.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception: {ex.Message}");
        }
        finally
        {
            Console.WriteLine("Waiting for message...");
        }
    }

    static void ExecuteServer(string host, int port)
    {
        int count = 0;
        TcpListener server = null;
        try
        {
            Console.Title = "Server Application";
            IPAddress localAddr = IPAddress.Parse(host);
            server = new TcpListener(localAddr, port);

            // Start listening for client requests.
            server.Start();
            Console.WriteLine(new string('*', 40));
            Console.WriteLine("Waiting for a connection...");

            // Enter the listening loop.
            while (true)
            {
                TcpClient client = server.AcceptTcpClient();
                Console.WriteLine($"Number of client connected: {++count}");
                Console.WriteLine(new string('*', 40));

                // Create a thread to handle client communication
                Thread thread = new Thread(new ParameterizedThreadStart(ProcessMessage));
                thread.Start(client);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception: {ex.Message}");
        }
        finally
        {
            server?.Stop();
            Console.WriteLine("Server stopped. Press any key to exit!");
        }
    }

    public static void Main()
    {
        string host = "127.0.0.1";
        int port = 13000;
        ExecuteServer(host, port);
    }
}