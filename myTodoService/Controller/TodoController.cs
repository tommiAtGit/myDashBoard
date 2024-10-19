using Microsoft.AspNetCore.Mvc;
using myTodoService.Domain;
using myTodoService.Services;


namespace myTodoService.controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class TodoController : ControllerBase
    {

        private readonly ITodoService _toDoService;

        public TodoController(ITodoService todoService)
        {
            _toDoService = todoService;
        }

        [HttpGet("{id}")]
        public ActionResult<TaskDTO> GetTaskById(Guid id)
        {
            var task = _toDoService.GetTaskById(id);
            if (task == null)
                return NotFound();
            return Ok(task);
        }
        // GET: api/tasks
        [HttpGet]
        public ActionResult<IEnumerable<TaskDTO>> GetAllTasks()
        {
            var tasks = _toDoService.GetAllTasks();
            if (tasks == null)
                return NotFound();

            return Ok(tasks);
        }

        [HttpPost]
        public ActionResult<IEnumerable<TaskDTO>> GetTasksByStatus([FromBody] TodoStatus status)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (status == TodoStatus.UNDEFINED)
                return NotFound();
            var tasks = _toDoService.GetTasksByStatus(status);
            return Ok(tasks);
        }


        // POST: api/products
        [HttpPost]
        public ActionResult<TaskDTO> AddTask([FromBody] TaskDTO task)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var createdTask = _toDoService.AddNewTask(task);
            if (createdTask == null)
                return NotFound();
            return CreatedAtAction(nameof(GetTaskById), new { id = createdTask.Id }, createdTask);
        }

        // PUT: api/products/1
        [HttpPut("{id}")]
        public ActionResult<TaskDTO> UpdateTask(Guid id, [FromBody] TaskDTO task)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var updatedTask = _toDoService.UpdateTask(task);
            if (updatedTask == null) return NotFound();
            return Ok(updatedTask);
        }

        // DELETE: api/products/1
        [HttpDelete("{id}")]
        public IActionResult DeleteTask(Guid id)
        {
            var result = _toDoService.DeleteTask(id);
            if (!result) return NotFound();
            return NoContent();
        }

    }
}