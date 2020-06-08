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

        public TransactionControl(TransactionJSON transactionJSON)
        {
            InitializeComponent();

            Width = 700;
            
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
                    InputControl inputControl = new InputControl(input.Address, input.Signature);
                    spInputs.Children.Add(inputControl);
                }
            }

            for (int index = 0; index < transactionJSON.Outputs.Count(); index++)
            {
                OutputJSON output = transactionJSON.Outputs[index];
                OutputControl outputControl = new OutputControl(output.Amount, output.Address, output.PublicKey);
                spOutputs.Children.Add(outputControl);
            }
            
            

            
        }
    }
}
