using AutoMapper;
using System.Net;
using TaskAppAPI.Repository.Interfaces;
using TaskAppAPI.Services.Interfaces;
using TaskAppAPI.Data.Entities;
using TaskApp.Shared.Models;
using TaskApp.Shared.Models.Task;

namespace TaskAppAPI.Services.Implementations
{
    public class TaskService : ITaskService
    {
        private readonly ILogger<TaskService> _logger;
        private readonly IRepository<TaskItem> _taskRepository;
        private readonly IMapper _mapper;
        public TaskService(ILogger<TaskService> logger, IRepository<TaskItem> taskRepository, IMapper mapper)
        {
            _logger = logger;
            _taskRepository = taskRepository;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<Guid>> PostTask(CreateTaskDto createTaskDto)
        {
            var task = _mapper.Map<TaskItem>(createTaskDto);
            _taskRepository.Add(task);

                var taskEntity = await _taskRepository.CommitAsync();

            return new ServiceResponse<Guid>(HttpStatusCode.OK)
            {
                Data = task.Id
            };
        }
    

        public async Task<ServiceResponse<TaskDto>> GetTask(Guid taskId)
        {
            var task = await _taskRepository.FindByIdAsync(taskId);

            return new ServiceResponse<TaskDto>(HttpStatusCode.OK)
            {
                Data = _mapper.Map<TaskDto>(task)

            };
        }

        public async Task<ServiceResponse<List<TaskDto>>> GetTasks(CancellationToken cancellationToken)
        {

            var tasks = await _taskRepository.FindAll();

            return new ServiceResponse<List<TaskDto>>(HttpStatusCode.OK)
            {
                Data = tasks.Select(_ => _mapper.Map<TaskDto>(_)).ToList()

            };

        }

        public async Task<ServiceResponse> RemoveTask(Guid taskId)
        {
            var task = await _taskRepository.FindByIdAsync(taskId);
            _taskRepository.Delete(entity: task);
            await _taskRepository.CommitAsync();

            return new ServiceResponse(HttpStatusCode.OK);
        }

        public async Task<ServiceResponse> UpdateTask(UpdateTaskDto updateTaskDTO)
        {
            var task = await _taskRepository.FindByIdAsync(updateTaskDTO.Id);
            _mapper.Map(updateTaskDTO, task);
            _taskRepository.Update(task);
            try
            {
                await _taskRepository.CommitAsync();
            }
            catch (Exception ex)
            {
                    _logger.LogError($"Task Update Error Message - {ex.Message.ToString()}");
                    throw new Exception(ex.Message.ToString());
   
            }

            return new ServiceResponse<Guid>(HttpStatusCode.OK)
            {
                Data = task.Id

            };
        }
    }
}
