using Microsoft.VisualBasic;
using System.Net;
using static System.Console;
class Program
{
    static void Main(string[] args)
    {
        WebRequest request = WebRequest.Create("https://translate.google.com/?hl=vi&sl=en&tl=vi&text=supply&op=translate");
        request.Credentials = CredentialCache.DefaultCredentials;
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        Console.WriteLine("Status: " + response.StatusDescription);
        Console.WriteLine(new string('*', 50));
        Stream datastream = response.GetResponseStream();
        StreamReader reader = new StreamReader(datastream);
        string responseFromServer = reader.ReadToEnd();
        Console.WriteLine(responseFromServer);
        Console.WriteLine(new string('*', 50));
        reader.Close();
        datastream.Close();
        response.Close();
    }
}