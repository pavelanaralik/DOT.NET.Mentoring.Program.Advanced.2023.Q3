using MongoDB.Driver;
using Service.Catalog.Domain.Entities;

namespace Service.Catalog.Infrastructure.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly IMongoCollection<Category> _categoryCollection;

    public CategoryRepository(string connectionString = "mongodb://127.0.0.1:27017/?compressors=disabled&gssapiServiceName=mongodb", string databaseName = "CategoryDatabase", string collectionName = "Category")
    {
        var client = new MongoClient(connectionString);
        var database = client.GetDatabase(databaseName);
        _categoryCollection = database.GetCollection<Category>(collectionName);
    }

    public Task<Category> GetByIdAsync(int id, CancellationToken cancellationToken = default)       
    {
        return _categoryCollection.Find(category => category.Id == id).FirstOrDefaultAsync(cancellationToken);
    }

    public Task<long> GetCountAsync(CancellationToken cancellationToken = default)
    {
        return _categoryCollection.CountDocumentsAsync(category => true, new CountOptions(), cancellationToken: cancellationToken);
    }

    public Task<List<Category>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return _categoryCollection.Find(category => true).ToListAsync(cancellationToken);
    }

    public Task AddAsync(Category category, CancellationToken cancellationToken = default)
    {
        return _categoryCollection.InsertOneAsync(category, new InsertOneOptions() {}, cancellationToken);
    }

    public Task UpdateAsync(Category category, CancellationToken cancellationToken = default)
    {
        var filter = Builders<Category>.Filter.Eq(e => e.Id, category.Id);
        return _categoryCollection.ReplaceOneAsync(filter, category, cancellationToken: cancellationToken);
    }

    public Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var filter = Builders<Category>.Filter.Eq(e => e.Id, id);
        return _categoryCollection.DeleteOneAsync(filter, cancellationToken);
    }

    public async Task SaveAsync(Category category, CancellationToken cancellationToken = default)
    {
        var existingCategory = await GetByIdAsync(category.Id, cancellationToken);
        if (existingCategory == null)
            await AddAsync(category, cancellationToken);
        else
            await UpdateAsync(category, cancellationToken);
    }
}