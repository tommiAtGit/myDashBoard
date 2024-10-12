using myTodoService.Services;
using myTodoService.Domain;
using myTodoService.controllers;
using myNotesService.Services;

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
            List<TaskDTO> tasks = CreateListOfTasks();
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
        public void GetAllTasksTest()
        {
            // Given

            // When

            // Then
        }
        [Fact]
        public void GetTaskByIdTest()
        {
            // Given

            // When

            // Then
        }
        [Fact]
        public void GetTaskByDateTest()
        {

        }
        [Fact]
        public void UpdateTask_UpdateStausTest()
        {
            // Given

            // When

            // Then
        }
        [Fact]
        public void UpdateTask_CheckStatusDatesTest()
        {
            // Given

            // When

            // Then
        }
        [Fact]
        public void UpdateTask_UpdateDescription()
        {
            // Given

            // When

            // Then
        }
        private static TaskDTO CreateNewTask()
        {
            TaskDTO task = new()
            {
                Name = TASK_NAME,
                Description = TASK_DESCRIPTION,
                Reporter = "Tommi",
                Owner = "Tommi",
                Status = (int)TodoStatus.OPEN,
                DateReported = DateTime.Now
            };

            return task;
        }

        private static List<TaskDTO> CreateListOfTasks()
        {
            List<TaskDTO> taskList = [];
            for (int i = 0; i < NUMBER_OF_TASKS; i++)
            {
                TaskDTO task = new();
                task.Name = "Create new Task_#" + i;
                task.Description = "New task for testig_#" + i;
                task.Reporter = "Tommi_#" + i;
                task.Owner = "Tommi_#" + i;
                task.Status = (int)TodoStatus.OPEN;
                DateTime date = DateTime.Now;
                task.DateReported = date.AddDays(i * (-1));
                taskList.Add(task);

            }

            return taskList;
        }

    }
}