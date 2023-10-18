using MongoDB.Driver;
using Service.Catalog.DAL.Models;

namespace Service.Catalog.DAL.Repositories;

public class CartRepository : ICartRepository
{
    private readonly IMongoCollection<CartItem> _cartItems;

    public CartRepository(string connectionString = "mongodb://127.0.0.1:27017/?compressors=disabled&gssapiServiceName=mongodb", 
        string databaseName = "CartDatabase", 
        string collectionName = "CartItems")
    {
        var client = new MongoClient(connectionString);
        var database = client.GetDatabase(databaseName);
        _cartItems = database.GetCollection<CartItem>(collectionName);
    }

    public List<CartItem> GetCartItems(int cartId)
    {
        return _cartItems.Find(item => item.Id == cartId).ToList();
    }

    public void AddItemToCart(int cartId, CartItem item)
    {
        item.Id = cartId; 
        _cartItems.InsertOne(item);
    }

    public void RemoveItemFromCart(int cartId, int itemId)
    {
        var filter = Builders<CartItem>.Filter.Eq("CartId", cartId) & Builders<CartItem>.Filter.Eq("Id", itemId);
        _cartItems.DeleteOne(filter);
    }
}