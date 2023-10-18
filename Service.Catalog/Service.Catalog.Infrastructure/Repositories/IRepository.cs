namespace Service.Catalog.Infrastructure.Repositories;

public interface IRepository<TEntity> where TEntity : class
{
    TEntity GetById(int id);

    void Add(TEntity entity);

    void Update(TEntity entity);

    void Delete(TEntity entity);

    IEnumerable<TEntity> GetAll();

    void Save(TEntity entity);
}