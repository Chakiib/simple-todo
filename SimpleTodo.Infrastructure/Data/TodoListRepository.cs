namespace SimpleTodo.Infrastructure.Data;

using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SimpleTodo.Core.Interfaces;
using SimpleTodo.Core.Models;

public class TodoListRepository : ITodoListRepository
{
    private readonly TodoDbContext _context;

    public TodoListRepository(TodoDbContext dbContext)
    {
        _context = dbContext;
    }

    public async Task<ICollection<TodoList>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.TodoLists.Include(l => l.TodoItems).ToListAsync(cancellationToken);
    }

    public async Task<TodoList?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _context.TodoLists.FindAsync(new object[] { id }, cancellationToken);
    }

    public async Task<IEnumerable<TodoList>> FindAsync(Expression<Func<TodoList, bool>>? filter = null, CancellationToken cancellationToken = default)
    {
        IQueryable<TodoList> query = _context.TodoLists;

        if (filter != null)
        {
            query = query.Where(filter);
        }

        return await query.Include(l => l.TodoItems).ToListAsync(cancellationToken);
    }

    public async Task<TodoList> AddAsync(TodoList entity, CancellationToken cancellationToken = default)
    {
        _context.TodoLists.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task UpdateAsync(TodoList entity, CancellationToken cancellationToken = default)
    {
        _context.TodoLists.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> DeleteAsync(TodoList entity, CancellationToken cancellationToken = default)
    {
        _context.TodoLists.Remove(entity);
        var deleted = await _context.SaveChangesAsync(cancellationToken);
        return deleted > 0;
    }
}