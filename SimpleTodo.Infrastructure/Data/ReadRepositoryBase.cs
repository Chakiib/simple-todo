namespace SimpleTodo.Infrastructure.Data;

using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SimpleTodo.Core;
using SimpleTodo.Core.Interfaces;

public abstract class ReadRepositoryBase<T> : IReadRepository<T> where T : EntityBase
{
    protected abstract IQueryable<T> EntitySet { get; }

    public async Task<List<T>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await EntitySet.ToListAsync(cancellationToken);
    }

    public virtual Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return FindAsQueryable(i => i.Id == id).SingleOrDefaultAsync(cancellationToken);
    }

    public Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default)
    {
        return EntitySet.AnyAsync(i => i.Id == id, cancellationToken);
    }

    public async Task<List<T>> FindAsync(Expression<Func<T, bool>> filter, CancellationToken cancellationToken)
    {
        return await FindAsQueryable(filter).ToListAsync(cancellationToken);
    }

    public virtual IQueryable<T> FindAsQueryable(Expression<Func<T, bool>> filter)
    {
        return EntitySet.Where(filter);
    }
}