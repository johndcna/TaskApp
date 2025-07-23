using TaskApp.Shared.Models;
using TaskApp.Shared.Models.Task;

namespace BlazorApp.Services
{
    public class TaskService
    {
        private readonly HttpClient _http;

        public TaskService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<TaskDto>> GetTasksAsync(CancellationToken cancellationToken = default)
        {
            // Call GET api/tasks
            var response = await _http.GetFromJsonAsync<List<TaskDto>>("api/tasks", cancellationToken);
            return response;
        }

        public async Task<ServiceResponse<TaskDto>> GetTaskAsync(Guid taskId)
        {
            // Call GET api/tasks/{id}
            var response = await _http.GetFromJsonAsync<ServiceResponse<TaskDto>>($"api/tasks/{taskId}");
            return response;
        }

        public Task PostTaskAsync(CreateTaskDto createTaskDto)
        {
            // Call POST api/tasks
            //var httpResponse = await _http.PostAsJsonAsync("api/tasks", createTaskDto);
            //return await httpResponse.Content.ReadFromJsonAsync<ServiceResponse<Guid>>();
            return Task.CompletedTask;
        }

        public async Task<ServiceResponse> RemoveTaskAsync(Guid taskId)
        {
            // Call DELETE api/tasks/{id}
            var httpResponse = await _http.DeleteAsync($"api/tasks/{taskId}");
            return await httpResponse.Content.ReadFromJsonAsync<ServiceResponse>();
        }

        public async Task<ServiceResponse> UpdateTaskAsync(UpdateTaskDto updateTaskDto)
        {
            // Call PUT api/tasks/{id}
            var httpResponse = await _http.PutAsJsonAsync($"api/tasks/{updateTaskDto.Id}", updateTaskDto);
            return await httpResponse.Content.ReadFromJsonAsync<ServiceResponse>();
        }
    }

}
