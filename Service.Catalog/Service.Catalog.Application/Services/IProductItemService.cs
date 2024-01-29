using Service.Catalog.Application.DTOs;

namespace Service.Catalog.Application.Services;

public interface IProductItemService
{
    Task<ProductItemDto> GetItemByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<ProductItemDto>> GetAllItemsAsync(CancellationToken cancellationToken = default);
    Task<(IEnumerable<ProductItemDto>, long)> GetItemsByCategoryIdAsync(int? categoryId, int pageNumber, int pageSize, CancellationToken cancellationToken = default);
    Task AddItemAsync(ProductItemDto item, CancellationToken cancellationToken = default);
    Task UpdateItemAsync(ProductItemDto item, string correlationId, CancellationToken cancellationToken = default);
    Task DeleteItemAsync(int id, CancellationToken cancellationToken = default);
}