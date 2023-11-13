using Service.Catalog.Domain.Entities;

namespace Service.Catalog.Infrastructure.Repositories;

public interface IProductItemRepository : IRepository<ProductItem>
{
    Task<List<ProductItem>> GetItemsByCategoryIdAsync(int? categoryId, int pageNumber, int pageSize,
        CancellationToken cancellationToken = default);

    Task<long> GetCountAsync(int? categoryId, CancellationToken cancellationToken = default);
}