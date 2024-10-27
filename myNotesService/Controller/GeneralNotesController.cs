using Microsoft.AspNetCore.Mvc;
using myNotesService.Services;
using myNotesService.Model;
using myNotesService.Domain;

namespace myNotesService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GeneneralNotesController : ControllerBase
    {
        private readonly IGeneralNotesService _service;
        public GeneneralNotesController(IGeneralNotesService service)
        {
            _service = service;

        }
        [HttpGet]
        public ActionResult<IEnumerable<GeneralNotes>> GetAllGeneralNotes()
        {
            var allNotes = _service.GetAllGeneralNotes();
            if (allNotes == null)
                return NotFound();

            return Ok(allNotes);

        }
        [HttpGet("{id}")]
        public ActionResult<GeneralNotes> GetGeneralNotesById(Guid id)
        {
            if (id == Guid.Empty)
                return NotFound();

            var notes = _service.GetGeneralNotesById(id);
            if (notes == null)
            {
                return NotFound();
            }
            return Ok(notes);
        }
        [HttpPost]
        public ActionResult<IEnumerable<GeneralNotes>> GetNotesByKeyWord([FromBody] List<string> keywords)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if ((keywords == null) || (keywords.Count < 1))
                return NotFound();
            var notes = _service.GetGeneralNotesByKeyWords(keywords);
            if (!notes.Any())
                return NotFound();
            return Ok(notes);


        }
        [HttpPost]
        public ActionResult<GeneralNotes> GetGeneralNotesByOwner([FromBody] string owner)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if ((owner == null) || (owner == ""))
                return NotFound();
            var notes = _service.GetGeneralNotesByOwner(owner);
            if (notes == null)
                return NotFound();
            return Ok(notes);
        }
        [HttpPost]
        public ActionResult<GeneralNotes> GetNotestByDateCreated([FromBody] DateTime dateCreated)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var notes = _service.GetGeneralNotesByDateCreated(dateCreated);
            if (notes == null)
                return NotFound();
            return Ok(notes);

        }

        [HttpPut("{id}")]
        public ActionResult<GeneralNotes> UpdateGeneralNotes(Guid id, [FromBody] GeneralNotes notes)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (notes == null)
                return NotFound();
            var updateNotes = _service.UpdateGeneralNotes(id,notes);
            if (updateNotes == null)
                return NotFound();
            return updateNotes;

        }
        [HttpDelete("{id}")]
        public IActionResult DeleteGenaralNotes(Guid id)
        {
            var result = _service.DeleteGeneralNote(id);
            if (!result) return NotFound();
            return NoContent();
        }


    }
}