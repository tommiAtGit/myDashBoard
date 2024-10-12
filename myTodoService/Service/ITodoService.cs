using myTodoService.Domain;
using System.Collections.Generic;

namespace myNotesService.Services
{
    public interface ITodoService{
        IEnumerable<TaskDTO> GetAllTasks();
        TaskDTO GetTaskById(Guid Id);

        IEnumerable<TaskDTO> GetTasksByDate(DateTime startDate, DateTime endDate);

        TaskDTO AddNewTask(TaskDTO newTask); 

        TaskDTO UpdateTask(TaskDTO task);

        bool DeleteTask(TaskDTO task);

        

        
    }
}