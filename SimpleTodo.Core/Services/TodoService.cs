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

    public Task<TodoItem> CreateTodoAsync(
        TodoItem todoItem,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<TodoList?> UpdateTodoAsync(TodoItem todoItem, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteTodoAsync(int id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<TodoList>> GetTodoListsAsync(CancellationToken cancellationToken)
    {
        return await _todoListRepository.GetAllAsync(cancellationToken);
    }

    public async Task<TodoList?> GetTodoListAsync(int id, CancellationToken cancellationToken = default)
    {
        var item = await _todoListRepository.GetByIdAsync(id, cancellationToken);
        return item;
    }

    public async Task<TodoList> CreateTodoListAsync(TodoList todoList, CancellationToken cancellationToken = default)
    {
        var newTodoList = await _todoListRepository.AddAsync(todoList, cancellationToken);
        return newTodoList;
    }

    public async Task<bool> DeleteTodoListAsync(int id, CancellationToken cancellationToken = default)
    {
        var todoList = await _todoListRepository.GetByIdAsync(id, cancellationToken);
        if (todoList == null)
        {
            return false;
        }

        return await _todoListRepository.DeleteAsync(todoList, cancellationToken);
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
}