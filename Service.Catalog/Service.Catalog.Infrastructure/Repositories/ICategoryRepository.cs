using Service.Catalog.Domain.Entities;

namespace Service.Catalog.Infrastructure.Repositories;

public interface ICategoryRepository : IRepository<Category>
{
    Task<long> GetCountAsync(CancellationToken cancellationToken = default);
}