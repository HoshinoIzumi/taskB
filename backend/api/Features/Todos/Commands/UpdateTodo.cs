using Api.Data;
using Api.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Api.Features.Todos.Commands;

public record UpdateTodoRequest(
    [Required] Guid Id,
    [Required] string Title,
    string? Description,
    bool Completed,
    Guid? CategoryId
);

public class UpdateTodoHandler
{
    private readonly AppDbContext _context;
    private readonly ILogger<UpdateTodoHandler> _logger;

    public UpdateTodoHandler(AppDbContext context, ILogger<UpdateTodoHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(UpdateTodoRequest request, CancellationToken ct)
    {
        _logger.LogInformation("Updating todo: {Id}", request.Id);

        var todo = await _context.TodoItems.FindAsync(new object[] { request.Id }, ct);

        if (todo == null)
        {
            _logger.LogWarning("Todo {Id} not found for update", request.Id);
            return false;
        }

        todo.Title = request.Title;
        todo.Description = request.Description;
        todo.Completed = request.Completed;
        
        if (request.CategoryId != null && request.CategoryId != Guid.Empty)
        {
            todo.CategoryId = request.CategoryId.Value;
        }
        
        todo.UpdatedAt = DateTime.UtcNow;

        try
        {
            await _context.SaveChangesAsync(ct);
            _logger.LogInformation("Successfully updated todo {Id}", todo.Id);
            return true;
        }
        catch (DbUpdateConcurrencyException)
        {
            _logger.LogError("Concurrency error updating todo {Id}", request.Id);
            throw;
        }
    }
}
