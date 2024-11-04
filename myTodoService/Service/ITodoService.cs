using myTodoService.Domain;
using myTodoService.Model;

using System.Collections.Generic;

namespace myTodoService.Services
{
    public interface ITodoService
    {
        IEnumerable<MyTask> GetAllTasks();
        MyTask GetTaskById(Guid Id);

        IEnumerable<MyTask> GetTasksByDate(DateTime startDate, DateTime endDate);
        IEnumerable<MyTask> GetTasksByStatus(TodoStatus status);

        MyTask AddNewTask(MyTask newTask);

        MyTask UpdateTask(MyTask task);

        bool DeleteTask(Guid id);




    }
}