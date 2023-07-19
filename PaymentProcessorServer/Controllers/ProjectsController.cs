using Microsoft.AspNetCore.Mvc;
using PaymentProcessorServer.Data;
using PaymentProcessorServer.Models;

namespace PaymentProcessorServer.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ProjectsController : ControllerBase
    {
        private readonly ILogger<ProjectsController> _logger;
        private readonly PaymentsContext _paymentsContext;

        public ProjectsController(PaymentsContext paymentsContext, ILogger<ProjectsController> logger)
        {
            _paymentsContext = paymentsContext;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public Project? GetProjectById(Guid id)
        {
            if (_paymentsContext?.Projects == null)
            {
                _logger.LogError("Projects table is null");
                return null;
            }

            return _paymentsContext.Projects.FirstOrDefault(v => v.Id.Equals(id));
        }
    }
}
