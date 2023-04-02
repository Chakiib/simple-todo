namespace SimpleTodo.Web.Api;

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SimpleTodo.Core.Interfaces;
using SimpleTodo.Core.Models;
using SimpleTodo.Web.ApiModels;

[Route("api/[controller]")]
[ApiController]
public class TodosController : ControllerBase
{
    private readonly ITodoService _todoService;
    private readonly IMapper _mapper;
    private readonly ILogger<TodosController> _logger;

    public TodosController(ITodoService todoService, IMapper mapper, ILogger<TodosController> logger)
    {
        _todoService = todoService;
        _mapper = mapper;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TodoDto>>> GetTodosAsync(CancellationToken cancellationToken = default)
    {
        var todos = await _todoService.GetTodosAsync(cancellationToken);
        return Ok(_mapper.Map<IEnumerable<TodoDto>>(todos));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetTodo(int id, CancellationToken cancellationToken = default)
    {
        var todo = await _todoService.GetTodoAsync(id, cancellationToken);

        if (todo == null)
        {
            return NotFound();
        }

        return Ok(_mapper.Map<TodoDto>(todo));
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateTodoAsync(int id, [FromBody] TodoDto todoDto, CancellationToken cancellationToken = default)
    {
        if (id != todoDto.Id)
        {
            return BadRequest();
        }
        if (!ModelState.IsValid)
        {
            _logger.LogError("Received invalid todo item.");
            return BadRequest(ModelState);
        }

        var todoList = await _todoService.UpdateTodoAsync(_mapper.Map<TodoItem>(todoDto), cancellationToken);
        if (todoList == null)
        {
            return NotFound("No todo item found");
        }

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteTodoAsync(int id, CancellationToken cancellationToken = default)
    {
        var result = await _todoService.DeleteTodoAsync(id, cancellationToken);
        if (!result)
        {
            return NotFound("No todo item found");
        }

        _logger.LogInformation($"Removed todo item with id {id}.");
        return NoContent();
    }
}