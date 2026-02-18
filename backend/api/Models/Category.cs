namespace Api.Models;

public class Category
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string Color { get; set; } = string.Empty;

    public ICollection<TodoItem> TodoItems { get; set; } = new List<TodoItem>();
}