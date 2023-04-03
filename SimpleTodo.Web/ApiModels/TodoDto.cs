namespace SimpleTodo.Web.ApiModels;

public class TodoDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; } = string.Empty;
    public bool IsComplete { get; set; }
    public int TodoListId { get; set; }
}