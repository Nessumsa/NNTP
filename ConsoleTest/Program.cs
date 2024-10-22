using System;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace ConsoleTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TcpClient client = new TcpClient("news.dotsrc.org", 119);
            NetworkStream networkStream = client.GetStream();
            StreamReader reader = new StreamReader(networkStream);
            StreamWriter writer = new StreamWriter(networkStream);

            byte[] serverData = new byte[1024];
            int bytesRead = networkStream.Read(serverData,0,serverData.Length);

            // Convert byte array to a string
            string response = Encoding.ASCII.GetString(serverData, 0, bytesRead);
            Console.WriteLine("Server response: \n" + response);

            string commandConnectUsername = "AUTHINFO USER ronasm01@easv365.dk\r\n";
            byte[] commandBytes = Encoding.ASCII.GetBytes(commandConnectUsername);

            networkStream.Write(commandBytes, 0, commandBytes.Length);

            Console.WriteLine("Command sent: " + commandConnectUsername.Trim());

            bytesRead = networkStream.Read(serverData, 0, serverData.Length);
            
            string response2 = Encoding.ASCII.GetString(serverData, 0, bytesRead);
            Console.WriteLine("Response from server:\n" + response2);

            string commandConnectPassword = "AUTHINFO PASS 255977\r\n";
            byte[] command2bytes = Encoding.ASCII.GetBytes(commandConnectPassword);

            networkStream.Write(command2bytes, 0, command2bytes.Length);
            Console.WriteLine("Command sent: " + commandConnectPassword.Trim());

            bytesRead = networkStream.Read(serverData, 0, serverData.Length);

            string response3 = Encoding.ASCII.GetString(serverData, 0, bytesRead);
            Console.WriteLine("Response from server:\n" + response3);


        }
    }
}
