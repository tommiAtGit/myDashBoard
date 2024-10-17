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
        [HttpGet]
        public ActionResult<FinanceDTO> GetTransaction(Guid Id)
        {

            var financeAction = _service.GetTransactionById(Id);
            return Ok(financeAction);
        }
        [HttpGet]
        public ActionResult<IEnumerable<FinanceDTO>> GetTransactionByDate(String startDate, String endDate)
        {

            var financeAction = _service.GetTransactionsByDate(startDate, endDate);
            return Ok(financeAction);
        }
        [HttpPost]
        public ActionResult<FinanceDTO> AddTransaction(FinanceDTO transAction)
        {
            var financeAction = _service.AddTransaction(transAction);
            return Ok(financeAction);
        }
        [HttpDelete]
        public ActionResult DeleteTransaction(Guid Id)
        {
            var result = _service.DeleteTransaction(Id);
            if (!result)
                return NotFound();
            return NoContent();

        }
    }
}