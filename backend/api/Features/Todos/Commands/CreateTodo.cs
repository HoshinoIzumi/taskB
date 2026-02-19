using Api.Data;
using Api.Models;
using System.ComponentModel.DataAnnotations;

namespace Api.Features.Todos.Commands;

public record CreateTodoRequest(
    [Required] string Title,
    string? Description,
    Guid? CategoryId
);

public class CreateTodoHandler
{
    private readonly AppDbContext _context;
    private readonly ILogger<CreateTodoHandler> _logger;

    public CreateTodoHandler(AppDbContext context, ILogger<CreateTodoHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<TodoItem> Handle(CreateTodoRequest request, CancellationToken ct)
    {
        _logger.LogInformation("Creating a new todo: {Title}", request.Title);

        var categoryId = request.CategoryId;
        
        // Fallback to first category if none provided or invalid
        if (categoryId == null || categoryId == Guid.Empty)
        {
            var firstCategory = await Microsoft.EntityFrameworkCore.EntityFrameworkQueryableExtensions.FirstOrDefaultAsync(_context.Categories, ct);
            if (firstCategory != null)
            {
                categoryId = firstCategory.Id;
                _logger.LogInformation("No CategoryId provided, defaulting to category: {CategoryName}", firstCategory.Name);
            }
            else
            {
                throw new Exception("No categories found in database. Please seed categories first.");
            }
        }

        var todo = new TodoItem
        {
            Id = Guid.NewGuid(),
            Title = request.Title,
            Description = request.Description,
            CategoryId = categoryId.Value,
            Completed = false,
            CreatedAt = DateTime.UtcNow
        };

        _context.TodoItems.Add(todo);
        await _context.SaveChangesAsync(ct);

        _logger.LogInformation("Successfully created todo {Id}", todo.Id);
        return todo;
    }
}
