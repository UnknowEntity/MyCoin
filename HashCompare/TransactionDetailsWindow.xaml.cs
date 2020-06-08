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
using HashCompare.User_Controls;

namespace HashCompare
{
    /// <summary>
    /// Interaction logic for TransactionDetailsWindow.xaml
    /// </summary>
    public partial class TransactionDetailsWindow : Window
    {
        public TransactionDetailsWindow()
        {
            InitializeComponent();
        }

        public TransactionDetailsWindow(TransactionJSON[] transactionJSONs)
        {
            InitializeComponent();

            foreach(TransactionJSON transactionJSON in transactionJSONs)
            {
                TransactionControl transactionControl = new TransactionControl(transactionJSON);
                gTransactionList.Children.Add(transactionControl);
            }
        }
    }
}
