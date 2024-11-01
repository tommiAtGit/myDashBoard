
using myFinanceService.Model;
using Microsoft.AspNetCore.Mvc;
using myFinanceService.Services;


namespace myFinanceService.controllers{
     [ApiController]
    [Route("api/[controller]")]
    public class BudgetController : ControllerBase
    {

        private IBudgetService _service;
        public BudgetController(IBudgetService budgetService)
        {
            _service = budgetService;
        }
        [HttpGet]
        public ActionResult<IEnumerable<Budget>> GetAllBudgets()
        {
            var budgetActions = _service.GetAllBudgets();
            return Ok(budgetActions);
        }
        [HttpGet("{id}")]
        public ActionResult<Budget>GetBudgetById(Guid id){
             if (!ModelState.IsValid) return BadRequest(ModelState);
             if(id == Guid.Empty){
                return BadRequest(ModelState);
             }
            var budget =  _service.GetBudgetById(id);
            if(budget == null)
                return NotFound();
            return Ok(budget);

        }

        [HttpGet("account/{account}")]
        public ActionResult<Budget>GetBudgetByAccount(string account){
             if (!ModelState.IsValid) return BadRequest(ModelState);
             if((account== null) || (account=="")){
                return BadRequest(ModelState);
             }
            var budget =  _service.GetBudgetByAccount(account);
            if(budget == null)
                return NotFound();
            return Ok(budget);

        }
        [HttpPost]
        public ActionResult<Budget>AddBudget([FromBody] Budget budget){
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if(budget == null)
                return BadRequest(ModelState);

            var newB = _service.AddBudget(budget);
             return CreatedAtAction(nameof(AddBudget), new { account = newB.BudgetAccount }, newB);

        }

        [HttpPut("id")]
        public ActionResult<Budget>UpdateBudget(Guid id, [FromBody] Budget budget){

            if (!ModelState.IsValid) return BadRequest(ModelState);
            if(id == Guid.Empty){
                return BadRequest(ModelState);
             }

            var b = _service.UpdateAccountBudget(id, budget);
            if(b == null)
                return NotFound();
            return Ok(b);
        }

        [HttpDelete("id")]
        public ActionResult DeleteBudget(Guid id){
             if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = _service.DeleteBudget(id);
            if (!result)
                return NotFound();
            return NoContent();
        }
        



    }
}