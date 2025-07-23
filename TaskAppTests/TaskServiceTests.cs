using Microsoft.Extensions.Logging;
using Moq;
using AutoMapper;
using TaskAppAPI.Data.Entities;
using TaskAppAPI.Repository.Interfaces;
using TaskAppAPI.Services.Implementations;
using System.Net;
using TaskApp.Shared.Models.Task;
using TaskApp.Shared.Models;

namespace TaskAppTests
{
    public class TaskServiceTests
    {
        private readonly TaskService _taskService;
        private readonly Mock<IRepository<TaskItem>> _taskRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<ILogger<TaskService>> _loggerMock = new();

        public TaskServiceTests()
        {
            _taskRepositoryMock = new Mock<IRepository<TaskItem>>();
            _mapperMock = new Mock<IMapper>();
            _taskService = new TaskService(
                _loggerMock.Object,
                _taskRepositoryMock.Object,
                _mapperMock.Object
            );
        }

        [Fact]
        public async Task PostTask_ShouldReturnGuid_WhenSuccessful()
        {
            // Arrange
            var taskDto = new CreateTaskDto { Title = "Test", Description = "Test task" };
            var taskItem = new TaskItem { Id = Guid.NewGuid() };

            _mapperMock.Setup(m => m.Map<TaskItem>(It.IsAny<CreateTaskDto>())).Returns(taskItem);
            _taskRepositoryMock.Setup(r => r.Add(It.IsAny<TaskItem>()));
            _taskRepositoryMock.Setup(r => r.CommitAsync()).ReturnsAsync(1);

            // Act
            var result = await _taskService.PostTask(taskDto);

            // Assert
            Assert.True(result.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            Assert.Equal(taskItem.Id, result.Data);
        }

        [Fact]
        public async Task GetTask_ShouldReturnMappedTask_WhenTaskExists()
        {
            // Arrange
            var taskId = Guid.NewGuid();
            var taskEntity = new TaskItem
            {
                Id = taskId,
                Title = "Sample Task",
                Description = "Test Description"
            };

            var taskDto = new TaskDto
            {
                Id = taskId,
                Title = "Sample Task",
                Description = "Test Description"
            };

            _taskRepositoryMock.Setup(r => r.FindByIdAsync(taskId)).ReturnsAsync(taskEntity);
            _mapperMock.Setup(m => m.Map<TaskDto>(taskEntity)).Returns(taskDto);

            // Act
            var result = await _taskService.GetTask(taskId);

            // Assert
            Assert.True(result.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            Assert.NotNull(result.Data);
            Assert.Equal(taskDto.Id, result.Data.Id);
            Assert.Equal(taskDto.Title, result.Data.Title);
        }

        [Fact]
        public async Task UpdateTask_ShouldUpdateTask_WhenValidInput()
        {
            // Arrange
            var taskId = Guid.NewGuid();
            var updateDto = new UpdateTaskDto
            {
                Id = taskId,
                Title = "Updated Title",
                Description = "Updated Desc"
            };

            var existingTask = new TaskItem
            {
                Id = taskId,
                Title = "Old Title",
                Description = "Old Desc"
            };

            _taskRepositoryMock.Setup(r => r.FindByIdAsync(taskId)).ReturnsAsync(existingTask);
            _mapperMock.Setup(m => m.Map(updateDto, existingTask)); // void
            _taskRepositoryMock.Setup(r => r.Update(existingTask));
            _taskRepositoryMock.Setup(r => r.CommitAsync()).ReturnsAsync(1);

            // Act
            var result = await _taskService.UpdateTask(updateDto);

            // Assert
            Assert.True(result.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            Assert.IsType<ServiceResponse<Guid>>(result);
            Assert.Equal(taskId, ((ServiceResponse<Guid>)result).Data);
        }
    }
}