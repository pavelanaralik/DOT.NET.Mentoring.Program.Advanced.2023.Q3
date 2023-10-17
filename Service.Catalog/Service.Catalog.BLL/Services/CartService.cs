using Service.Catalog.DAL.Models;
using Service.Catalog.DAL.Repositories;

namespace Service.Catalog.BLL.Services;

internal class CartService
{
    private readonly ICartRepository _repository;

    public CartService(ICartRepository repository)
    {
        _repository = repository;
    }

    public List<CartItem> GetCartItems(int cartId)
    {
        return _repository.GetCartItems(cartId);
    }

    public void AddItemToCart(int cartId, CartItem item)
    {
        _repository.AddItemToCart(cartId, item);
    }

    public void RemoveItemFromCart(int cartId, int itemId)
    {
        _repository.RemoveItemFromCart(cartId, itemId);
    }
}