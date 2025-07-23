using Microsoft.AspNetCore.Mvc;
using TaskApp.Shared.Models.Task;
using TaskAppAPI.Services.Interfaces;

namespace TaskAppAPI.Controllers
{
    [ApiController]
    [Route("api/tasks")]
    public class TaskController : ControllerBase
    {
        private readonly ILogger<TaskController> _logger;   
        private readonly ITaskService _taskService;

        public TaskController(ILogger<TaskController> logger, ITaskService taskService)
        {
            _logger = logger; 
            _taskService = taskService;
        }

   
		[HttpPost]
        public async Task<ActionResult> PostTask(CreateTaskDto taskDto)
        {
            var result = await _taskService.PostTask(taskDto);
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult> GetTasks(CancellationToken cancellationToken)
        {
            var result = await _taskService.GetTasks(cancellationToken);
            return Ok(result.Data);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult> GetTask(Guid id)
        {
            var result = await _taskService.GetTask(id);
            return Ok(result.Data);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateTask(UpdateTaskDto updateTaskDto)
        {
            var result = await _taskService.UpdateTask(updateTaskDto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> RemoveTask(Guid id)
        {
            var result = await _taskService.RemoveTask(id);
            return Ok(result);
        }
    }
}
