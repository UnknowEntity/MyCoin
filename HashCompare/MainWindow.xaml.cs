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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Security.Cryptography;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Xml.Linq;
using System.IO;
using Secp256k1Net;
using System.Threading;

namespace HashCompare
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

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (XmlManager.IsExist())
            {
                List<string> adviceList = new List<string>()
                {
                    "You should remove spend address so that the program may run faster",
                    "You should only use each address once",
                    "Your private key is in the UserInfo.xml file"
                };

                Random r = new Random();
                int rInt = r.Next(0, 3); //for ints

                txtResult.Text = adviceList[rInt];

                await Task.Delay(2000);

                WalletWindow wallet = new WalletWindow();
                wallet.Show();
                this.Close();
            }
            else
            {
                XmlManager.NewXmlFile();
                txtResult.Text = "Welcome new user!\n Currently generate your first address!\nPlease don't close window";
                Transaction transaction = new Transaction();

                TransactionProtocol protocol = new TransactionProtocol();
                TransactionResponse result = await protocol.MakeTransaction(transaction);
                

                if (result.Status == "invalid")
                {
                    txtResult.Text = "Cannot send key to host";
                }
                else
                {
                    txtResult.Text = "You create your first transaction";
                }

                await Task.Delay(2000);

                WalletWindow wallet = new WalletWindow();
                wallet.Show();
                this.Close();
            }
        }
    }
}
