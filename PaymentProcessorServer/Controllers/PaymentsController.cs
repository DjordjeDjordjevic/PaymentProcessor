using Microsoft.AspNetCore.Mvc;
using PaymentProcessorServer.Data;

namespace PaymentProcessorServer.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class PaymentsController : ControllerBase
    {
        private readonly ILogger<PaymentsController> _logger;
        private readonly PaymentsContext _paymentsContext;

        public PaymentsController(PaymentsContext paymentsContext, ILogger<PaymentsController> logger)
        {
            _paymentsContext = paymentsContext;
            _logger = logger;
        }

        [HttpPut("{transactionId}")]
        public IActionResult MakePayment(Guid transactionId)
        {
            if (_paymentsContext?.Transactions == null)
            {
                _logger.LogError("Transactions table is null");
                return new JsonResult(new
                {
                    Value = "Transactions table is null"
                });
            }

            var transaction = _paymentsContext.Transactions.FirstOrDefault(v => v.Id.Equals(transactionId));

            if (transaction == null)
            {
                return new JsonResult(new
                {
                    Value = "Transactions not found"
                });
            }
            
            transaction.IsPaid = true;
            
            _paymentsContext.Update(transaction);
            _paymentsContext.SaveChanges();

            return Ok();
        }
    }
}
