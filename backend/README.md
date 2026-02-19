# Backend Creation Guide

## Module 1 - Build Your First Web API with EF Core (Hands-On)

This guide covers building a simple Web API using .NET 8 and Entity Framework Core (EF Core) connected to a SQL Server database.

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (LocalDB, Express, or Docker container)
- [dotnet-ef tool](https://learn.microsoft.com/en-us/ef/core/cli/dotnet) (`dotnet tool install --global dotnet-ef`)

### Step 1: Initialize the Project

Create a new ASP.NET Core Web API project:

```bash
dotnet new webapi --use-controllers -o Api -f net8.0
cd Api
```

### Step 2: Install EF Core Packages

Add the necessary NuGet packages for EF Core and SQL Server support:

```bash
dotnet add package Microsoft.EntityFrameworkCore.SqlServer -v 8.0.12
dotnet add package Microsoft.EntityFrameworkCore.Design -v 8.0.12
dotnet add package Microsoft.EntityFrameworkCore.Tools -v 8.0.12
```

### Step 3: Create the Domain Model

Create a `Models` folder and add a `TodoItem.cs` file:

```csharp
namespace Api.Models;

public class TodoItem
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public bool IsCompleted { get; set; }
}
```

### Step 4: Create the Database Context

Create a `Data` folder and add an `AppDbContext.cs` file:

```csharp
using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<TodoItem> TodoItems => Set<TodoItem>();
}
```

### Step 5: Configure Connection String

Update `appsettings.json` with your SQL Server connection string:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=TodoAppDb;Trusted_Connection=True;MultipleActiveResultSets=true"
  },
  ...
}
```

### Step 6: Register DbContext in Program.cs

Modify `Program.cs` to use the SQL Server provider:

```csharp
using Api.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ... (Rest of the Program.cs)
```

### Step 7: Create and Apply Migrations

Generate the initial database schema and apply it:

```bash
dotnet ef migrations add SeedTodoData
dotnet ef database update
```

### Step 8: Create a Controller

Create a `Controllers/TodoController.cs` to handle API requests using the DbContext.

---

## Module 2 - Database Communication

_Building the official Web API for the TodoApp._

### Step 1: Seed Database with Mock Data

- Implement data seeding in your `DbContext` or a separate `DataInitializer`.
- Use `HasData()` in `OnModelCreating` to add initial Todo items.
- Run migrations to apply seed data.

### Step 2: Create Read Endpoint

- Implement a GET endpoint to fetch todos from the database.
- Verify that it returns the seeded mock data.

### Step 3: Full CRUD Implementation

- Implement the remaining endpoints:
  - **Create**: POST `/api/todo`
  - **Update**: PUT `/api/todo/{id}`
  - **Delete**: DELETE `/api/todo/{id}`

---

## Module 3 - Frontend Communication

Connect your Next.js frontend to your .NET Web API.

### Step 1: Configure CORS in the Backend

CORS (Cross-Origin Resource Sharing) is a security feature that restricts web pages from making requests to a different domain than the one that served the web page.

In `Program.cs`, add the following to allow requests from your frontend:

```csharp
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        builder => builder.WithOrigins("http://localhost:3000")
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});

// ... middle of file ...

app.UseCors("AllowFrontend");
```

### Step 2: Connect Next.js using Fetch

Update your frontend API configuration (e.g., `api.ts`) to point to the .NET backend URL (usually `http://localhost:5133/api`).

```typescript
const baseUrl = "http://localhost:5133/api";

export const getAllTodos = async () => {
  const res = await fetch(`${baseUrl}/Todo`, { cache: "no-store" });
  if (!res.ok) throw new Error("GET /Todo failed");
  return res.json();
};
```

### Step 3: Handle Data Mapping

Ensure your frontend interfaces match the backend models. If your backend uses `Id` (Guid) and `Completed` (bool), map them accordingly in your frontend components.

---

## Module 4 - Domain Relation

Learn how to model a one-to-many relationship using Entity Framework Core.

### Step 1: Add Category Model

Create a new `Category.cs` file in the `Models` folder:

- **Name**: The category title (e.g., "Work", "Personal").
- **Description**: A brief note about the category.
- **Color**: A hex code or color name for visual identification.

### Step 2: Establish One-to-Many Relationship

- Update the `TodoItem` model to include a `CategoryId` (Foreign Key) and a `Category` navigation property.
- Update the `Category` model to include a collection of `TodoItems`.

### Step 3: Seed Categories and Assign to Todos

- Add `DbSet<Category>` to `AppDbContext.cs`.
- Use `OnModelCreating` to seed initial categories.
- Update your existing seeded `TodoItems` to assign them to specific `CategoryId`s.

### Step 4: Run Migrations and Submit PR

Apply the schema changes to your database:

```bash
dotnet ef migrations add AddCategoryToTodo
dotnet ef database update
```

### Step 5: Update API Endpoint

