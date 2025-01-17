using System;
using System.Net.Sockets;
using System.Text;

class Program
{
    static void ConnectServer(string server, int port)
    {
        string message, responseData;
        int bytes;
        try
        {
            // Create a TcpClient
            TcpClient client = new TcpClient(server, port);
            Console.Title = "Client Application";
            NetworkStream stream = client.GetStream();

            while (true)
            {
                Console.Write("Input message <press Enter to exit>: ");
                message = Console.ReadLine();
                if (string.IsNullOrEmpty(message))
                {
                    break;
                }

                // Translate the passed message into ASCII and store it as a byte array.
                byte[] data = Encoding.ASCII.GetBytes(message);

                // Send the message to the connected TcpServer.
                stream.Write(data, 0, data.Length);
                Console.WriteLine("Sent: {0}", message);

                // Receive the TcpServer response.
                // Use buffer to store the response bytes.
                data = new byte[256];
                bytes = stream.Read(data, 0, data.Length);
                responseData = Encoding.ASCII.GetString(data, 0, bytes);
                Console.WriteLine("Received: {0}", responseData);
            }

            // Close the network stream and TcpClient.
            stream.Close();
            client.Close();
        }
        catch (SocketException se)
        {
            Console.WriteLine("SocketException: {0}", se.Message);
        }
        catch (Exception e)
        {
            Console.WriteLine("Exception: {0}", e.Message);
        }
    }

    static void Main(string[] args)
    {
        string server = "127.0.0.1";
        int port = 13000;
        ConnectServer(server, port);
    }
}