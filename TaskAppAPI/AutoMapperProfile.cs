using AutoMapper;
using TaskApp.Shared.Models.Task;
using TaskAppAPI.Data.Entities;
namespace TaskAppAPI
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<TaskItem, CreateTaskDto>().ReverseMap();
            CreateMap<TaskItem, UpdateTaskDto>().ReverseMap();
            CreateMap<TaskItem, TaskDto>().ReverseMap();

        }
    }
}

