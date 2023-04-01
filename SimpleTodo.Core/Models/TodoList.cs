namespace SimpleTodo.Core.Models;

public class TodoList : EntityBase
{
    public string Name { get; set; }
    public ICollection<TodoItem> ToDoItems { get; set; }
}