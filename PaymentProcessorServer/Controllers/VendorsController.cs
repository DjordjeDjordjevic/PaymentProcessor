using Microsoft.AspNetCore.Mvc;
using PaymentProcessorServer.Data;
using PaymentProcessorServer.Models;

namespace PaymentProcessorServer.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class VendorsController : ControllerBase
    {
        private readonly ILogger<VendorsController> _logger;
        private readonly PaymentsContext _paymentsContext;

        public VendorsController(PaymentsContext paymentsContext, ILogger<VendorsController> logger)
        {
            _paymentsContext = paymentsContext;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public Vendor? GetVendorById(Guid id)
        {
            if (_paymentsContext?.Vendors == null)
            {
                _logger.LogError("Vendors table is null");
                return null;
            }

            return _paymentsContext.Vendors.FirstOrDefault(v => v.Id.Equals(id));
        }
    }
}
