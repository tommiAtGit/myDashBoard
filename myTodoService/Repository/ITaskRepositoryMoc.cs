using myTodoService.Domain;



namespace myTodoService.Repository
{
    public interface ITaskRepositoryMoc
    {
        public MyTaskDTO AddNewTask(MyTaskDTO newTask);
        public IEnumerable<MyTaskDTO> GetAllTasks();
        public MyTaskDTO GetTaskById(Guid Id);
        public IEnumerable<MyTaskDTO> GetTasksByStatus(TodoStatus status);

        public IEnumerable<MyTaskDTO> GetTasksByDate(DateTime startDate, DateTime EndDate);
        public MyTaskDTO UpdateTask(MyTaskDTO task);
        public bool DeleteTask(Guid Id);
    }
}