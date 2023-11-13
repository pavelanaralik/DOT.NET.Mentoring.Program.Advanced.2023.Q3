using Microsoft.AspNetCore.Mvc;
using Service.Catalog.Application.DTOs;
using Service.Catalog.Application.Services;
using Service.Catalog.WebApi.Models;

namespace Service.Catalog.WebApi.Controllers;

/// <summary>
/// Represents endpoints for working with categories
/// </summary>
/// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
[ApiController]
[Produces("application/json")]
[Route("api/categories")]
public class CategoryController : ControllerBase
{
    /// <summary>
    /// The catalog service
    /// </summary>
    private readonly ICatalogService _catalogService;

    /// <summary>
    /// The resource factory
    /// </summary>
    private readonly CategoryResourceFactory _resourceFactory;

    /// <summary>
    /// Initializes a new instance of the <see cref="CategoryController"/> class.
    /// </summary>
    /// <param name="catalogService">The catalog service.</param>
    /// <param name="resourceFactory">The resource factory.</param>
    public CategoryController(ICatalogService catalogService, CategoryResourceFactory resourceFactory)
    {
        _catalogService = catalogService;
        _resourceFactory = resourceFactory;
    }

    /// <summary>
    /// Returns metadata in the header of the response that describes what other methods
    /// and operations are supported at this URL
    /// </summary>
    /// <returns>Supported methods in header of response</returns>
    [HttpOptions(Name = nameof(GetCategoryOptions))]
    public IActionResult GetCategoryOptions()
    {
        Response.Headers.Add("Allow", "GET,POST,PUT,DELETE");

        return Ok();
    }

    /// <summary>
    /// Gets the categories.
    /// </summary>
    /// <param name="pageNumber">The page number.</param>
    /// <param name="pageSize">Size of the page.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    [HttpGet]
    public async Task<IActionResult> GetCategories([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, CancellationToken cancellationToken = default)
    {
        var (results, itemCount) = await _catalogService.GetAllCategoriesAsync(cancellationToken);
        var categories = new PagedCollection<CategoryDto>((IReadOnlyList<CategoryDto>)results, (int)itemCount, pageNumber, pageSize);
        Response.AddPaginationHeader(categories, nameof(GetCategories), new PagedQueryParams(), Url);

        return Ok(_resourceFactory.CreateCategoryResourceList(categories, new PagedQueryParams()));
    }

    /// <summary>
    /// Adds the category.
    /// </summary>
    /// <param name="categoryDto">The category dto.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> AddCategory([FromBody] CategoryDto categoryDto, CancellationToken cancellationToken = default)
    {
        await _catalogService.AddCategoryAsync(categoryDto, cancellationToken);
        return CreatedAtAction(nameof(GetCategory), new { categoryId = categoryDto.Id },
            _resourceFactory.CreateCategoryResource(categoryDto));
    }

    /// <summary>
    /// Gets the category.
    /// </summary>
    /// <param name="categoryId">The category identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    [HttpGet("{categoryId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> GetCategory(int categoryId, CancellationToken cancellationToken = default)
    {
        var category = await _catalogService.GetCategoryByIdAsync(categoryId, cancellationToken);
        if (category == null)
            return NotFound();

        return Ok(_resourceFactory.CreateCategoryResource(category));
    }

    /// <summary>
    /// Updates the category.
    /// </summary>
    /// <param name="categoryId">The category identifier.</param>
    /// <param name="categoryDto">The category dto.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    [HttpPut("{categoryId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> UpdateCategory(int categoryId, [FromBody] CategoryDto categoryDto, CancellationToken cancellationToken = default)
    {
        if (categoryId != categoryDto.Id)
            return BadRequest();

        await _catalogService.UpdateCategoryAsync(categoryDto, cancellationToken);
        return NoContent();
    }

    /// <summary>
    /// Deletes the category.
    /// </summary>
    /// <param name="categoryId">The category identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    [HttpDelete("{categoryId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> DeleteCategory(int categoryId, CancellationToken cancellationToken = default)
    {
        await _catalogService.DeleteCategoryAsync(categoryId, cancellationToken);
        return NoContent();
    }
}