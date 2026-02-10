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
            new TodoItem { Id = 1, Title = "Learn .NET 8", IsCompleted = true },
            new TodoItem { Id = 2, Title = "Learn EF Core", IsCompleted = false },
            new TodoItem { Id = 3, Title = "Build a Web API", IsCompleted = false }
        );
    }
}