using Microsoft.AspNetCore.Mvc;
using PaymentProcessorServer.Data;
using PaymentProcessorServer.Models;

namespace PaymentProcessorServer.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ConsultantsController : ControllerBase
    {
        private readonly ILogger<ConsultantsController> _logger;
        private readonly PaymentsContext _paymentsContext;

        public ConsultantsController(PaymentsContext paymentsContext, ILogger<ConsultantsController> logger)
        {
            _paymentsContext = paymentsContext;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public Consultant? GetConsultantById(Guid id)
        {
            if (_paymentsContext?.Consultants == null)
            {
                _logger.LogError("Consultants table is null");
                return null;
            }

            return _paymentsContext.Consultants.FirstOrDefault(v => v.Id.Equals(id));
        }
    }
}
