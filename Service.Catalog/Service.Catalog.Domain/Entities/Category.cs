using Service.Catalog.Domain.ValueObjects;

namespace Service.Catalog.Domain.Entities;

public class Category : BaseEntity
{
    public string Name { get; set; }

    public ImageInfo Image { get; set; }

    public int? ParentCategoryId { get; set; }
}