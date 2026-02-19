using Api.Data;
using Microsoft.EntityFrameworkCore;

namespace Api.Features.Todos.Commands;

public class DeleteTodoHandler
{
    private readonly AppDbContext _context;
    private readonly ILogger<DeleteTodoHandler> _logger;

    public DeleteTodoHandler(AppDbContext context, ILogger<DeleteTodoHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(Guid id, CancellationToken ct)
    {
        _logger.LogInformation("Deleting todo: {Id}", id);

        var todo = await _context.TodoItems.FindAsync(new object[] { id }, ct);

        if (todo == null)
        {
            _logger.LogWarning("Todo {Id} not found for deletion", id);
            return false;
        }

        _context.TodoItems.Remove(todo);
        await _context.SaveChangesAsync(ct);

        _logger.LogInformation("Successfully deleted todo {Id}", id);
        return true;
    }
}
