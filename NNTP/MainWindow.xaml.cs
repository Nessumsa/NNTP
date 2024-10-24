using System;
using System.IO;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NNTP
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private TcpClient client;
        private NetworkStream networkStream;
        private StreamWriter writer;
        private StreamReader reader;
        public static String serverName { get; set; }
        public static String portNumber { get; set; }
        public static String username { get; set; }
        public static String password { get; set; }

        
        public void Connect_Button_Click(object sender, RoutedEventArgs e)
        {
            client = new TcpClient(serverName, int.Parse(portNumber));
            networkStream = client.GetStream();
            writer = new StreamWriter(networkStream);
            reader = new StreamReader(networkStream);

            byte[] serverData = new byte[1024];
            int bytesRead = networkStream.Read(serverData, 0, serverData.Length);

            string commandConnectUsername = $"AUTHINFO USER {username}\r\n";
            byte[] commandBytes = Encoding.ASCII.GetBytes(commandConnectUsername);

            networkStream.Write(commandBytes, 0, commandBytes.Length);

            bytesRead = networkStream.Read(serverData, 0, serverData.Length);

            string commandConnectPassword = $"AUTHINFO PASS {password}\r\n";
            commandBytes = Encoding.ASCII.GetBytes(commandConnectPassword);

            networkStream.Write(commandBytes, 0, commandBytes.Length);
            
            bytesRead = networkStream.Read(serverData, 0, serverData.Length);

            string response = Encoding.ASCII.GetString(serverData, 0, bytesRead);

            lblContent.Content = response;

            //GetListView(commandBytes, bytesRead, serverData);

        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Setup_Click(object sender, RoutedEventArgs e)
        {
            SetupWindow setupWindow = new SetupWindow();
            setupWindow.Show();
        }
        private void Open_Button_Click(object sender, RoutedEventArgs e)
        {
            serverName = "news.dotsrc.org";
            portNumber = "119";
            username = "ronasm01@easv365.dk";
            password = "255977";
        }

        private async void GetListView_Button_Click(object sender, RoutedEventArgs e)
        {
            byte[] serverData = new byte[262144];
            byte[] commandBytes;
            int bytesRead;
            
            string command = "LIST\r\n";
            commandBytes = Encoding.ASCII.GetBytes(command);
            networkStream.WriteAsync(commandBytes, 0, commandBytes.Length);

            string fullResponse = "";
            do
            {
                bytesRead = await networkStream.ReadAsync(serverData, 0, serverData.Length);
                string response = Encoding.ASCII.GetString(serverData, 0, bytesRead);
                fullResponse += response;

                if (response.Contains("\r\n.\r\n") || response.Contains("\n.\n"))
                {
                    break;
                }
            } while (bytesRead > 0);

            string[] fullResponseInLines = fullResponse.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string singleResponse in fullResponseInLines)
            {
                ListView.Items.Add(singleResponse);
            }
        }
    }
}