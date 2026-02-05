# TaskB - Todo Application

A modern, full-stack Todo application built with .NET 10 and Next.js 15. This project demonstrates a clean architecture approach with a robust backend and a responsive, dynamic frontend.

## 🚀 Tech Stack

### Backend

- **Framework**: .NET 10 Web API
- **Language**: C#
- **Database**: SQL Server (via Entity Framework Core)
- **API Documentation**: OpenAPI / Scalar

### Frontend

- **Framework**: Next.js 15 (App Router)
- **Styling**: Tailwind CSS 4, DaisyUI
- **State Management**: Zustand
- **Data Fetching**: TanStack Query (React Query)
- **Forms**: React Hook Form + Zod
- **Icons**: Lucide React
- **Tooling**: Turbopack, TypeScript, Prettier, ESLint

## 🛠 Prerequisites

Before running the project, ensure you have the following installed:

- **.NET 10 SDK** (Preview)
- **Node.js** (v20 or later recommended)
- **SQL Server** (LocalDB or Docker container)

## 🏁 Getting Started

### 1. Backend Setup

Navigate to the backend directory:

```bash
cd backend
```

Restore dependencies:

```bash
dotnet restore
```

Update the database (ensure your connection string in `appsettings.Development.json` is correct):

```bash
# Apply EF Core migrations if you have the tool installed
dotnet ef database update
```

_Note: If you don't have EF tools, the app might be configured to create the DB on startup or you may need to install them via `dotnet tool install --global dotnet-ef`._

Run the API:

```bash
dotnet run --project src/TaskB.API
# Or use hot reload
dotnet watch run --project src/TaskB.API
```

The API will be available at `http://localhost:5115` (or similar, check output).
Scalar API Reference: `http://localhost:5115/scalar/v1`
OpenAPI definition: `http://localhost:5115/openapi/v1.json`

### 2. Frontend Setup

Navigate to the frontend directory:

```bash
cd frontend
```

Install dependencies:

```bash
npm install
```

Run the development server:

```bash
npm run dev
```

The application will be available at `http://localhost:3000`.

## 📁 Project Structure

```
taskB/
├── backend/            # .NET Web API solution
│   ├── src/
│   │   └── TaskB.API/  # Main API Project
│   └── ...
├── frontend/           # Next.js Application
│   ├── app/            # App Router pages
│   ├── components/     # UI Components
│   ├── lib/            # Utilities (Zustand, API clients)
│   └── ...
└── README.md           # Project Documentation
```

## ✨ Features

- Create, Read, Update, and Delete (CRUD) tasks.
- Filter and sort tasks.
- Responsive UI with dark/light mode support (via DaisyUI).
