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

    public async Task<TodoItem> GetTodoAsync(int id, CancellationToken cancellationToken = default)
    {
        var item = await _todoRepository.GetByIdAsync(id, cancellationToken);

        return item;
    }

    public async Task<TodoList> GetTodoListAsync(int id, CancellationToken cancellationToken = default)
    {
        var item = await _todoListRepository.GetByIdAsync(id, cancellationToken);

        return item;
    }
}