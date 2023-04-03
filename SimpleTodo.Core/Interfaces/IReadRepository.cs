namespace SimpleTodo.Core.Interfaces;

using System.Linq.Expressions;

public interface IReadRepository<T> where T: EntityBase
{
    Task<List<T>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default);
    Task<List<T>> FindAsync(Expression<Func<T, bool>> filter, CancellationToken cancellationToken = default);
    IQueryable<T> FindAsQueryable(Expression<Func<T, bool>> filter);
}