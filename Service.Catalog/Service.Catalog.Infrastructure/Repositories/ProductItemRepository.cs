using MongoDB.Driver;
using Service.Catalog.Domain.Entities;

namespace Service.Catalog.Infrastructure.Repositories;

public class ProductItemRepository : IProductItemRepository
{
    private readonly IMongoCollection<ProductItem> _productItemCollection;

    public ProductItemRepository(string connectionString = "mongodb://127.0.0.1:27017/?compressors=disabled&gssapiServiceName=mongodb", string databaseName = "ProductItemDatabase", string collectionName = "ProductItem")
    {
        var client = new MongoClient(connectionString);
        var database = client.GetDatabase(databaseName);
        _productItemCollection = database.GetCollection<ProductItem>(collectionName);
    }

    public Task<ProductItem> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return _productItemCollection.Find(productItem => productItem.Id == id).FirstOrDefaultAsync();
    }

    public Task<List<ProductItem>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return _productItemCollection.Find(productItem => true).ToListAsync(cancellationToken);
    }

    public Task AddAsync(ProductItem productItem, CancellationToken cancellationToken = default)
    {
        return _productItemCollection.InsertOneAsync(productItem, new InsertOneOptions(), cancellationToken);
    }

    public Task UpdateAsync(ProductItem productItem, CancellationToken cancellationToken = default)
    {
        var filter = Builders<ProductItem>.Filter.Eq(e => e.Id, productItem.Id);
        return _productItemCollection.ReplaceOneAsync(filter, productItem, cancellationToken: cancellationToken);
    }

    public Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var filter = Builders<ProductItem>.Filter.Eq(e => e.Id, id);
        return _productItemCollection.DeleteOneAsync(filter, cancellationToken);
    }

    public async Task SaveAsync(ProductItem productItem, CancellationToken cancellationToken = default)
    {
        var existingProductItem = await GetByIdAsync(productItem.Id, cancellationToken);
        if (existingProductItem == null)
            await AddAsync(productItem, cancellationToken);
        else
            await UpdateAsync(productItem, cancellationToken);
    }

    public Task<List<ProductItem>> GetItemsByCategoryIdAsync(int? categoryId, int pageNumber, int pageSize,
        CancellationToken cancellationToken = default)
    {
        var query = _productItemCollection.Find(productItem => categoryId == null || productItem.CategoryId == categoryId);

        int skip = (pageNumber - 1) * pageSize;

        return query.Skip(skip)
            .Limit(pageSize)
            .ToListAsync(cancellationToken);
    }

    public Task<long> GetCountAsync(int? categoryId, CancellationToken cancellationToken = default)
    {
        return _productItemCollection.CountDocumentsAsync(
            productItem => categoryId == null || productItem.CategoryId == categoryId, new CountOptions(),
            cancellationToken);
    }
}