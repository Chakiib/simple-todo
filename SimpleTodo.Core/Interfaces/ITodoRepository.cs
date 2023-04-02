namespace SimpleTodo.Core.Interfaces;

using System.Linq.Expressions;
using SimpleTodo.Core.Models;

public interface ITodoRepository
{
    Task<IEnumerable<TodoItem>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<TodoItem?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<IEnumerable<TodoItem>> FindAsync(Expression<Func<TodoItem, bool>>? filter = null, CancellationToken cancellationToken = default);
    Task<TodoItem> AddAsync(TodoItem entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(TodoItem entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(TodoItem entity, CancellationToken cancellationToken = default);
}