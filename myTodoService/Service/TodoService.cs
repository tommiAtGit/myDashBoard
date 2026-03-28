using AutoMapper;
using myTodoService.Domain;
using myTodoService.Model;
using myTodoService.Repository;



namespace myTodoService.Services
{
    public class TodoService : ITodoService
    {
        private TaskRepositoryMoc _repository;
        private readonly ILogger<TodoService> _logger;
        private IMapper _mapper;

        public TodoService(IMapper mapper,ILogger<TodoService> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(_logger));
            _repository = new();
            _mapper = mapper;
        }
        public MyTask AddNewTask(MyTask newTask)
        {

            if (newTask != null)
            {
                if (newTask.Status == TodoStatus.OPEN)
                {
                    newTask.DateReported = DateTime.Now;
                }
                var task = _repository.AddNewTask(_mapper.Map<MyTaskDTO>(newTask));
                
                return _mapper.Map<MyTask>(task);
            }
            else
                throw new ArgumentNullException(nameof(newTask), "No task defined");
        }

        public bool DeleteTask(Guid id)
        {
             _logger.LogInformation($"Delete task called with Id: {id}");
            if (id == Guid.Empty)
                throw new ArgumentNullException(nameof(id), "Id cannot be empty");

            MyTaskDTO task = _repository.GetTaskById(id);
            if (task != null)
            {
                _logger.LogInformation($"Task found with Id: {id}");
                var result = _repository.DeleteTask(task);
                return result;
            }
            else
            {
                _logger.LogWarning($"Task NOT found with Id: {id}");
                return false;
            }
        }

        public IEnumerable<MyTask> GetAllTasks()
        {
            return _mapper.Map<IEnumerable<MyTask>>(_repository.GetAllTasks());
        }

        public MyTask GetTaskById(Guid Id)
        {
            return _mapper.Map<MyTask>(_repository.GetTaskById(Id));
        }

        public IEnumerable<MyTask> GetTasksByDate(DateTime startDate, DateTime endDate)
        {
            return _mapper.Map<IEnumerable<MyTask>>(_repository.GetTasksByDate(startDate, endDate));
        }
        public IEnumerable<MyTask> GetTasksByStatus(TodoStatus status)
        {
            
            return _mapper.Map<IEnumerable<MyTask>>(_repository.GetTasksByStatus(status));
        }

        public MyTask UpdateTask(MyTask task)
        {
            MyTask t = new();
            t = GetTaskById(task.Id);

            if (task.Status != t.Status)
            {
                if ((task.Status == TodoStatus.PROGRESS) && (t.Status == TodoStatus.OPEN))
                    task.DateOpened = DateTime.Now;
                else if ((task.Status == TodoStatus.DONE) && (t.Status == TodoStatus.PROGRESS))
                    task.DataCompleted = DateTime.Now;
                else if ((task.Status == TodoStatus.OPEN) && (t.Status == TodoStatus.DONE))
                    task.DataCompleted = DateTime.Now;
                else if ((task.Status == TodoStatus.CLOSED) && (t.Status == TodoStatus.DONE))
                    task.DateClosed = DateTime.Now;
                else
                    throw new ArgumentException(nameof(task.Status), "Dad status change");

            }

            return _mapper.Map<MyTask>(_repository.UpdateTask(_mapper.Map<MyTaskDTO>(task)));
        }

    }
}