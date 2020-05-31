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

namespace HashCompare
{
    /// <summary>
    /// Interaction logic for NewKeyWindow.xaml
    /// </summary>
    public partial class NewKeyWindow : Window
    {
        public NewKeyWindow()
        {
            InitializeComponent();
        }

        public NewKeyWindow(string address, string publicKey)
        {
            InitializeComponent();
            txtAddress.Text = address;
            txtPublicKey.Text = publicKey;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(txtAddress.Text);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(txtPublicKey.Text);
        }
    }
}
