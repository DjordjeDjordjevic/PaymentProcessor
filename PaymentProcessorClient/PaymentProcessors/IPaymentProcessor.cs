using System;

namespace PaymentProcessorClient.PaymentProcessors
{
    interface IPaymentProcessor
    {
        void MakePayment(Guid transactionId);
    }
}
