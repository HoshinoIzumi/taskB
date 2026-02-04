using Microsoft.EntityFrameworkCore;

namespace TaskB.API.Data;

public class TaskDbContext : DbContext
{
    public TaskDbContext(DbContextOptions<TaskDbContext> options) : base(options) { }

    public DbSet<TodoItem> TodoItems { get; set; }
}
