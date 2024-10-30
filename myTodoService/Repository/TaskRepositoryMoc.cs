using myTodoService.Domain;
using System.Collections.Generic;

using System.Linq;

namespace myTodoService.Repository
{
    public class TaskRepositoryMoc : ITaskRepositoryMoc
    {
        private List<TaskDTO> _mocTasks;
        public TaskRepositoryMoc()
        {
            _mocTasks ??= [];
            _mocTasks = MockTasks();

        }

        public TaskDTO AddNewTask(TaskDTO newTask)
        {

            if (newTask != null)
            {
                newTask.Id = Guid.NewGuid();
                _mocTasks.Add(newTask);
                return newTask;
            }
            else
            {
                throw new ArgumentNullException();
            }
        }

        public bool DeleteTask(Guid Id)
        {
            TaskDTO theTask = GetTaskById(Id);
            if (theTask != null)
                return _mocTasks.Remove(theTask);
            else
                throw new ArgumentNullException("NotFound", nameof(theTask));
        }

        public IEnumerable<TaskDTO> GetAllTasks()
        {
            return _mocTasks;
        }

        public TaskDTO GetTaskById(Guid Id)
        {

            var task = _mocTasks.FirstOrDefault(p => p.Id == Id);
            if (task != null)
                return task;
            else
                throw new ArgumentNullException("NotFound", nameof(task));


        }

        public IEnumerable<TaskDTO> GetTasksByDate(DateTime startDate, DateTime EndDate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TaskDTO> GetTasksByStatus(TodoStatus status)
        {
            var tasks = _mocTasks.FindAll(p => p.Status == status);
            if (tasks != null)
                return tasks;
            else
                throw new ArgumentNullException("NotFound", nameof(tasks));

        }

        public TaskDTO UpdateTask(TaskDTO task)
        {
            var tempTask = _mocTasks.FirstOrDefault(p => p.Id == task.Id);
            if (tempTask != null){
                int index = _mocTasks.IndexOf(tempTask);
                _mocTasks[index] = task;
                return _mocTasks[index];
            }
            else{
                throw new Exception();
            }


        }
        private List<TaskDTO> MockTasks(){
            const int NUMBER_OF_TASKS = 10;

            List<TaskDTO> taskList = [];
            for (int i = 0; i < NUMBER_OF_TASKS; i++)
            {
                TaskDTO task = new();
                task.Id = new Guid("8face6e0-fce6-40d6-b3f9-12d2d8dcaba"+i);
                task.Name = "Create new Task_#" + i;
                task.Description = "New task for testig_#" + i;
                task.Reporter = "Tommi_#" + i;
                task.Owner = "Tommi_#" + i;
                task.Status = TodoStatus.OPEN;
                DateTime date = DateTime.Now;
                task.DateReported = date.AddDays(i * (-1));
                taskList.Add(task);

            }
            taskList[2].Status = TodoStatus.CLOSED;
            taskList[4].Status = TodoStatus.DEPRECATED;
            taskList[6].Status = TodoStatus.PROGRESS;
            taskList[8].Status = TodoStatus.PROGRESS;
            taskList[8].Status = TodoStatus.DONE;

            return taskList;
        }
    }

}