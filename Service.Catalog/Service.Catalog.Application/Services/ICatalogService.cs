using Service.Catalog.Application.DTOs;

namespace Service.Catalog.Application.Services;

public interface ICatalogService
{
    Task<CategoryDto> GetCategoryByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<(IEnumerable<CategoryDto>, long)> GetAllCategoriesAsync(CancellationToken cancellationToken = default);
    Task AddCategoryAsync(CategoryDto category, CancellationToken cancellationToken = default);
    Task UpdateCategoryAsync(CategoryDto category, CancellationToken cancellationToken = default);
    Task DeleteCategoryAsync(int id, CancellationToken cancellationToken = default);
}