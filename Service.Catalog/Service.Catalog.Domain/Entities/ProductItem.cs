using Service.Catalog.Domain.ValueObjects;

namespace Service.Catalog.Domain.Entities;

public class ProductItem : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public ImageInfo Image { get; set; }
    public int CategoryId { get; set; }
    public decimal Price { get; set; }
    public int Amount { get; set; }
}