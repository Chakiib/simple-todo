namespace SimpleTodo.Infrastructure;

using SimpleTodo.Core.Models;

public static class DbInitializer
{
    public static void Initialize(TodoDbContext dbContext)
    {
        if (dbContext.TodoLists.Any() && dbContext.TodoItems.Any())
        {
            return; // DB has been seeded
        }

        var todoList = new TodoList { Name = "My todo list", ToDoItems = new List<TodoItem>() };
        dbContext.TodoLists.Add(todoList);
        dbContext.SaveChanges();

        var todoItem1 = new TodoItem { Name = "Todo 1", TodoListId = todoList.Id };
        var todoItem2 = new TodoItem { Name = "Todo 2", IsComplete = true, TodoListId = todoList.Id };
        var todoItem3 = new TodoItem { Name = "Todo 3", TodoListId = todoList.Id };
        dbContext.TodoItems.Add(todoItem1);
        dbContext.TodoItems.Add(todoItem2);
        dbContext.TodoItems.Add(todoItem3);

        todoList.ToDoItems.Add(todoItem1);
        todoList.ToDoItems.Add(todoItem2);
        todoList.ToDoItems.Add(todoItem3);

        dbContext.SaveChanges();
    }
}