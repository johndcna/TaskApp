using System.Net.Http.Json;
using TaskApp.Shared.Models.Task;

namespace Blazor.Services
{
    public class TaskApiService
    {
        private readonly HttpClient _httpClient;

        public TaskApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        public async Task<List<TaskDto>> GetTasksAsync(CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.GetFromJsonAsync<List<TaskDto>>("api/tasks", cancellationToken);
            return response;
        }


        public async Task<TaskDto?> GetTaskAsync(Guid id)
        {
            var response = await _httpClient.GetFromJsonAsync<TaskDto>($"api/tasks/{id}");
            if (response != null)
            {
                return response;
            }
            return null;
        }

        public async Task<bool> CreateTaskAsync(CreateTaskDto createTaskDto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/tasks", createTaskDto);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateTaskAsync(UpdateTaskDto updateTaskDto)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/tasks", updateTaskDto);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteTaskAsync(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"api/tasks/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
