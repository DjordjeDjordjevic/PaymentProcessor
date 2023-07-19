using PaymentProcessorClient.Server;
using System;

namespace PaymentProcessorClient.PaymentProcessors
{
    public class FirstPaymentProcessor : IPaymentProcessor
    {
        public async void MakePayment(Guid transactionId)
        {
            await ServerApi.MakePayments(transactionId);
        }
    }
}
