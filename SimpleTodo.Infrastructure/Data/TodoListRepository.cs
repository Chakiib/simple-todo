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

    public async Task<TodoList> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        IQueryable<TodoList> query = _context.TodoLists;

        return await query.Where(list => list.Id == id)
            .Include(l => l.ToDoItems)
            .SingleOrDefaultAsync(cancellationToken);
    }

    public async Task<IEnumerable<TodoList>> FindAsync(Expression<Func<TodoList, bool>>? filter = null, CancellationToken cancellationToken = default)
    {
        IQueryable<TodoList> query = _context.TodoLists;

        if (filter != null)
        {
            query = query.Where(filter);
        }

        return await query.ToListAsync(cancellationToken);
    }

    public Task<TodoList> AddAsync(TodoList entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(TodoList entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(TodoList entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}