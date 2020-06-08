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

        public OutputControl(float amount, string address, string publicKey)
        {
            InitializeComponent();

            txtInputAmount.Text = amount.ToString("0.00000");
            txtInputAddress.Text = address;
            Height = 50;
            Width = 380;
            HorizontalAlignment = HorizontalAlignment.Right;
            VerticalAlignment = VerticalAlignment.Top;
            
        }
    }
}
