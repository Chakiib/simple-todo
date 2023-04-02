namespace SimpleTodo.Core.Interfaces;

using SimpleTodo.Core.Models;

public interface ITodoService
{
    Task<IEnumerable<TodoItem>> GetTodosAsync(CancellationToken cancellationToken = default);
    Task<TodoItem?> GetTodoAsync(int id, CancellationToken cancellationToken = default);
    Task<TodoItem?> CreateTodoAsync(int todoListId, TodoItem todo, CancellationToken cancellationToken = default);
    Task<TodoItem?> UpdateTodoAsync(TodoItem todoItem, CancellationToken cancellationToken = default);
    Task<bool> DeleteTodoAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<TodoList>> GetTodoListsAsync(CancellationToken cancellationToken = default);
    Task<TodoList?> GetTodoListAsync(int id, CancellationToken cancellationToken = default);
    Task<bool> TodoListExistsAsync(int id, CancellationToken cancellationToken = default);
    Task<TodoList> CreateTodoListAsync(TodoList todoList, CancellationToken cancellationToken = default);
    Task<TodoList?> UpdateTodoListAsync(TodoList model, CancellationToken cancellationToken = default);
    Task<bool> DeleteTodoListAsync(int id, CancellationToken cancellationToken = default);
}