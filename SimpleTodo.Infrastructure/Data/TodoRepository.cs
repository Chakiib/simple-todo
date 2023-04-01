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

    public async Task<TodoItem> GetByIdAsync(int id, CancellationToken cancellationToken)
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

    public Task<TodoItem> AddAsync(TodoItem entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(TodoItem entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(TodoItem entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}