using Microsoft.AspNetCore.Mvc;
using PaymentProcessorServer.Data;
using PaymentProcessorServer.Models;

namespace PaymentProcessorServer.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class AccountsController : ControllerBase
    {
        private readonly ILogger<AccountsController> _logger;
        private readonly PaymentsContext _paymentsContext;

        public AccountsController(PaymentsContext paymentsContext, ILogger<AccountsController> logger)
        {
            _paymentsContext = paymentsContext;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public Account? GetAccountById(Guid id)
        {
            if (_paymentsContext?.Account == null)
            {
                _logger.LogError("Account table is null");
                return null;
            }

            return _paymentsContext.Account.FirstOrDefault(v => v.Id.Equals(id));
        }
    }
}
