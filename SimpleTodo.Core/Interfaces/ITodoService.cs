namespace SimpleTodo.Core.Interfaces;

using SimpleTodo.Core.Models;

public interface ITodoService
{
    Task<TodoItem> GetTodoAsync(int id, CancellationToken cancellationToken);
    Task<TodoList> GetTodoListAsync(int id, CancellationToken cancellationToken);
}