namespace SimpleTodo.Core.Models;

public class TodoItem : EntityBase
{
    public string Name { get; set; }
    public string Description { get; set; } = string.Empty;
    public bool IsComplete { get; set; }
    public int TodoListId { get; set; }
}