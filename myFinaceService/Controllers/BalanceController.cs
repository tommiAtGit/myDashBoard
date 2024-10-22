using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using myFinanceService.Domain;
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
        public ActionResult<IEnumerable<BalanceDTO>> GetAllBalances()
        {
            var balanceActions = _service.GetAllBalances();
            return Ok(balanceActions);
        }
        [HttpGet("{account}")]
        public ActionResult<BalanceDTO> GetBalance(string account)
        {
             if (!ModelState.IsValid) return BadRequest(ModelState);
            var balanceAction =  _service.GetBalance(account);
            if (balanceAction == null) return NotFound();
            return Ok(balanceAction);
        }
        
        [HttpPost]
        public ActionResult<BalanceDTO> AddBalance( [FromBody] FinanceDTO finance)
        {
             if (!ModelState.IsValid) return BadRequest(ModelState);
            var balanceAction = _service.AddNewBalance(finance);
            return CreatedAtAction(nameof(GetBalance), new { account = balanceAction.Id }, balanceAction);
        }
         [HttpPut("{account}")]
         public ActionResult<BalanceDTO> UpdateBalance(string account, [FromBody]FinanceDTO financeAction){
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