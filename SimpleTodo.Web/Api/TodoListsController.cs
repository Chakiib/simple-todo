namespace SimpleTodo.Web.Api;

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SimpleTodo.Core.Interfaces;
using SimpleTodo.Core.Models;
using SimpleTodo.Web.ApiModels;

[Route("api/[controller]")]
[ApiController]
public class TodoListsController : ControllerBase
{
    private readonly ITodoService _todoService;
    private readonly IMapper _mapper;
    private readonly ILogger<TodoListsController> _logger;

    public TodoListsController(ITodoService todoService, IMapper mapper, ILogger<TodoListsController> logger)
    {
        _todoService = todoService;
        _mapper = mapper;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TodoListDto>>> GetTodoListsAsync(CancellationToken cancellationToken = default)
    {
        var todoList = await _todoService.GetTodoListsAsync(cancellationToken);

        _logger.LogInformation($"Returned all todo lists.");
        return Ok(_mapper.Map<IEnumerable<TodoListDto>>(todoList));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<TodoListDto>> GetTodoList(int id, CancellationToken cancellationToken = default)
    {
        var todoList = await _todoService.GetTodoListAsync(id, cancellationToken);

        if (todoList == null)
        {
            return NotFound();
        }

        _logger.LogInformation($"Returned the list {todoList.Id}.");
        return Ok(_mapper.Map<TodoListDto>(todoList));
    }

    [HttpPost]
    public async Task<IActionResult> CreateTodoListAsync([FromBody] TodoListDto todoListDto, CancellationToken cancellationToken = default)
    {
        if (!ModelState.IsValid)
        {
            _logger.LogError("Received invalid todo list.");
            return BadRequest(ModelState);
        }

        var todoList = await _todoService.CreateTodoListAsync(_mapper.Map<TodoList>(todoListDto), cancellationToken);

        _logger.LogInformation($"Created a new todo list with id {todoList.Id}.");
        return CreatedAtAction(nameof(GetTodoList), new { id = todoList.Id }, _mapper.Map<TodoListDto>(todoList));
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateTodoListAsync(int id, [FromBody] TodoListDto todoListDto, CancellationToken cancellationToken = default)
    {
        if (id != todoListDto.Id)
        {
            return BadRequest();
        }
        if (!ModelState.IsValid)
        {
            _logger.LogError("Received invalid todo list.");
            return BadRequest(ModelState);
        }

        var todoList = await _todoService.UpdateTodoListAsync(_mapper.Map<TodoList>(todoListDto), cancellationToken);
        if (todoList == null)
        {
            return NotFound("No todo list found");
        }

        _logger.LogInformation($"Updated the todo list {id}.");
        return NoContent();
    }

    [HttpPost("{id:int}/items")]
    public async Task<IActionResult> CreateTodoItemAsync(int id, [FromBody] TodoDto todoDto, CancellationToken cancellationToken = default)
    {
        if (!ModelState.IsValid)
        {
            _logger.LogError("Received invalid todo item.");
            return BadRequest(ModelState);
        }

        var todoItem = await _todoService.CreateTodoAsync(id, _mapper.Map<TodoItem>(todoDto), cancellationToken);
        if (todoItem == null)
        {
            return NotFound("No todo list found");
        }

        _logger.LogInformation($"Created a new todo item with id {id}.");
        return CreatedAtAction(
            nameof(TodosController.GetTodo),
            "Todos",
            new { id = todoItem.Id },
            _mapper.Map<TodoDto>(todoItem));
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteTodoListAsync(int id, CancellationToken cancellationToken = default)
    {
        var result = await _todoService.DeleteTodoListAsync(id, cancellationToken);
        if (!result)
        {
            return NotFound("No todo list found");
        }

        _logger.LogInformation($"Removed todo list {id}.");
        return NoContent();
    }
}