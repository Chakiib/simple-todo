namespace SimpleTodo.Infrastructure.Data;

using SimpleTodo.Core.Interfaces;
using SimpleTodo.Core.Models;

public class TodoRepository : RepositoryBase<TodoItem>, ITodoRepository
{
    protected override IQueryable<TodoItem> EntitySet { get; }

    public TodoRepository(TodoDbContext context) : base(context)
    {
        EntitySet = context.TodoItems;
    }
}