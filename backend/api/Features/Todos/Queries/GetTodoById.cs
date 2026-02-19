using Api.Data;
using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Features.Todos.Queries;

public class GetTodoByIdHandler
{
    private readonly AppDbContext _context;
    private readonly ILogger<GetTodoByIdHandler> _logger;

    public GetTodoByIdHandler(AppDbContext context, ILogger<GetTodoByIdHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<TodoItem?> Handle(Guid id, CancellationToken ct)
    {
        _logger.LogInformation("Fetching todo with ID: {Id}", id);
        return await _context.TodoItems
            .Include(t => t.Category)
            .FirstOrDefaultAsync(t => t.Id == id, ct);
    }
}
