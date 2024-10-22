using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace NNTP
{
    /// <summary>
    /// Interaction logic for SetupWindow.xaml
    /// </summary>
    public partial class SetupWindow : Window
    {
        public SetupWindow()
        {
            InitializeComponent();

        }

        private void Save_Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.serverName = TbServerName.Text;
            MainWindow.portNumber = TbPortNumber.Text;
            MainWindow.username = TbUsername.Text;
            MainWindow.password = TbPassword.Text;
            Close();
        }
    }
}
