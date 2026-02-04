# Backend Setup Steps (.NET 10)

This guide outlines the steps to initialize and configure the backend for the TaskB project using .NET 10.

## 1. Verify Prerequisites
Ensure you have the .NET 10 SDK installed on your machine.
```bash
dotnet --version
```
If not installed, download it from the official Microsoft .NET website.

## 2. Create the Project Structure
We will create a specific folder structure for the source code to keep things organized.

### Create a Solution File
Initialize a new blank solution in the `backend` directory.
```bash
dotnet new sln -n TaskB
```

### Create the Web API Project
Create a new Web API project named `TaskB.API` inside a `src` folder.
```bash
dotnet new webapi -n TaskB.API -o src/TaskB.API
```

### Add the Project to the Solution
Link the newly created project to your main solution file.
```bash
dotnet sln add src/TaskB.API/TaskB.API.csproj
```

## 3. Verify the Setup
Restore dependencies and build the solution to ensure everything is correct.
```bash
dotnet restore
dotnet build
```

## 4. Running the Application
To start the backend server locally:
```bash
dotnet run --project src/TaskB.API
```
Once running, you can verify the API is working by visiting the Swagger UI (usually at `http://localhost:5000/swagger/index.html` or similar, check the terminal output for the exact URL).

## 5. Recommended Next Steps
1.  **Database Setup**: install Entity Framework Core packages (e.g., `Microsoft.EntityFrameworkCore.SqlServer` or `Npgsql`).
2.  **Architecture**: Create `TaskB.Core` (for domain models) and `TaskB.Infrastructure` (for database contexts) class libraries if you plan on a larger architecture.
3.  **Git Ignore**: Ensure a standard `.gitignore` for dotnet is present.
