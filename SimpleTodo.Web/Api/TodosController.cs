namespace SimpleTodo.Web.Api;

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SimpleTodo.Core.Interfaces;
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

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetTodoAsync(int id, CancellationToken cancellationToken = default)
    {
        var todo = await _todoService.GetTodoAsync(id, cancellationToken);

        if (todo == null)
        {
            return NotFound();
        }

        return Ok(_mapper.Map<TodoDto>(todo));
    }
}