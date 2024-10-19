using myTodoService.Services;
using myTodoService.Domain;
using myTodoService.controllers;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.Serialization;

namespace myTodoService.Services.Tests
{
    public class TodoServiceTests
    {
        private const int NUMBER_OF_TASKS = 10;
        private const string TASK_NAME = "Create new Task";
        private const string TASK_DESCRIPTION = "New task for testig";
        private TodoService _service;

        public TodoServiceTests()
        {
            _service = new();
        }
        [Fact]
        public void AddNewTaskTest()
        {
            // Given
            var _task = CreateNewTask();

            // When
            var result = _service.AddNewTask(_task);
            var newTask = _service.GetAllTasks();
            // Then
            Assert.True(result.Id != Guid.Empty);
            Assert.Equal(TASK_NAME, result.Name);
            Assert.Single(newTask);
            var t = newTask.FirstOrDefault();
            if (t != null)
                Assert.Equal(TodoStatus.OPEN, t.Status);
        }
        [Fact]
        public void AddNewTasks_GetbyIdTest()
        {
            // Given
            List<TaskDTO> allTasks = [];
            List<TaskDTO> tasks = CreateListOfOpenTasks();
            foreach (TaskDTO t in tasks)
            {
                var result = _service.AddNewTask(t);
                Assert.True(result.Id != Guid.Empty);
            }
            allTasks = (List<TaskDTO>)_service.GetAllTasks();
            Assert.Equal(NUMBER_OF_TASKS, _service.GetAllTasks().Count());

            // When
            var t0 = _service.GetTaskById(allTasks[0].Id);
            var t3 = _service.GetTaskById(allTasks[3].Id);

            // Then
            Assert.Equal(allTasks[0].Id, t0.Id);
            Assert.Equal(allTasks[0].Name, t0.Name);
            Assert.Equal(allTasks[3].Id, t3.Id);
            Assert.Equal(allTasks[3].Name, t3.Name);

        }

        [Fact]
        public void GetTaskByDateTest()
        {

        }
        [Fact]
        public void GetTaskByStatusTest(){
            // Given
            List<TaskDTO> allTasks = [];

            InitializeOpenTasks();
            allTasks = (List<TaskDTO>)_service.GetAllTasks();
            Assert.Equal(NUMBER_OF_TASKS, allTasks.Count);
            // When
            var openTasks = _service.GetTasksByStatus(TodoStatus.OPEN);
            var progressTasks = _service.GetTasksByStatus(TodoStatus.PROGRESS);
            var doneTasks = _service.GetTasksByStatus(TodoStatus.DONE);
        }
        [Fact]
        public void UpdateTask_UpdateStausTest()
        {
            // Given
            List<TaskDTO> allTasks = [];

            InitializeOpenTasks();
            allTasks = (List<TaskDTO>)_service.GetAllTasks();
            Assert.Equal(NUMBER_OF_TASKS, allTasks.Count());
            // When
            // Status: Open -> Progress
            TaskDTO task_progress = CopyItemOfObject(allTasks[1]);
            task_progress.Status = TodoStatus.PROGRESS;
            _service.UpdateTask(task_progress);
            
            //Status Done -> Open
            TaskDTO task_done = CopyItemOfObject(allTasks[8]);
            task_done.Status = TodoStatus.OPEN;
            _service.UpdateTask(task_done);

            // Then
            var t1 = _service.GetTaskById(task_progress.Id);
            Assert.NotNull(t1);
            Assert.True(t1.DateReported != default);
            Assert.True(t1.Status == TodoStatus.PROGRESS);
            Assert.True(t1.DateOpened != default);
           
            var t2 = _service.GetTaskById(task_done.Id);
             Assert.NotNull(t2);
             Assert.True(t2.DateReported != default);
            Assert.True(t2.Status == TodoStatus.OPEN);
            Assert.True(t2.DataCompleted != default);

        }

        [Fact]
        public void DeleteTaskTest()
        {
            // Given
            List<TaskDTO> allTasks = [];

            InitializeOpenTasks();
            allTasks = (List<TaskDTO>)_service.GetAllTasks();
            Assert.Equal(NUMBER_OF_TASKS, allTasks.Count());

            // When
            var TestTask = allTasks[7];
            _service.DeleteTask(TestTask.Id);
            // Then
            IEnumerable<TaskDTO> d = _service.GetAllTasks();
            Assert.Equal(NUMBER_OF_TASKS - 1, d.Count());
        }
        [Fact]
        public void UpdateTask_UpdateDescription()
        {
            // Given
            List<TaskDTO> allTasks = [];

            InitializeOpenTasks();
            allTasks = (List<TaskDTO>)_service.GetAllTasks();
            Assert.Equal(NUMBER_OF_TASKS, allTasks.Count());
            // When
            TaskDTO testTask = CopyItemOfObject(allTasks[3]);
            testTask.Description = "Updated_by_test";
            testTask.Owner = "Updated_by_test";
            _service.UpdateTask(testTask);

            // Then
            var t1 = _service.GetTaskById(testTask.Id);
            Assert.NotNull(t1);
            Assert.Equal(t1.Description, testTask.Description);
            Assert.Equal(t1.Owner, testTask.Owner);
        }
        private static TaskDTO CreateNewTask()
        {
            TaskDTO task = new()
            {
                Name = TASK_NAME,
                Description = TASK_DESCRIPTION,
                Reporter = "Tommi",
                Owner = "Tommi",
                Status = TodoStatus.OPEN,
                DateReported = DateTime.Now
            };

            return task;
        }

        private void InitializeOpenTasks()
        {
            List<TaskDTO> tasks = CreateListOfOpenTasks();
            foreach (TaskDTO t in tasks)
            {
                var result = _service.AddNewTask(t);
                Assert.True(result.Id != Guid.Empty);
            }
        }
        private TaskDTO CopyItemOfObject(TaskDTO copyTask)
        {
            TaskDTO o = new()
            {
                Id = copyTask.Id,
                Name = copyTask.Name,
                Description = copyTask.Description,
                Reporter = copyTask.Reporter,
                Owner = copyTask.Owner,
                Status = copyTask.Status,
                DateReported = copyTask.DateReported,
                DateOpened = copyTask.DateOpened,
                DataCompleted = copyTask.DataCompleted,
                DateClosed = copyTask.DateClosed
            };
            return o;
        }

        private static List<TaskDTO> CreateListOfOpenTasks()
        {
            List<TaskDTO> taskList = [];
            for (int i = 0; i < NUMBER_OF_TASKS; i++)
            {
                TaskDTO task = new();
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