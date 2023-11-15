﻿using Microsoft.AspNetCore.Mvc;
using Service.Carting.Application.DTOs;
using Service.Carting.Application.Services;

namespace Service.Carting.WebApi.Controllers;

/// <summary>
/// Represents endpoints for working with Cart version 2.
/// </summary>
/// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
[ApiController]
[ApiVersion("2.0")]
[Route("api/v{version:apiVersion}/carts")]
[Produces("application/json")]
public class CartV2Controller : ControllerBase
{
    /// <summary>
    /// The cart service
    /// </summary>
    private readonly ICartAppService _cartService;

    /// <summary>
    /// Initializes a new instance of the <see cref="CartV2Controller"/> class.
    /// </summary>
    /// <param name="cartService">The cart service.</param>
    public CartV2Controller(ICartAppService cartService)
    {
        _cartService = cartService;
    }

    /// <summary>
    /// Gets all items from cart asynchronous.
    /// </summary>
    /// <param name="cartId">The cart identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <response code="200">The list of the CartItemDto<see cref="CartItemDto"/> objects.</response>
    /// <response code="404">Cart with this Id: '<see cref="cartId"/>' was not found.</response>
    /// <response code="500">Unexpected error happens on server.</response>
    /// <returns>
    /// The list of the CartItemDto<see cref="CartItemDto"/> objects.
    /// </returns>
    [HttpGet("{cartId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> GetAllItemsFromCartAsync([FromRoute] int cartId, CancellationToken cancellationToken = default)
    {
        var result = await _cartService.GetAllItemsFromCartAsync(cartId);

        if (result is null) return NotFound($"Cart with this Id: '{cartId}' was not found.");

        return Ok(result);
    }

    /// <summary>
    /// Adds the item to cart asynchronous.
    /// </summary>
    /// <param name="cartId">The cart identifier.</param>
    /// <param name="cartItem">The cart item.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <response code="204">No content.</response>
    /// <response code="500">Unexpected error happens on server.</response>
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesDefaultResponseType]
    [HttpPost("{cartId}/items")]
    public async Task<IActionResult> AddItemToCartAsync([FromRoute] int cartId, [FromBody] CartItemDto cartItem, CancellationToken cancellationToken = default)
    {
        await _cartService.AddItemToCartAsync(cartId, cartItem);
        return NoContent();
    }

    /// <summary>
    /// Deletes the item from cart asynchronous.
    /// </summary>
    /// <param name="cartId">The cart identifier.</param>
    /// <param name="itemId">The item identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <response code="204">No content.</response>
    /// <response code="500">Unexpected error happens on server.</response>
    [HttpDelete("{cartId}/items/{itemId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> DeleteItemFromCartAsync([FromRoute] int cartId, [FromRoute] int itemId, CancellationToken cancellationToken = default)
    {
        await _cartService.RemoveItemFromCartAsync(cartId, itemId);
        return NoContent();
    }
}