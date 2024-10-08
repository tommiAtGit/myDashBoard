using Microsoft.AspNetCore.Mvc;
using myNotesService.Services;

namespace myNotesService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HealthController : ControllerBase
    {
        private readonly IHealthService _myService;

        public HealthController(IHealthService myService)
        {
            _myService = myService;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var message = _myService.getMessage();
            return Ok(new { status = "Healthy", service = "Microservice 1" });
        }
    }
}