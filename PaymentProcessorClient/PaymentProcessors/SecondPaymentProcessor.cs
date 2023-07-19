using PaymentProcessorClient.Server;
using System;

namespace PaymentProcessorClient.PaymentProcessors
{
    public class SecondPaymentProcessor : IPaymentProcessor
    {
        public async void MakePayment(Guid transactionId)
        {
            await ServerApi.MakePayments(transactionId);
        }
    }
}
