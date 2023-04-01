namespace SimpleTodo.Core.Interfaces;

using System.Linq.Expressions;
using SimpleTodo.Core.Models;

public interface ITodoListRepository
{
    Task<TodoList> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<IEnumerable<TodoList>> FindAsync(Expression<Func<TodoList, bool>>? filter = null, CancellationToken cancellationToken = default);
    Task<TodoList> AddAsync(TodoList entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(TodoList entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(TodoList entity, CancellationToken cancellationToken = default);
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}