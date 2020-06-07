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
    /// Interaction logic for TransactionControl.xaml
    /// </summary>
    public partial class TransactionControl : UserControl
    {
        public TransactionControl()
        {
            InitializeComponent();
        }

        public TransactionControl(TransactionJSON transactionJSON,double currentHeight)
        {
            InitializeComponent();

            Width = 700;

            double esInHeight = 0;
            double esOuHeight = 0;
            txtTransactionId.Text = transactionJSON.Id;
            txtTimestamp.Text = transactionJSON.Timestamp.ToString();
            txtType.Text = transactionJSON.Type;
            VerticalAlignment = VerticalAlignment.Top;
            HorizontalAlignment = HorizontalAlignment.Center;

            if (transactionJSON.Inputs!= null)
            {
                for(int index = 0; index<transactionJSON.Inputs.Count();index++)
                {
                    InputJSON input = transactionJSON.Inputs[index];
                    InputControl inputControl = new InputControl(input.Address, input.Signature, index);
                    esInHeight += inputControl.Height;
                    gContent.Children.Add(inputControl);
                }
            }

            for (int index = 0; index < transactionJSON.Outputs.Count(); index++)
            {
                OutputJSON output = transactionJSON.Outputs[index];
                OutputControl outputControl = new OutputControl(output.Amount, output.Address, output.PublicKey, index);
                esOuHeight += outputControl.Height;
                gContent.Children.Add(outputControl);
            }

            double totalHeight = esInHeight > esOuHeight ? esInHeight : esOuHeight;

            Height = totalHeight + 70;
            Thickness margin = this.Margin;
            margin.Top = currentHeight + 5;


            this.Margin = margin;
        }
    }
}
