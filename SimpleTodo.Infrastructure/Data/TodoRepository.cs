namespace SimpleTodo.Infrastructure.Data;

using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SimpleTodo.Core.Interfaces;
using SimpleTodo.Core.Models;

public class TodoRepository : ITodoRepository
{
    private readonly TodoDbContext _context;

    public TodoRepository(TodoDbContext dbContext)
    {
        _context = dbContext;
    }

    public async Task<IEnumerable<TodoItem>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.TodoItems.ToListAsync(cancellationToken);
    }

    public async Task<TodoItem?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _context.TodoItems.FindAsync(new object[] { id }, cancellationToken);
    }

    public async Task<IEnumerable<TodoItem>> FindAsync(Expression<Func<TodoItem, bool>>? filter = null, CancellationToken cancellationToken = default)
    {
        IQueryable<TodoItem> query = _context.TodoItems;

        if (filter != null)
        {
            query = query.Where(filter);
        }

        return await query.ToListAsync(cancellationToken);
    }

    public async Task<TodoItem> AddAsync(TodoItem entity, CancellationToken cancellationToken = default)
    {
        _context.TodoItems.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task UpdateAsync(TodoItem entity, CancellationToken cancellationToken = default)
    {
        _context.TodoItems.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> DeleteAsync(TodoItem entity, CancellationToken cancellationToken = default)
    {
        _context.TodoItems.Remove(entity);
        var deleted = await _context.SaveChangesAsync(cancellationToken);
        return deleted > 0;
    }
}