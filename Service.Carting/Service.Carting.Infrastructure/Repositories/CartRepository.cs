using MongoDB.Driver;
using Service.Carting.Domain.Aggregates;

namespace Service.Carting.Infrastructure.Repositories;

public class CartRepository : ICartRepository
{
    private readonly IMongoCollection<Cart> _cartCollection;

    public CartRepository(string connectionString = "mongodb://127.0.0.1:27017/?compressors=disabled&gssapiServiceName=mongodb", string databaseName = "CartDatabase", string collectionName = "Carts")
    {
        var client = new MongoClient(connectionString);
        var database = client.GetDatabase(databaseName);
        _cartCollection = database.GetCollection<Cart>(collectionName);
    }

    public Cart GetById(int id)
    {
        return _cartCollection.Find(cart => cart.Id == id).FirstOrDefault();
    }

    public IEnumerable<Cart> GetAll()
    {
        return _cartCollection.Find(cart => true).ToList();
    }

    public void Add(Cart cart)
    {
        _cartCollection.InsertOne(cart);
    }

    public void Update(Cart cart)
    {
        var filter = Builders<Cart>.Filter.Eq(e => e.Id, cart.Id);
        _cartCollection.ReplaceOne(filter, cart);
    }

    public void Delete(Cart cart)
    {
        var filter = Builders<Cart>.Filter.Eq(e => e.Id, cart.Id);
        _cartCollection.DeleteOne(filter);
    }

    public void Save(Cart cart)
    {
        var existingCart = GetById(cart.Id);
        if (existingCart == null)
        {
            Add(cart);
        }
        else
        {
            Update(cart);
        }
    }
}