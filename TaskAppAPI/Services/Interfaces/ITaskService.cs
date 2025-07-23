

using TaskApp.Shared.Models;
using TaskApp.Shared.Models.Task;

namespace TaskAppAPI.Services.Interfaces
{
    public interface ITaskService
    {

        Task<ServiceResponse<List<TaskDto>>> GetTasks(CancellationToken cancellationToken);
        Task<ServiceResponse<TaskDto>> GetTask(Guid taskId);
        Task<ServiceResponse<Guid>> PostTask(CreateTaskDto createTaskDto);

        Task<ServiceResponse> RemoveTask(Guid taskId);
        Task<ServiceResponse> UpdateTask(UpdateTaskDto updateTaskDTO);
    }
}
