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

        public InputControl(string address, string signature)
        {
            InitializeComponent();
            txtInputAddress.Text = address;
            Height = 30;
            Width = 260;
            HorizontalAlignment = HorizontalAlignment.Left;
            VerticalAlignment = VerticalAlignment.Top;
            
        }
    }
}
