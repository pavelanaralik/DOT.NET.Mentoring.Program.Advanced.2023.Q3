using Service.Catalog.Application.DTOs;

namespace Service.Catalog.Application.Services;

public interface ICartAppService
{
    List<CartItemDto> GetAllItemsFromCart(int cartId);

    void AddItemToCart(int cartId, CartItemDto itemDto);

    void RemoveItemFromCart(int cartId, int itemId);

    void UpdateItemQuantity(int cartId, int itemId, int newQuantity);  

    void ClearCart(int cartId);
}