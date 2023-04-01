namespace SimpleTodo.Core.Interfaces;

using System.Linq.Expressions;
using SimpleTodo.Core.Models;

public interface ITodoRepository
{
    Task<TodoItem> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<IEnumerable<TodoItem>> FindAsync(Expression<Func<TodoItem, bool>>? filter = null, CancellationToken cancellationToken = default);
    Task<TodoItem> AddAsync(TodoItem entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(TodoItem entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(TodoItem entity, CancellationToken cancellationToken = default);
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}