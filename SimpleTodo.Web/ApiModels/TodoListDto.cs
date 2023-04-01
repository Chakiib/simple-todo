namespace SimpleTodo.Web.ApiModels;

public class TodoListDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public IEnumerable<TodoDto> Todos { get; set; } = new List<TodoDto>();
}