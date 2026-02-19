using Api.Data;
using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Features.Categories.Queries;

public class GetCategoriesHandler
{
    private readonly AppDbContext _context;
    private readonly ILogger<GetCategoriesHandler> _logger;

    public GetCategoriesHandler(AppDbContext context, ILogger<GetCategoriesHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<Category>> Handle(CancellationToken ct)
    {
        _logger.LogInformation("Fetching all categories");
        return await _context.Categories.ToListAsync(ct);
    }
}
