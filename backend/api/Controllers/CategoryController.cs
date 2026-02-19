using Api.Features.Categories.Queries;
using Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly GetCategoriesHandler _getCategoriesHandler;

    public CategoryController(GetCategoriesHandler getCategoriesHandler)
    {
        _getCategoriesHandler = getCategoriesHandler;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Category>>> GetCategories(CancellationToken ct)
    {
        var categories = await _getCategoriesHandler.Handle(ct);
        return Ok(categories);
    }
}
