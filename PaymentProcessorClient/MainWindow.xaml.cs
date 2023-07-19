using PaymentProcessorClient.Models;
using PaymentProcessorClient.PaymentProcessors;
using PaymentProcessorClient.Server;
using System.Collections.Generic;
using System.Windows;

namespace PaymentProcessorClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Transaction> Transactions { get; set; } = new List<Transaction>();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            RefreshDataGrid();
        }
        
        private async void RefreshDataGrid()
        {
            Transactions = await ServerApi.GetUnpaidTransactions();

            var itemSource = new List<TransactionsItemSource>();
            foreach (Transaction transaction in Transactions)
            {
                var account = await ServerApi.GetAccountById(transaction.AccountId);

                itemSource.Add(new TransactionsItemSource
                {
                    TransactionId = transaction.Id,
                    Consultant = transaction.ConsultantId,
                    Amount = transaction.Amount,
                    Date = transaction.Date,
                    Project = transaction.ProjectId,
                    Vendor = transaction.VendorId,
                    Account = account
                });
            }

            dgTransactions.ItemsSource = null;
            dgTransactions.ItemsSource = itemSource;
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            RefreshDataGrid();
        }

        private void MakePayment_Click(object sender, RoutedEventArgs e)
        {
            if(dgTransactions.SelectedItem is TransactionsItemSource item)
            {
                switch (item.Account?.BankName)
                {
                    case "Erste Bank":
                        new FirstPaymentProcessor().MakePayment(item.TransactionId);
                        RefreshDataGrid();
                        break;
                    case "Raiffeisen Bank":
                        new SecondPaymentProcessor().MakePayment(item.TransactionId);
                        RefreshDataGrid();
                        break;
                    case "Banca Intesa":
                        new ThirdPaymentProcessor().MakePayment(item.TransactionId);
                        RefreshDataGrid();
                        break;
                }
            }
        }
    }
}
