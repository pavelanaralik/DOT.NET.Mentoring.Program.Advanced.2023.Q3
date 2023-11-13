using Service.Catalog.Domain.ValueObjects;

namespace Service.Catalog.Application.DTOs;

public class ProductItemDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
    public string ImageAltText { get; set; }
    public decimal Price { get; set; }
    public int Amount { get; set; }
    public int CategoryId { get; set; }
}