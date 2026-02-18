using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<TodoItem> TodoItems => Set<TodoItem>();
    public DbSet<Category> Categories => Set<Category>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var workCategoryId = new Guid("210eec23-535a-403a-b400-316651db0402");
        var personalCategoryId = new Guid("51d48e62-31d8-43f0-ab1e-6667ec9ecf73");

        modelBuilder.Entity<Category>().HasData(
            new Category { Id = workCategoryId, Name = "Work", Color = "#007AFF", Description = "Work related tasks" },
            new Category { Id = personalCategoryId, Name = "Personal", Color = "#FF9500", Description = "Personal tasks" }
        );

        modelBuilder.Entity<TodoItem>().HasData(
            new TodoItem { Id = new Guid("aceafa4b-6fcc-4f42-acc1-629731e9bd19"), Title = "Learn .NET 8", Completed = true, CreatedAt = new DateTime(2026, 2, 18, 7, 58, 18, 814, DateTimeKind.Utc).AddTicks(3060), CategoryId = workCategoryId },
            new TodoItem { Id = new Guid("54676881-12fe-467f-87cc-0494e3ade227"), Title = "Learn EF Core", Completed = false, CreatedAt = new DateTime(2026, 2, 18, 7, 58, 18, 814, DateTimeKind.Utc).AddTicks(3070), CategoryId = workCategoryId },
            new TodoItem { Id = new Guid("69b31b6b-669f-45c4-a733-45081743f913"), Title = "Build a Web API", Completed = false, CreatedAt = new DateTime(2026, 2, 18, 7, 58, 18, 814, DateTimeKind.Utc).AddTicks(3070), CategoryId = personalCategoryId }
        );
    }
}