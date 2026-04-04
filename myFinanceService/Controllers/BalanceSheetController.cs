using Microsoft.AspNetCore.Mvc;
using myFinanceService.Model;
using myFinanceService.Services;

namespace myFinanceService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BalanceSheetController : ControllerBase
    {
        private readonly IBalanceSheetService _service;

        public BalanceSheetController(IBalanceSheetService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<IEnumerable<BalanceSheet>> GetAll()
        {
            return Ok(_service.GetAllBalanceSheets());
        }

        [HttpGet("{id}")]
        public ActionResult<BalanceSheet> GetById(Guid id)
        {
            var sheet = _service.GetBalanceSheetById(id);
            if (sheet.Id == Guid.Empty) return NotFound();
            return Ok(sheet);
        }

        [HttpPost]
        public ActionResult<BalanceSheet> Create(BalanceSheet balanceSheet)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = _service.AddBalanceSheet(balanceSheet);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public ActionResult<BalanceSheet> Update(Guid id, BalanceSheet balanceSheet)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = _service.UpdateBalanceSheet(id, balanceSheet);
            if (result.Id == Guid.Empty) return NotFound();
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var result = _service.DeleteBalanceSheet(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
