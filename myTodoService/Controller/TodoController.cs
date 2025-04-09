using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using myTodoService.Domain;
using myTodoService.Model;
using myTodoService.Services;


namespace myTodoService.controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class TodoController : ControllerBase
    {

        private readonly ITodoService _toDoService;
        private readonly ILogger<TodoController> _logger;


        public TodoController(ITodoService todoService,ILogger<TodoController> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(_logger));
            if (todoService == null)
                throw new ArgumentNullException(nameof(todoService));
            _toDoService = todoService;
        }

        // GET: api/todo/1
        [HttpGet("{id}")]
        public ActionResult<MyTask> GetTaskById(Guid id)
        {
            _logger.LogInformation($"Endpoint GetTaskById called with Id: {id}");
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (id == Guid.Empty)
                return BadRequest("Id cannot be empty");
            if (!Guid.TryParse(id.ToString(), out Guid parsedId))
                return BadRequest("Id is not a valid GUID");
            var task = _toDoService.GetTaskById(parsedId);
            if (task == null)
                return NotFound();
            _logger.LogInformation($"Task found with Id: {id}");
            return Ok(task);
        }

        // GET: api/todo
        [HttpGet]
        public ActionResult<IEnumerable<MyTask>> GetAllTasks()
        {
            var tasks = _toDoService.GetAllTasks();
            if (tasks == null)
                return NotFound();

            return Ok(tasks);
        }

        // GET: api/todo/taskByStatus
        [HttpGet("taskByStatus/{taskStatus}")]
        public ActionResult<IEnumerable<MyTask>> GetTasksByStatus(int taskStatus)
        {
            _logger.LogInformation($"Endpoint GetTasksByStatus called with Status: {taskStatus}");
            if (taskStatus < 1 || taskStatus > 3)
                return BadRequest("Invalid status value. Must be between 0 and 2.");
            if (!Enum.IsDefined(typeof(TodoStatus), taskStatus))
                return BadRequest("Invalid status value. Must be between 0 and 2.");
            if (taskStatus == (int)TodoStatus.UNDEFINED)
                return NotFound("Status not found");
            if (!ModelState.IsValid) return BadRequest(ModelState);

            TodoStatus status = (TodoStatus)taskStatus;
            if (status == TodoStatus.UNDEFINED)
                return NotFound();
            var tasks = _toDoService.GetTasksByStatus(status);
            if (tasks == null)
                return NotFound();
            if (tasks.Count() == 0)
                return NotFound("No tasks found with the specified status.");
            _logger.LogInformation($"Tasks found with Status: {taskStatus}");
            return Ok(tasks);
        }


        // POST: api/todo
        [HttpPost("AddTask")]
        public ActionResult<MyTask> AddTask([FromBody] MyTask task)
        {
            _logger.LogInformation("Endpoint AddTask called with Task:" + task);
            if (task == null)
                return BadRequest("Task cannot be null");
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var createdTask = _toDoService.AddNewTask(task);
            if (createdTask == null)
                return NotFound();
            _logger.LogInformation($"Task created with Id: {createdTask.Id}");
            return CreatedAtAction(nameof(GetTaskById), new { id = createdTask.Id }, createdTask);
        }

        // PUT: api/products/1
        [HttpPut("update/{id}")]
        public ActionResult<MyTask> UpdateTask(Guid id, [FromBody] MyTask task)
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
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (id == Guid.Empty)
                return BadRequest("Id cannot be empty");
            if (!Guid.TryParse(id.ToString(), out Guid parsedId))
                return BadRequest("Id is not a valid GUID");
        
             _logger.LogInformation($"Delete task with Id: {id}");
            var result = _toDoService.DeleteTask(id);
            if (!result) return NotFound();
            _logger.LogInformation($"Task with Id: {id} deleted successfully");
            return NoContent();
        }

    }
}