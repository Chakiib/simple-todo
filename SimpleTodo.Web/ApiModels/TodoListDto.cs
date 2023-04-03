namespace SimpleTodo.Web.ApiModels;

using System.ComponentModel.DataAnnotations;

public class TodoListDto
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    public IEnumerable<TodoDto> Todos { get; set; } = new List<TodoDto>();
}