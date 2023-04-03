namespace SimpleTodo.Web.MappingProfiles;

using AutoMapper;
using SimpleTodo.Core.Models;
using SimpleTodo.Web.ApiModels;

public class ApplicationMappingProfile : Profile
{
    public ApplicationMappingProfile()
    {
        CreateMap<TodoItem, TodoDto>().ReverseMap();
        CreateMap<TodoList, TodoListDto>().ForMember(dest => dest.Todos, opt => opt.MapFrom(src => src.TodoItems)).ReverseMap();
    }
}