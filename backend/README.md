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
  - **Create**: POST `/api/todoitems`
  - **Update**: PUT `/api/todoitems/{id}`
  - **Delete**: DELETE `/api/todoitems/{id}`

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
