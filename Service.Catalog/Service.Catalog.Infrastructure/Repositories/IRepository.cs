using Service.Catalog.Domain.Entities;

namespace Service.Catalog.Infrastructure.Repositories;

public interface IRepository<TEntity> where TEntity : class
{
    Task<TEntity> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);

    Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

    Task DeleteAsync(int id, CancellationToken cancellationToken = default);

    Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);

    Task SaveAsync(TEntity entity, CancellationToken cancellationToken = default);
}