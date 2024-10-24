using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using myFinanceService.Domain;
using myFinanceService.Services;

namespace myFinanceService.controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class FinanceTrackerController : ControllerBase
    {

        private IFinanceTrackerService _service;
        public FinanceTrackerController(IFinanceTrackerService financeTrackerService)
        {
            _service = financeTrackerService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<FinanceDTO>> GetAllTransactions()
        {
            var financeActions = _service.GetAllTransactions();
            return Ok(financeActions);
        }

        [HttpGet("{id}")]
        public ActionResult<FinanceDTO> GetTransaction(Guid Id)
        {

            var financeAction = _service.GetTransactionById(Id);
            if (financeAction == null) return NotFound();
            return Ok(financeAction);
        }

        [HttpGet]
        public ActionResult<IEnumerable<FinanceDTO>> GetTransactionByDate([FromBody] String startDate, [FromBody] String endDate)
        {

            if (!ModelState.IsValid) return BadRequest(ModelState);
            var financeAction = _service.GetTransactionsByDate(startDate, endDate);
            if ((financeAction == null) || (financeAction.Count() < 1))
                return NotFound();
            return Ok(financeAction);
        }

        [HttpPost]
        public ActionResult<FinanceDTO> AddTransaction([FromBody] FinanceDTO transAction)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var financeAction = _service.AddTransaction(transAction);
            return CreatedAtAction(nameof(GetTransaction), new { id = financeAction.Id }, financeAction);
        }

        [HttpPut("{id}")]
        public ActionResult<FinanceDTO> UpdateTransaction(Guid id, [FromBody] FinanceDTO transAction)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var fianceAction = _service.UpdateTransaction(id, transAction);
            if (fianceAction == null) return NotFound();
            return Ok(fianceAction);
        }

        [HttpDelete("Id")]
        public ActionResult DeleteTransaction(Guid Id)
        {
            var result = _service.DeleteTransaction(Id);
            if (!result)
                return NotFound();
            return NoContent();

        }
    }
}