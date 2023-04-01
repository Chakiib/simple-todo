namespace SimpleTodo.Web;

using AutoMapper;
using SimpleTodo.Core.Models;
using SimpleTodo.Web.ApiModels;

public class ApplicationMappingProfile : Profile
{
    public ApplicationMappingProfile()
    {
        CreateMap<TodoItem, TodoDto>();
        CreateMap<TodoList, TodoListDto>()
            .ForMember(dest => dest.Todos, opt => opt.MapFrom(src => src.ToDoItems));
    }
}