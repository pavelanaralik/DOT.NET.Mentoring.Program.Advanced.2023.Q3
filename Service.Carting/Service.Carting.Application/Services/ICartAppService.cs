using Service.Carting.Application.DTOs;

namespace Service.Carting.Application.Services;

public interface ICartAppService
{
    Task<CartDto> GetCartAsync(int cartId);

    Task<List<CartItemDto>> GetAllItemsFromCartAsync(int cartId);

    Task AddItemToCartAsync(int cartId, CartItemDto itemDto);

    Task RemoveItemFromCartAsync(int cartId, int itemId);

    Task UpdateItemQuantityAsync(int cartId, int itemId, int newQuantity);

    Task ClearCartAsync(int cartId);

    Task UpdateItemToCartsAsync(CartItemDto itemDto);
}