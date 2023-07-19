using Microsoft.AspNetCore.Mvc;
using PaymentProcessorServer.Data;
using PaymentProcessorServer.Models;

namespace PaymentProcessorServer.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class TransactionsController : ControllerBase
    {
        private readonly ILogger<TransactionsController> _logger;
        private readonly PaymentsContext _paymentsContext;

        public TransactionsController(PaymentsContext paymentsContext, ILogger<TransactionsController> logger)
        {
            _paymentsContext = paymentsContext;
            _logger = logger;
        }

        [HttpGet()]
        public IEnumerable<Transaction> GetAllUnpaidTransactions()
        {
            if (_paymentsContext?.Transactions == null)
            {
                _logger.LogError("Transactions table is null");
                return Enumerable.Empty<Transaction>();
            }

            return _paymentsContext.Transactions.Where(t => !t.IsPaid).OrderBy(t => t.Date);
        }

        [HttpGet("{fromDate}/{untilDate}")]
        public IEnumerable<object> GetAmountPaidPerVendor(DateTime fromDate, DateTime untilDate)
        {
            if (_paymentsContext?.Transactions == null)
            {
                _logger.LogError("Transactions table is null");
                return Enumerable.Empty<object>();
            }

            if (_paymentsContext?.Vendors == null)
            {
                _logger.LogError("Vendors table is null");
                return Enumerable.Empty<object>();
            }

            return from trans in _paymentsContext.Transactions
                         join vendor in _paymentsContext.Vendors
                            on trans.VendorId equals vendor.Id
                         group vendor by new { trans.VendorId, vendor.Name, trans.Amount, trans.Date } into g
                         where g.Key.Date.Ticks > fromDate.Ticks && g.Key.Date.Ticks < untilDate.Ticks 
                         select new
                         {
                             g.Key.VendorId,
                             g.Key.Name,
                             Amount = g.Sum(a => g.Key.Amount)
                         };
        }
    }
}