Modify the GET endpoint in `TodoController` to include category data using `.Include(t => t.Category)`.

### Step 6: Frontend Integration

Update your frontend todo list table to include a **Category** column:

- Show the category name.
- Use the category's `Color` property to style the tag.
- Handle cases where a todo has "No category".

---

## Module 5 - CQRS

In this module, you will refactor the Todo App you built in earlier modules to adopt the CQRS (Command Query Responsibility Segregation) pattern.

### Why CQRS?

CQRS separates commands (write operations) from queries (read operations).

Benefits include:

- 🚀 **Performance and scalability** – read and write models can be optimized and scaled independently.
- 🧩 **Clarity and maintainability** – code is easier to understand and evolve since responsibilities are not mixed.
- 🔒 **Security** – commands and queries can enforce different validation and access rules.

### How Our Project Uses CQRS

In this Todo App project, we are applying CQRS by:

- Using **Command Handlers** for all write operations (add, update, delete, mark complete).
- Using **Query Handlers** for all read operations (fetch todos, get details, filter/search).
- Structuring the codebase into clear modules (`commands/`, `queries/`) for better organization.

This approach sets up a strong foundation for future scalability and real-world practices.

### Recommended Code Structure

To keep the project organized, we recommend using a "Feature-based" or "Vertical Slice" structure:

```text
Api/
├── Features/
│   └── Todos/
│       ├── Commands/
│       │   ├── CreateTodo.cs
│       │   ├── UpdateTodo.cs
│       │   └── DeleteTodo.cs
│       └── Queries/
│           ├── GetTodos.cs
│           └── GetTodoById.cs
```

### Example: Create Todo Command

In CQRS, we often group the Request (DTO), the Command, and the Handler in a single file for better discoverability.

**File**: `Features/Todos/Commands/CreateTodo.cs`

```csharp
using Api.Data;
using Api.Models;
using System.ComponentModel.DataAnnotations;

namespace Api.Features.Todos.Commands;

public record CreateTodoRequest(
    [Required] string Title,
    string? Description,
    Guid CategoryId
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

        var todo = new TodoItem
        {
            Id = Guid.NewGuid(),
            Title = request.Title,
            Description = request.Description,
            CategoryId = request.CategoryId,
            Completed = false,
            CreatedAt = DateTime.UtcNow
        };

        _context.TodoItems.Add(todo);
        await _context.SaveChangesAsync(ct);

        _logger.LogInformation("Successfully created todo {Id}", todo.Id);
        return todo;
    }
}
```

### Dependency Injection

Instead of bloating `Program.cs`, create a `DependencyInjection.cs` file in the root of your project to register all your handlers.

**File**: `DependencyInjection.cs`

```csharp
using Api.Features.Todos.Commands;
using Api.Features.Todos.Queries;

namespace Api;

public static class DependencyInjection
{
    public static IServiceCollection AddHandlers(this IServiceCollection services)
    {
        // Commands
        services.AddScoped<CreateTodoHandler>();
        services.AddScoped<UpdateTodoHandler>();
        services.AddScoped<DeleteTodoHandler>();

        // Queries
        services.AddScoped<GetTodosHandler>();
        services.AddScoped<GetTodoByIdHandler>();

        return services;
    }
}
```

Then call it in `Program.cs`: `builder.Services.AddHandlers();`

### Hands On

Refactor your existing Todo App following these steps:

1. **Move write logic into commands**: Extract creating, updating, and deleting logic into dedicated command handlers.
2. **Move read logic into queries**: Extract fetching and filtering logic into dedicated query handlers.
3. **Verify your app**: Use Postman or the frontend to ensure all operations still work as expected.

### CQRS Migration Guide

Watch - https://www.youtube.com/watch?v=pQTHwYMh6CM

🔧 Before (prep work)

- Test current endpoint is still working
- Save a screenshot of the current data return (for comparison after migration)

⏳ In Progress (coding standards)

- Start migrating service methods to CQRS
  Put DTO payload + Command + Handler in one file
  You can use AI to help: paste an existing migrated method as reference

- Add CancellationToken from controller → handler → EF Core calls
- Inject ILogger<THandler> and log at start, key decisions, and success/failure
- Add basic validation with DataAnnotations (e.g., [Required])
- Add try/catch if special handling is required
- Register all new handlers in DependencyInjection.cs

✅ After (wrap-up)

- Test all migrated endpoints
- Test also from frontend (connect to local backend or mobile app)
- Remove old service methods, dependencies, and DTOs

---

## Troubleshooting & Reset

If you encounter errors like `There is already an object named 'TodoItems' in the database`, or if you change your model schema, you can reset your database:

```bash
# ⚠️ This will DELETE the entire database and all data
dotnet ef database drop --force

# Remove and recreate migrations if schema changed significantly
rm -rf Migrations
dotnet ef migrations add InitialCreate

# Apply migrations again to recreate the schema
dotnet ef database update
```
