namespace SimpleTodo.Web.Api;

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SimpleTodo.Core.Interfaces;
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

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetTodoListAsync(int id, CancellationToken cancellationToken = default)
    {
        var todoList = await _todoService.GetTodoListAsync(id, cancellationToken);

        if (todoList == null)
        {
            return NotFound();
        }

        return Ok(_mapper.Map<TodoListDto>(todoList));
    }
}