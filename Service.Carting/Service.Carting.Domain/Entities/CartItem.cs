using Service.Carting.Domain.ValueObjects;
using System.Xml.Linq;

namespace Service.Carting.Domain.Entities;

public class CartItem : BaseEntity
{
    public string Name { get; private set; }
    public ImageInfo Image { get; private set; }
    public decimal Price { get; private set; }
    public int Quantity { get; private set; }

    public CartItem(int id, string name, decimal price, int quantity, ImageInfo image = null)
    {
        Id = id;
        Name = name;
        Price = price;
        Quantity = quantity;
        Image = image;
    }

    public void UpdateQuantity(int newQuantity)
    {
        if (newQuantity <= 0)
        {
            throw new ArgumentException("Quantity must be a positive integer.");
        }

        Quantity = newQuantity;
    }

    public void UpdatePrice(decimal price)
    {
        if (price < 0)
        {
            throw new ArgumentException("Price must be a positive integer.");
        }

        Price = price;
    }

    public void UpdateName(string name)
    {
        Name = name ?? throw new ArgumentException();
    }
}