using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<TodoItem> TodoItems => Set<TodoItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TodoItem>().HasData(
            new TodoItem { Id = Guid.NewGuid(), Title = "Learn .NET 8", Completed = true, CreatedAt = DateTime.UtcNow },
            new TodoItem { Id = Guid.NewGuid(), Title = "Learn EF Core", Completed = false, CreatedAt = DateTime.UtcNow },
            new TodoItem { Id = Guid.NewGuid(), Title = "Build a Web API", Completed = false, CreatedAt = DateTime.UtcNow }
        );
    }
}