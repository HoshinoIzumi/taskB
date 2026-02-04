using System.ComponentModel.DataAnnotations;

namespace TaskB.API.Data;

public class TodoItem
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    
    [Required]
    public string Title { get; set; } = string.Empty;
    
    public string? Description { get; set; }
    public bool Completed { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}
