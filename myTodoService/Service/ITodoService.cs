using myTodoService.Domain;
using System.Collections.Generic;

namespace myTodoService.Services
{
    public interface ITodoService
    {
        IEnumerable<TaskDTO> GetAllTasks();
        TaskDTO GetTaskById(Guid Id);

        IEnumerable<TaskDTO> GetTasksByDate(DateTime startDate, DateTime endDate);
        IEnumerable<TaskDTO> GetTasksByStatus(TodoStatus status);

        TaskDTO AddNewTask(TaskDTO newTask);

        TaskDTO UpdateTask(TaskDTO task);

        bool DeleteTask(Guid id);




    }
}