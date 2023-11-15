using Service.Carting.Domain.Aggregates;

namespace Service.Carting.Infrastructure.Repositories;

public interface IRepository<TEntity> where TEntity : class
{
    Task<TEntity> GetByIdAsync(int id);

    Task AddAsync(TEntity entity);

    Task UpdateAsync(TEntity entity);

    Task DeleteAsync(TEntity entity);

    Task<List<TEntity>> GetAllAsync();

    Task SaveAsync(TEntity entity);
}