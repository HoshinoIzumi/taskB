using Api.Data;
using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Features.Todos.Queries;

public class GetTodosHandler
{
    private readonly AppDbContext _context;
    private readonly ILogger<GetTodosHandler> _logger;

    public GetTodosHandler(AppDbContext context, ILogger<GetTodosHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<TodoItem>> Handle(CancellationToken ct)
    {
        _logger.LogInformation("Fetching all todos with categories");
        return await _context.TodoItems
            .Include(t => t.Category)
            .OrderByDescending(t => t.CreatedAt)
            .ToListAsync(ct);
    }
}
