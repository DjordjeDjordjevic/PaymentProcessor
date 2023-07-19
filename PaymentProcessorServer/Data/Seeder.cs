using AutoFixture;
using PaymentProcessorServer.Models;

namespace PaymentProcessorServer.Data
{
    public static class Seeder
    {
        public static void Seed(this PaymentsContext paymentContext)
        {
            if(paymentContext.Transactions != null && !paymentContext.Transactions.Any())
            {
                var random = new Random();
                Fixture fixture = new();

                var accountName = new List<string> { "Erste Bank", "Raiffeisen Bank", "Banca Intesa" };

                string randomBankName() => accountName[random.Next(0, accountName.Count)];

                List<Consultant> consultants = fixture.CreateMany<Consultant>(5).ToList();
                List<Vendor> vendors = fixture.CreateMany<Vendor>(5).ToList();
                List<Project> projects = fixture.CreateMany<Project>(10).ToList();

                fixture.Customize<Account>(a => a.With(x => x.BankName, randomBankName));
                List <Account> accounts = fixture.CreateMany<Account>(5).ToList();

                Guid randomConsultantsId() => consultants[random.Next(0, consultants.Count)].Id;
                Guid randomVendorsId() => vendors[random.Next(0, vendors.Count)].Id;
                Guid randomProjectsId() => projects[random.Next(0, projects.Count)].Id;
                Guid randomAccountsId() => accounts[random.Next(0, accounts.Count)].Id;

                fixture.Customize<Transaction>(t => t.With(cons=> cons.ConsultantId, randomConsultantsId)
                                                    .With(vend => vend.VendorId, randomVendorsId)
                                                    .With(proj => proj.ProjectId, randomProjectsId)
                                                    .With(proj => proj.AccountId, randomAccountsId));

                List<Transaction> transactions = fixture.CreateMany<Transaction>(50).ToList();

                paymentContext.AddRange(transactions);
                paymentContext.AddRange(vendors);
                paymentContext.AddRange(projects);
                paymentContext.AddRange(accounts);
                paymentContext.SaveChanges();
            }
        }
    }
}
