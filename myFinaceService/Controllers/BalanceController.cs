using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using myFinanceService.Domain;
using myFinanceService.Model;
using myFinanceService.Services;

namespace myFinanceService.controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class BalanceController : ControllerBase
    {

        private IBalanceService _service;
        public BalanceController(IBalanceService balanceServiceService)
        {
            _service = balanceServiceService;
        }
        [HttpGet]
        public ActionResult<IEnumerable<Balance>> GetAllBalances()
        {
            var balanceActions = _service.GetAllBalances();
            return Ok(balanceActions);
        }
        [HttpGet("{account}")]
        public ActionResult<Balance> GetBalance(string account)
        {
             if (!ModelState.IsValid) return BadRequest(ModelState);
            var balanceAction =  _service.GetBalance(account);
            if (balanceAction == null) return NotFound();
            return Ok(balanceAction);
        }
        
        [HttpPost]
        public ActionResult<Balance> AddBalance( [FromBody] Finance finance)
        {
             if (!ModelState.IsValid) return BadRequest(ModelState);
            var balanceAction = _service.AddNewBalance(finance);
            return CreatedAtAction(nameof(GetBalance), new { account = balanceAction.Id }, balanceAction);
        }
         [HttpPut("{account}")]
         public ActionResult<Balance> UpdateBalance(string account, [FromBody]Finance financeAction){
             if (!ModelState.IsValid) return BadRequest(ModelState);
             var ub = _service.UpdateBalance(account, financeAction );
             if(ub==null)
               return NotFound();
            return Ok(ub);

         }
       [HttpDelete("id")]
        public ActionResult DeleteBalance(Guid id)
        {
             if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = _service.DeleteBalance(id);
            if (!result)
                return NotFound();
            return NoContent();

        }
    }
}