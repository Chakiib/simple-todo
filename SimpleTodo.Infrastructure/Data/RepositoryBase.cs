namespace SimpleTodo.Infrastructure.Data;

using Microsoft.EntityFrameworkCore;
using SimpleTodo.Core;
using SimpleTodo.Core.Interfaces;

public abstract class RepositoryBase<T> : ReadRepositoryBase<T>, IRepository<T> where T : EntityBase
{
    protected DbSet<T> DbSet { get; }

    private readonly DbContext _context;

    protected RepositoryBase(DbContext context)
    {
        _context = context;
        DbSet = context.Set<T>();
    }

    public virtual async Task<T> AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        DbSet.Add(entity);
        await SaveChangesAsync(cancellationToken);
        return entity;
    }

    public virtual async Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        _context.Entry(entity).State = EntityState.Modified;
        await SaveChangesAsync(cancellationToken);
    }

    public virtual async Task DeleteAsync(T entity, CancellationToken cancellationToken = default)
    {
        DbSet.Remove(entity);
        await SaveChangesAsync(cancellationToken);
    }

    public virtual Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return _context.SaveChangesAsync(cancellationToken);
    }
}