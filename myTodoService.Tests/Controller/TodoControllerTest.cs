using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using myTodoService.controllers;
using myTodoService.Domain;
using myTodoService.Services;
using Microsoft.AspNetCore.Http.HttpResults;

namespace myTodoService.Tests.Controllers
{
    public class TodoControllerTests
    {
        private const int NUMBER_OF_TASKS = 10;
        private readonly Mock<ITodoService> _mockTodoService;
        private readonly TodoController _controller;

        public TodoControllerTests()
        {
            // Arrange
            _mockTodoService = new Mock<ITodoService>();
            _controller = new TodoController(_mockTodoService.Object);
        }

        [Fact]
        public void GetAllTasks_ReturnsOkResult_WithListOfTaks()
        {
            // Given
            List<TaskDTO> tasks = CreateListOfOpenTasks();
            _mockTodoService.Setup(service => service.GetAllTasks()).Returns(tasks);
            // When
            var result = _controller.GetAllTasks();

            // Then
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedTasks = Assert.IsAssignableFrom<IEnumerable<TaskDTO>>(okResult.Value);
            Assert.Equal(NUMBER_OF_TASKS, returnedTasks?.Count());
            const int i = 2;
            TaskDTO? myTasks = returnedTasks?.ElementAt(i);
            Assert.NotNull(myTasks);
            Assert.Equal(myTasks.Name, tasks[i].Name);
            Assert.Equal(myTasks.Owner, tasks[i].Owner);
            Assert.Equal(TodoStatus.OPEN, myTasks.Status);



        }
        [Fact]
        public void GetTaskById_ReturnsOKResult_WithTask()
        {
            // Given
            Guid id = Guid.NewGuid();
            TaskDTO task = CreateSingleTask(id);

            _mockTodoService.Setup(service => service.GetTaskById(id)).Returns(task);
            // When
            var result = _controller.GetTaskById(id);


            // Then
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedTask = Assert.IsType<TaskDTO>(okResult.Value);
            Assert.Equal(task.Id, returnedTask.Id);
            Assert.Equal(task.Name, returnedTask.Name);
            Assert.Equal(task.Reporter, returnedTask.Reporter);
            Assert.Equal(task.Status, returnedTask.Status);
        }

        [Fact]
        public void GetTaskById_ReturnsNotFound_WithOutTask()
        {
            // Given
            Guid id = Guid.NewGuid();
            TaskDTO task = CreateSingleTask(id);

            _mockTodoService.Setup(service => service.GetTaskById(Guid.NewGuid())).Returns(task);
            // When
            var result = _controller.GetTaskById(Guid.NewGuid());


            // Then
            var notFoundResult = Assert.IsType<NotFoundResult>(result.Result);

        }
        [Fact]
        public void GetTaskByStatus_ReturnOKResult_WithObject()
        {
            // Given
            Guid id = Guid.NewGuid();
            TaskDTO task = CreateSingleTask(id);
            IEnumerable<TaskDTO> taskDTOs = CreateListOfOpenTasks();
            
            _mockTodoService.Setup(service => service.GetTasksByStatus(TodoStatus.OPEN)).Returns(taskDTOs);
            // When
            var result = _controller.GetTasksByStatus((int)TodoStatus.OPEN);

            // Then
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedTasks = Assert.IsAssignableFrom<IEnumerable<TaskDTO>>(okResult.Value);
            Assert.Equal(NUMBER_OF_TASKS, returnedTasks?.Count());
            const int i = 2;
            TaskDTO? myTasks = returnedTasks?.ElementAt(i);
            Assert.NotNull(myTasks);
            
        }
        [Fact]
        public void GetTaskByStatus_NotFoundResult()
        {
            // Given
             Guid id = Guid.NewGuid();
            TaskDTO task = CreateSingleTask(id);
            IEnumerable<TaskDTO> taskDTOs = CreateListOfOpenTasks();
            // When
        var result = _controller.GetTasksByStatus((int)TodoStatus.UNDEFINED);

            // Then
            var notFoundResult = Assert.IsType<NotFoundResult>(result.Result);
        }
        [Fact]
        public void AddTask_ReturnsOKResult_ReturnsCreatedTask()
        {
            // Given
            Guid id = Guid.NewGuid();
            TaskDTO newTask = CreateSingleTask(id);
            TaskDTO createdTask = CreateSingleTask(id);

            // When
            _mockTodoService.Setup(service => service.AddNewTask(newTask)).Returns(createdTask);

            // Act
            var result = _controller.AddTask(newTask);
            // Then
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returnedTask = Assert.IsType<TaskDTO>(createdAtActionResult.Value);
            Assert.Equal(createdTask.Id, returnedTask.Id);
            Assert.Equal(createdTask.Name, returnedTask.Name);
            Assert.Equal(createdTask.Reporter, returnedTask.Reporter);
            Assert.Equal(createdTask.Status, returnedTask.Status);
        }

        [Fact]
        public void UpdateTask_ReturnsOKResult_WithUpdatedTask()
        {
            // Given
            Guid id = Guid.NewGuid();
            TaskDTO newTask = CreateSingleTask(id);
            TaskDTO UpdatedTask = CreateSingleTask(id);

            _mockTodoService.Setup(service => service.UpdateTask(newTask)).Returns(UpdatedTask);

            // When
            var result = _controller.UpdateTask(id, newTask);

            // Then
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedTask = Assert.IsType<TaskDTO>(okResult.Value);
            Assert.Equal(UpdatedTask.Id, returnedTask.Id);
            Assert.Equal(UpdatedTask.Name, returnedTask.Name);
            Assert.Equal(UpdatedTask.Reporter, returnedTask.Reporter);
            Assert.Equal(UpdatedTask.Status, returnedTask.Status);
        }

        [Fact]
        public void DeleteTask_WithOKResult()
        {
            
            // Given
            Guid id = Guid.NewGuid();
            _mockTodoService.Setup(service => service.DeleteTask(id)).Returns(true);

            // When
            var result = _controller.DeleteTask(id);

            // Then
            Assert.IsType<NoContentResult>(result);
        }
        [Fact]
        public void DeleteTask_WithNotFoundResult()
        {
             // Given
            Guid id = Guid.NewGuid();
            _mockTodoService.Setup(service => service.DeleteTask(id)).Returns(false);

            // When
            var result = _controller.DeleteTask(id);

            // Then
            Assert.IsType<NotFoundResult>(result);
        }

        private static TaskDTO CreateSingleTask(Guid id)
        {
            DateTime date = DateTime.Now;
            return new TaskDTO()
            {
                Id = id,
                Name = "Create new Task_#0",
                Description = "New task for testig_#0",
                Reporter = "Tommi_#0",
                Owner = "Tommi_#0",
                Status = TodoStatus.OPEN,
                DateReported = date.AddDays(3 * (-1))

            };





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
            taskList[2].Status = TodoStatus.OPEN;
            taskList[4].Status = TodoStatus.DEPRECATED;
            taskList[6].Status = TodoStatus.PROGRESS;
            taskList[8].Status = TodoStatus.CLOSED;
            taskList[9].Status = TodoStatus.DONE;

            return taskList;
        }


    }
}
