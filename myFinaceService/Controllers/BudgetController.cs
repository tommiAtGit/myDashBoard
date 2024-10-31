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
            var balanceActions = _service.GetAllBudgets;
            return Ok(balanceActions);
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
            return budget;

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
            return budget;

        }
        



    }
}