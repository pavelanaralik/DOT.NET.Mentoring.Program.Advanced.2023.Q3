using Microsoft.AspNetCore.Mvc;
using Service.Catalog.Application.DTOs;
using Service.Catalog.Application.Services;
using Service.Catalog.WebApi.Models;

namespace Service.Catalog.WebApi.Controllers;

/// <summary>
/// Represents endpoints for working with product items.
/// </summary>
/// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
[ApiController]
[Produces("application/json")]
[Route("api/items")]
public class ItemController : ControllerBase
{
    /// <summary>
    /// The product item service
    /// </summary>
    private readonly IProductItemService _productItemService;

    private readonly ItemResourceFactory _resourceFactory;

    /// <summary>
    /// Initializes a new instance of the <see cref="ItemController"/> class.
    /// </summary>
    /// <param name="productItemService">The product item service.</param>
    /// <param name="resourceFactory">The resource factory.</param>
    public ItemController(IProductItemService productItemService, ItemResourceFactory resourceFactory)
    {
        _productItemService = productItemService;
        _resourceFactory = resourceFactory;
    }

    /// <summary>
    /// Gets the list of Items using filtration by category id and pagination.
    /// </summary>
    /// <param name="categoryId">The category identifier.</param>
    /// <param name="pageNumber">The page number.</param>
    /// <param name="pageSize">Size of the page.</param>
    /// <param name="cancellationToken">The cancellation token.</param> 
    /// <response code="200">The list of the ProductItemDto<see cref="ProductItemDto"/> objects.</response>
    /// <response code="400">Page number must be greater than 0.</response>
    /// <response code="400">Page size must be greater than 0.</response>
    /// <response code="400">Category ID must be greater than 0.</response>
    /// <returns>
    /// The list of the ProductItemDto<see cref="ProductItemDto"/> objects.
    /// </returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> GetItems([FromQuery] int? categoryId, [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10, CancellationToken cancellationToken = default)
    {
        if (pageNumber <= 0)
        {
            return BadRequest("Page number must be greater than 0.");
        }

        if (pageSize <= 0)
        {
            return BadRequest("Page size must be greater than 0.");
        }

        if (categoryId.HasValue && categoryId <= 0)
        {
            return BadRequest("Category ID must be greater than 0.");
        }


        var (results, itemCount) = await _productItemService.GetItemsByCategoryIdAsync(categoryId, pageNumber, pageSize,
            cancellationToken);
        var items = new PagedCollection<ProductItemDto>((IReadOnlyList<ProductItemDto>)results, (int)itemCount, pageNumber, pageSize);
        Response.AddPaginationHeader(items, nameof(GetItems), new PagedQueryParams(), Url);

        return Ok(_resourceFactory.CreateItemResourceList(items, new PagedQueryParams()));
    }

    /// <summary>
    /// Adds the item.
    /// </summary>
    /// <param name="itemDto">The item dto.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The ProductItemDto<see cref="ProductItemDto"/> object.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> AddItem([FromBody] ProductItemDto itemDto, CancellationToken cancellationToken = default)
    {
        await _productItemService.AddItemAsync(itemDto, cancellationToken);
        return CreatedAtAction(nameof(GetItemById), new { productId = itemDto.Id }, itemDto);
    }

    /// <summary>
    /// Gets the item by identifier.
    /// </summary>
    /// <param name="productId">The product identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The ProductItemDto<see cref="ProductItemDto"/> object.</returns>
    [HttpGet("{productId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> GetItemById(int productId, CancellationToken cancellationToken = default)
    {
        var item = await _productItemService.GetItemByIdAsync(productId, cancellationToken);
        if (item == null)
            return NotFound();

        return Ok(item);
    }

    /// <summary>
    /// Updates the item.
    /// </summary>
    /// <param name="productId">The product identifier.</param>
    /// <param name="itemDto">The item dto.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The ProductItemDto<see cref="ProductItemDto"/> object.</returns>
    [HttpPut("{productId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> UpdateItem(int productId, [FromBody] ProductItemDto itemDto, 
        CancellationToken cancellationToken = default)
    {
        if (productId != itemDto.Id)
            return BadRequest();

        await _productItemService.UpdateItemAsync(itemDto);
        return NoContent();
    }

    /// <summary>
    /// Deletes the item.
    /// </summary>
    /// <param name="productId">The product identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    [HttpDelete("{productId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> DeleteItem(int productId, CancellationToken cancellationToken = default)
    {
        await _productItemService.DeleteItemAsync(productId, cancellationToken);
        return NoContent();
    }
}