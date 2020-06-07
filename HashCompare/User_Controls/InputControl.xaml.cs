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
    /// Interaction logic for InputControl.xaml
    /// </summary>
    public partial class InputControl : UserControl
    {
        public InputControl()
        {
            InitializeComponent();
        }

        public InputControl(string address, string signature, int index)
        {
            InitializeComponent();
            txtInputAddress.Text = address;
            txtSignature.Text = signature;
            Height = 60;
            Width = 310;
            HorizontalAlignment = HorizontalAlignment.Left;
            VerticalAlignment = VerticalAlignment.Top;

            Thickness margin = this.Margin;
            margin.Left = 0;
            margin.Top = Height*index+5;
            

            this.Margin = margin;
        }
    }
}
