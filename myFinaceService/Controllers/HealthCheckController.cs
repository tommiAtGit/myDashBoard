using Microsoft.AspNetCore.Mvc;


namespace myFinanceService.controllers{

    [ApiController]
    [Route("api/[controller]")]
    public class HealthController : ControllerBase{
        [HttpGet]
        public IActionResult Get(){
            return Ok(new { status = "I'm alive", service = "myFinanceService" });
        }
    }
}
