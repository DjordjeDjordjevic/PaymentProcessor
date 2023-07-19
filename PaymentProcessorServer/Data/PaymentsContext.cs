using Microsoft.EntityFrameworkCore;
using PaymentProcessorServer.Models;

namespace PaymentProcessorServer.Data
{
    public class PaymentsContext : DbContext
    {
        public PaymentsContext(DbContextOptions<PaymentsContext> options) : base(options) { }

        public DbSet<Transaction>? Transactions { get; set; }
        public DbSet<Vendor>? Vendors { get; set; }
        public DbSet<Consultant>? Consultants { get; set; }
        public DbSet<Project>? Projects { get; set; }
        public DbSet<Account>? Account { get; set; }
    }
}
