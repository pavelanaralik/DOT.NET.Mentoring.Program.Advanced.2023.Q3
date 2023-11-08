using Service.Carting.Domain.Entities;

namespace Service.Carting.Domain.DomainValidation;

public static class CartItemValidator
{
    public static void Validate(this CartItem item)
    {
        if (item.Id <= 0)
        {
            throw new ArgumentException("Item ID must be a positive integer.");
        }

        if (string.IsNullOrWhiteSpace(item.Name))
        {
            throw new ArgumentException("Item name cannot be null or empty.");
        }

        if (item.Price <= 0)
        {
            throw new ArgumentException("Price must be a positive value.");
        }

        if (item.Quantity <= 0)
        {
            throw new ArgumentException("Quantity must be a positive integer.");
        }

        if (item.Image != null && string.IsNullOrWhiteSpace(item.Image.Url))
        {
            throw new ArgumentException("Image URL cannot be null or empty if image is provided.");
        }
    }
}