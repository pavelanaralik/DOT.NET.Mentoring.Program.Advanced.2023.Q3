using Service.Catalog.DAL.Models;

namespace Service.Catalog.DAL.Repositories;

public interface ICartRepository
{
    public List<CartItem> GetCartItems(int cartId);

    public void AddItemToCart(int cartId, CartItem item);

    public void RemoveItemFromCart(int cartId, int itemId);
}