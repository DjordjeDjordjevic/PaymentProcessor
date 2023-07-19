namespace PaymentProcessorServer.Models
{
    public class Account
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Number { get; set; }
        public string BankName { get; set; } = string.Empty;
    }
}
