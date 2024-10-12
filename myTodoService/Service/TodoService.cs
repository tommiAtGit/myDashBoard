using myTodoService.Domain;
using myNotesService.Repository;

using System.Collections.Generic;

namespace myNotesService.Services
{
    public class TodoService : ITodoService
    {
        private TaskRepositoryMoc _repository;
        public TodoService()
        {
            _repository = new();
        }
        public TaskDTO AddNewTask(TaskDTO newTask)
        {

            if (newTask != null)
            {
                if (newTask.Status == TodoStatus.OPEN)
                {
                    newTask.DateReported = DateTime.Now;
                }
                var task = _repository.AddNewTask(newTask);
                return task;
            }
            else
                throw new ArgumentNullException(nameof(newTask), "No task defined");
        }

        public bool DeleteTask(TaskDTO task)
        {
            if (task != null)
            {
                var result = _repository.DeleteTask(task.Id);
                return result;
            }
            else
                throw new ArgumentNullException(nameof(task), "No task defined");
        }

        public IEnumerable<TaskDTO> GetAllTasks()
        {
            return _repository.getAllTasks();
        }

        public TaskDTO GetTaskById(Guid Id)
        {
            return _repository.GetTaskById(Id);
        }

        public IEnumerable<TaskDTO> GetTasksByDate(DateTime startDate, DateTime endDate)
        {
            return _repository.GetTasksByDate(startDate, endDate);
        }
        public IEnumerable<TaskDTO> GetTaskByStatus(TodoStatus status)
        {
            return _repository.GetTasksByStatus(status);
        }
        public TaskDTO UpdateTask(TaskDTO task)
        {
            var t = GetTaskById(task.Id);
            if ((task.Status == TodoStatus.PROGRESS) && (t.Status == TodoStatus.OPEN))
                task.DateOpened = DateTime.Now;
            else if ((task.Status == TodoStatus.DONE) && (t.Status == TodoStatus.PROGRESS))
                task.DataCompleted = DateTime.Now;
            else if ((task.Status == TodoStatus.CLOSED) && (t.Status == TodoStatus.DONE))
                task.DateClosed = DateTime.Now;
            else
                throw new ArgumentException(nameof(task.Status), "Dad status change");
            return _repository.UpdateTask(task);
        }
    }
}