using Microsoft.AspNetCore.Mvc;
using myFinanceService.Services;


namespace myFinanceService.controllers{

    [ApiController]
    [Route("api/[controller]")]
    public class HealthController : ControllerBase{
        private readonly IMyHealthService _myService;

        public HealthController(IMyHealthService myService)
        {
            _myService = myService;
        }
        
        [HttpGet]
        public IActionResult Get(){

            var message = _myService.getMessage();
            return Ok(new { status = "I'm alive " + message, service = "myFinanceService" });
        }
    }
}
