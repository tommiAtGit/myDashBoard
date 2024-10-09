using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using myFinanceService.domain;
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
        public ActionResult<FinanceDTO> GetTransaction(String Id)
        {

            var financeAction = _service.GetTransactionById(Id);
            return Ok(financeAction);
        }
        [HttpGet]
        public ActionResult<IEnumerable<FinanceDTO>> GetTransactionByDate(String startDate, String endDate)
        {

            var financeAction = _service.getTransactionsByDate(startDate, endDate);
            return Ok(financeAction);
        }
        [HttpPost]
        public ActionResult<FinanceDTO> AddTransaction(FinanceDTO transAction)
        {
            var financeAction = _service.addTransaction(transAction);
            return Ok(financeAction);
        }
        [HttpDelete]
        public ActionResult deleteTransaction(String Id)
        {
            var result = _service.deleteTransaction(Id);
            if (!result)
                return NotFound();
            return NoContent();

        }
    }
}