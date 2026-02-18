namespace Api.Models;

public class TodoItem
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool Completed { get; set; } // Renamed from IsCompleted to match frontend
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    public Guid CategoryId { get; set; }
    public Category Category { get; set; } = null!;
}