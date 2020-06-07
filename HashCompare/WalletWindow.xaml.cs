using Newtonsoft.Json;
using Secp256k1Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
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
using System.Xml.Linq;

namespace HashCompare
{
    /// <summary>
    /// Interaction logic for WalletWindow.xaml
    /// </summary>
    public partial class WalletWindow : Window
    {
        List<Output> Outputs = new List<Output>();

        public WalletWindow()
        {
            InitializeComponent();

            Task.Run(async () =>
            {
                await GetTotalBalance();
                var stringCoin = $": {XmlManager.GetCurrentTotal().ToString()} KCoin";

                txtTotal.Text = stringCoin;
            });
        }

        private async void MakeTransaction(object sender, RoutedEventArgs e)
        {
            if (Outputs.Count==0)
            {
                MessageBox.Show("You don't have Output");
                return;
            }

            await GetTotalBalance();
            float spend = 0;
            float total = XmlManager.GetCurrentTotal();
            var stringCoin = $": {total.ToString()} KCoin";

            txtTotal.Text = stringCoin;

            foreach (Output output in Outputs)
            {
                spend += output.Amount;
            }

            if(total<spend)
            {
                MessageBox.Show("You don't have enough money");
                return;
            }

            Transaction transaction = new Transaction(Outputs);

            TransactionProtocol protocol = new TransactionProtocol();
            await protocol.MakeTransaction(transaction);

            Outputs.Clear();
            lvOutput.ItemsSource = null;
            lvOutput.ItemsSource = Outputs;

        }

        private async void CheckBalance(object sender, RoutedEventArgs e)
        {
            await GetTotalBalance();
            var stringCoin = $": {XmlManager.GetCurrentTotal().ToString()} KCoin";

            txtTotal.Text = stringCoin;
        }

        private async Task GetTotalBalance()
        {
            TransactionProtocol protocol = new TransactionProtocol();
            await protocol.GetCurrentTotal();
        }

        private void NewAddress(object sender, RoutedEventArgs e)
        {
            byte[] privateKey = CryptoCurrency.Hash32Byte();
            byte[] publicKey = CryptoCurrency.GetPublicKey(privateKey);
            string address = CryptoCurrency.Sha1(publicKey);

            XmlManager.NewKey(CryptoCurrency.ByteArrayToString(privateKey), CryptoCurrency.ByteArrayToString(publicKey), address, "normal");
            NewKeyWindow newKeyWindow = new NewKeyWindow(address, CryptoCurrency.ByteArrayToString(publicKey));
            newKeyWindow.Show();
        }

        private void AddAddress(object sender, RoutedEventArgs e)
        {
            string currentAddress = txtAddr.Text;
            int[] currentPublicKey = CryptoCurrency.ByteArrayToIntArray(CryptoCurrency.StringToByteArray(txtPubKey.Text));
            float currentAmount = float.Parse(txtAmount.Text);
            Outputs.Add(new Output(currentAddress, currentPublicKey, currentAmount));
            lvOutput.ItemsSource = null;
            lvOutput.ItemsSource = Outputs;

            txtAddr.Text = "";
            txtAmount.Text = "";
            txtPubKey.Text = "";
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            txtAddr.Text = Clipboard.GetText();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            txtPubKey.Text = Clipboard.GetText();
        }

        private void ClearAllAddress(object sender, RoutedEventArgs e)
        {
            Outputs.Clear();
            lvOutput.ItemsSource = null;
            lvOutput.ItemsSource = Outputs;
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            TransactionProtocol protocol = new TransactionProtocol();
            TransactionJSON[] transactionJSONs = await protocol.GetAllTransaction();

            TransactionDetailsWindow transactionDetails = new TransactionDetailsWindow(transactionJSONs);
            transactionDetails.Show();
        }
    }
}
