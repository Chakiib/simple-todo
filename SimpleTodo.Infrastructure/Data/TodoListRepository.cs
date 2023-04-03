namespace SimpleTodo.Infrastructure.Data;

using Microsoft.EntityFrameworkCore;
using SimpleTodo.Core.Interfaces;
using SimpleTodo.Core.Models;

public class TodoListRepository : RepositoryBase<TodoList>, ITodoListRepository
{
    protected override IQueryable<TodoList> EntitySet { get; }

    public TodoListRepository(TodoDbContext context) : base(context)
    {
        EntitySet = context.TodoLists.Include(i => i.TodoItems);
    }
}