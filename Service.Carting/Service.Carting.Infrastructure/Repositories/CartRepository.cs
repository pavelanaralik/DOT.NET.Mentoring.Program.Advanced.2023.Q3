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

    public Task<Cart> GetByIdAsync(int id)
    {
        return _cartCollection.Find(cart => cart.Id == id).FirstOrDefaultAsync();
    }

    public Task<List<Cart>> GetAllAsync()
    {
        return _cartCollection.Find(cart => true).ToListAsync();
    }

    public async Task AddAsync(Cart cart)
    {
       await _cartCollection.InsertOneAsync(cart);
    }

    public async Task UpdateAsync(Cart cart)
    {
        var filter = Builders<Cart>.Filter.Eq(e => e.Id, cart.Id);
        await _cartCollection.ReplaceOneAsync(filter, cart);
    }

    public async Task DeleteAsync(Cart cart)
    {
        var filter = Builders<Cart>.Filter.Eq(e => e.Id, cart.Id);
        await _cartCollection.DeleteOneAsync(filter);
    }

    public async Task SaveAsync(Cart cart)
    {
        var existingCart = await GetByIdAsync(cart.Id);
        if (existingCart == null)
        {
           await AddAsync(cart);
        }
        else
        {
           await UpdateAsync(cart);
        }
    }
}