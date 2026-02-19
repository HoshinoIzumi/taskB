using Api.Features.Todos.Commands;
using Api.Features.Todos.Queries;
using Api.Features.Categories.Queries;

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
        services.AddScoped<GetCategoriesHandler>();

        return services;
    }
}
