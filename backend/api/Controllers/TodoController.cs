using Api.Features.Todos.Commands;
using Api.Features.Todos.Queries;
using Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TodoController : ControllerBase
{
    private readonly GetTodosHandler _getTodosHandler;
    private readonly GetTodoByIdHandler _getTodoByIdHandler;
    private readonly CreateTodoHandler _createTodoHandler;
    private readonly UpdateTodoHandler _updateTodoHandler;
    private readonly DeleteTodoHandler _deleteTodoHandler;

    public TodoController(
        GetTodosHandler getTodosHandler,
        GetTodoByIdHandler getTodoByIdHandler,
        CreateTodoHandler createTodoHandler,
        UpdateTodoHandler updateTodoHandler,
        DeleteTodoHandler deleteTodoHandler)
    {
        _getTodosHandler = getTodosHandler;
        _getTodoByIdHandler = getTodoByIdHandler;
        _createTodoHandler = createTodoHandler;
        _updateTodoHandler = updateTodoHandler;
        _deleteTodoHandler = deleteTodoHandler;
    }

    // GET: api/Todo
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodos(CancellationToken ct)
    {
        var todos = await _getTodosHandler.Handle(ct);
        return Ok(todos);
    }

    // GET: api/Todo/5
    [HttpGet("{id}")]
    public async Task<ActionResult<TodoItem>> GetTodo(Guid id, CancellationToken ct)
    {
        var todo = await _getTodoByIdHandler.Handle(id, ct);

        if (todo == null)
        {
            return NotFound();
        }

        return Ok(todo);
    }

    // POST: api/Todo
    [HttpPost]
    public async Task<ActionResult<TodoItem>> CreateTodo([FromBody] CreateTodoRequest request, CancellationToken ct)
    {
        var todo = await _createTodoHandler.Handle(request, ct);
        return CreatedAtAction(nameof(GetTodo), new { id = todo.Id }, todo);
    }

    // PUT: api/Todo/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTodo(Guid id, [FromBody] UpdateTodoRequest request, CancellationToken ct)
    {
        if (id != request.Id)
        {
            return BadRequest("ID mismatch");
        }

        var result = await _updateTodoHandler.Handle(request, ct);

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }

    // DELETE: api/Todo/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTodo(Guid id, CancellationToken ct)
    {
        var result = await _deleteTodoHandler.Handle(id, ct);

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
