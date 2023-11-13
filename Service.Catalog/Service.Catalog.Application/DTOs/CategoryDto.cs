namespace Service.Catalog.Application.DTOs;

public class CategoryDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string ImageUrl { get; set; }
    public string ImageAltText { get; set; }
    public int? ParentCategoryId { get; set; }
}