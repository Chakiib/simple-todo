namespace SimpleTodo.Core.Interfaces;

using System.Linq.Expressions;
using SimpleTodo.Core.Models;

public interface ITodoListRepository
{
    Task<ICollection<TodoList>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<TodoList?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<TodoList>> FindAsync(Expression<Func<TodoList, bool>>? filter = null, CancellationToken cancellationToken = default);
    Task<TodoList> AddAsync(TodoList entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(TodoList entity, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(TodoList entity, CancellationToken cancellationToken = default);
}