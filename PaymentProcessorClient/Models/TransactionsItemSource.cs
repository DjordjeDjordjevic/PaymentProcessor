using System;

namespace PaymentProcessorClient.Models
{
    class TransactionsItemSource
    {
        public Guid TransactionId { get; set; }
        public DateTime Date { get; set; }
        public double Amount { get; set; }
        public Guid Consultant { get; set; }
        public Guid Project { get; set; }
        public Account? Account { get; set; }
        public Guid Vendor { get; set; }
    }
}
