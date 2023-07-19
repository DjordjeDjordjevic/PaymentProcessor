using System;

namespace PaymentProcessorClient.Models
{
    public class Transaction
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public double Amount { get; set; }
        public bool IsPaid { get; set; }
        public Guid AccountId { get; set; }
        public Guid ConsultantId { get; set; }
        public Guid VendorId { get; set; }
        public Guid ProjectId { get; set; }

    }
}
