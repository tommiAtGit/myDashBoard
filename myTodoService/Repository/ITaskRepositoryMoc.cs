using myTodoService.Domain;



namespace myNotesService.Repository
{
    public interface ITaskReposioryMoc
    {
        public TaskDTO AddNewTask(TaskDTO newTask);
        public IEnumerable<TaskDTO> getAllTasks();
        public TaskDTO GetTaskById(Guid Id);
        public IEnumerable<TaskDTO> GetTasksByStatus(TodoStatus status);

        public IEnumerable<TaskDTO> GetTasksByDate(DateTime startDate, DateTime EndDate);
        public TaskDTO UpdateTask(TaskDTO task);
        public bool DeleteTask(Guid Id);
    }
}