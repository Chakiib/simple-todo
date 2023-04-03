namespace SimpleTodo.Core.Services;

using SimpleTodo.Core.Interfaces;
using SimpleTodo.Core.Models;

public class TodoService : ITodoService
{
    private readonly ITodoRepository _todoRepository;
    private readonly ITodoListRepository _todoListRepository;

    public TodoService(ITodoRepository todoRepository, ITodoListRepository todoListRepository)
    {
        _todoRepository = todoRepository;
        _todoListRepository = todoListRepository;
    }

    public async Task<IEnumerable<TodoItem>> GetTodosAsync(CancellationToken cancellationToken = default)
    {
        return await _todoRepository.GetAllAsync(cancellationToken);
    }

    public async Task<TodoItem?> GetTodoAsync(int id, CancellationToken cancellationToken = default)
    {
        var item = await _todoRepository.GetByIdAsync(id, cancellationToken);
        return item;
    }

    public async Task<TodoItem?> CreateTodoAsync(int todoListId, TodoItem todo, CancellationToken cancellationToken = default)
    {
        var todoList = await _todoListRepository.GetByIdAsync(todoListId, cancellationToken);
        if (todoList == null)
        {
            return null;
        }

        todoList.TodoItems.Add(todo);
        await _todoListRepository.UpdateAsync(todoList, cancellationToken);
        return todo;
    }

    public async Task<TodoItem?> UpdateTodoAsync(TodoItem model, CancellationToken cancellationToken = default)
    {
        var todoItem = await _todoRepository.GetByIdAsync(model.Id, cancellationToken);
        if (todoItem == null)
        {
            return null;
        }

        todoItem.Name = model.Name;
        todoItem.Description = model.Description;
        todoItem.IsComplete = model.IsComplete;
        await _todoRepository.UpdateAsync(todoItem, cancellationToken);

        return todoItem;
    }

    public async Task<bool> DeleteTodoAsync(int id, CancellationToken cancellationToken = default)
    {
        var todoItem = await _todoRepository.GetByIdAsync(id, cancellationToken);
        if (todoItem == null)
        {
            return false;
        }

        await _todoRepository.DeleteAsync(todoItem, cancellationToken);
        return true;
    }

    public async Task<IEnumerable<TodoList>> GetTodoListsAsync(CancellationToken cancellationToken)
    {
        return await _todoListRepository.GetAllAsync(cancellationToken);
    }

    public Task<TodoList?> GetTodoListAsync(int id, CancellationToken cancellationToken = default)
    {
        return _todoListRepository.GetByIdAsync(id, cancellationToken);
    }

    public Task<bool> TodoListExistsAsync(int id, CancellationToken cancellationToken = default)
    {
        return _todoListRepository.ExistsAsync(id, cancellationToken);
    }

    public async Task<TodoList> CreateTodoListAsync(TodoList todoList, CancellationToken cancellationToken = default)
    {
        var newTodoList = await _todoListRepository.AddAsync(todoList, cancellationToken);
        return newTodoList;
    }

    public async Task<TodoList?> UpdateTodoListAsync(
        TodoList model,
        CancellationToken cancellationToken = default)
    {
        var todoList = await _todoListRepository.GetByIdAsync(model.Id, cancellationToken);
        if (todoList == null)
        {
            return null;
        }

        // Update only the name
        todoList.Name = model.Name;
        await _todoListRepository.UpdateAsync(todoList, cancellationToken);

        return todoList;
    }

    public async Task<bool> DeleteTodoListAsync(int id, CancellationToken cancellationToken = default)
    {
        var todoList = await _todoListRepository.GetByIdAsync(id, cancellationToken);
        if (todoList == null)
        {
            return false;
        }

        await _todoListRepository.DeleteAsync(todoList, cancellationToken);
        return true;
    }
}