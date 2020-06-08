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
    /// Interaction logic for SettingWindow.xaml
    /// </summary>
    public partial class SettingWindow : Window
    {

        public SettingWindow()
        {
            InitializeComponent();
            txtHostname.Text = XmlManager.GetHttp();
            txtDefaultName.Text = "Default URL: http://localhost:3000/";
        }

        private void OnCancel(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void OnChange(object sender, RoutedEventArgs e)
        {
            XmlManager.SetHttp(txtHostname.Text);
        }

        private async void GetNodeList(object sender, RoutedEventArgs e)
        {
            TransactionProtocol protocol = new TransactionProtocol();
            string[] nodeList = await protocol.GetAllNode();

            if(nodeList[0]!="")
            {
                RadioButton originalRadioButton = new RadioButton()
                {
                    Content = XmlManager.GetHttp(),
                    IsChecked = true,
                };

                originalRadioButton.Checked += OnSelect;

                spNodeList.Children.Add(originalRadioButton);

                for (int index = 0; index < nodeList.Length; index++)
                {
                    RadioButton radioButton = new RadioButton();
                    radioButton.Content = $"{nodeList[index]}/";
                    radioButton.Checked += OnSelect;
                    spNodeList.Children.Add(radioButton);
                }
            }
            else
            {
                MessageBox.Show("Timeout", "Error");
            }

        }

        private void OnSelect(object sender, RoutedEventArgs e)
        {
            RadioButton button = sender as RadioButton;
            if (button.IsChecked==true)
            {
                txtHostname.Text = button.Content.ToString();
            }
        }

        private async void Check(object sender, RoutedEventArgs e)
        {
            TransactionProtocol protocol = new TransactionProtocol();
            bool result = await protocol.CheckServer(txtHostname.Text);
            if(result)
            {
                MessageBox.Show("Server confirm");
            }
            else
            {
                MessageBox.Show("Timeout", "Error");
            }
        }
    }
}
