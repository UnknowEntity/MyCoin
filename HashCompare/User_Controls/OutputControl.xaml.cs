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

namespace HashCompare.User_Controls
{
    /// <summary>
    /// Interaction logic for OutputControl.xaml
    /// </summary>
    public partial class OutputControl : UserControl
    {
        public OutputControl()
        {
            InitializeComponent();
        }

        public OutputControl(float amount, string address, string publicKey,int index)
        {
            InitializeComponent();

            txtInputAmount.Text = amount.ToString("0.00000");
            txtInputAddress.Text = address;
            txtPublicKey.Text = publicKey;
            Height = 80;
            Width = 310;
            HorizontalAlignment = HorizontalAlignment.Right;
            VerticalAlignment = VerticalAlignment.Top;

            Thickness margin = this.Margin;
            margin.Right = 0;
            margin.Top = Height * index + 5;


            this.Margin = margin;
        }
    }
}
