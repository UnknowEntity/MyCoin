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

namespace HashCompare
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        //string filePath = AppDomain.CurrentDomain.BaseDirectory + "\\UserInfo.xml";

        public MainWindow()
        {
            InitializeComponent();
            //var doc = new XDocument();

            //if (File.Exists(filePath))
            //{
            //    doc = XDocument.Load(filePath);
            //    if (doc.Element("UserInfo").Element("DateCreated").Value!="00")
            //    {
            //        txtResult.Text = "Private Key Found";
            //        WalletWindow wallet = new WalletWindow();
            //        wallet.Show();
            //        this.Close();
            //    }
            //    else
            //    {
            //        txtResult.Text = "Private Key Not Found";
            //    }
            //}
            //else
            //{
            //    doc.Add(new XDeclaration("1.0", "utf-8", "yes"));
            //    XElement root = new XElement("UserInfo");
            //    root.Add(new XElement("DateCreated", "00"));
            //    root.Add(new XElement("PrivateKey", "00"));
            //    root.Add(new XElement("PublicKey", "00"));
            //    root.Add(new XElement("ChainCode", "00"));
            //    root.Add(new XElement("Index", "00"));
            //    doc.Add(root);
            //    doc.Save(filePath);
            //}

            if(XmlManager.IsExist())
            {
                WalletWindow wallet = new WalletWindow();
                wallet.Show();
                this.Close();
            }
            else
            {
                XmlManager.NewXmlFile();
            }
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            //txtResult.Text = "GENERATE KEY PAIR";


            //var privateKey = CryptoCurrency.Hash32Byte();
            //var chainCode = CryptoCurrency.Hash32Byte();

            //string privateKeyStr = CryptoCurrency.ByteArrayToString(privateKey);
            //string chainCodeStr = CryptoCurrency.ByteArrayToString(chainCode);

            //var publicKey = CryptoCurrency.GetPublicKey(privateKey);

            //string publicKeyStr = CryptoCurrency.ByteArrayToString(publicKey);
            //string address = CryptoCurrency.Sha1(publicKey);
            //XmlManager.NewKey(privateKeyStr, publicKeyStr, address, "first");
            //XDocument document = XDocument.Load(filePath);
            //document.Element("UserInfo").Element("DateCreated").Value = DateTime.Now.ToLongDateString();
            //document.Element("UserInfo").Element("PrivateKey").Value = privateKeyStr;
            //document.Element("UserInfo").Element("PublicKey").Value = publicKeyStr;
            //document.Element("UserInfo").Element("ChainCode").Value = chainCodeStr;
            //document.Save(filePath);

            Transaction transaction = new Transaction();

            TransactionProtocol protocol = new TransactionProtocol();
            await protocol.MakeTransaction(transaction);
            txtResult.Text = protocol.Response;

            WalletWindow wallet = new WalletWindow();
            wallet.Show();
            this.Close();

        }

        
    }
}
