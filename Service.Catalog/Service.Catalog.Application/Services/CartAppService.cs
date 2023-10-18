using Service.Catalog.Application.DTOs;
using Service.Catalog.Domain.Entities;
using Service.Catalog.Domain.ValueObjects;
using Service.Catalog.Infrastructure.Repositories;

namespace Service.Catalog.Application.Services;

public class CartAppService : ICartAppService
{
    private readonly ICartRepository _cartRepository;

    public CartAppService(ICartRepository cartRepository)
    {
        _cartRepository = cartRepository;
    }

    public List<CartItemDto> GetAllItemsFromCart(int cartId)
    {
        var cart = _cartRepository.GetById(cartId);
        return cart.Items.Select(item => new CartItemDto
        {
            Id = item.Id,
            Name = item.Name,
            ImageUrl = item.Image?.Url,
            ImageAltText = item.Image?.AltText,
            Price = item.Price,
            Quantity = item.Quantity
        }).ToList();
    }

    public void AddItemToCart(int cartId, CartItemDto itemDto)
    {
        var cart = _cartRepository.GetById(cartId);

        var item = new CartItem(itemDto.Id, itemDto.Name, itemDto.Price, itemDto.Quantity,
            new ImageInfo(itemDto.ImageUrl, itemDto.ImageAltText));

        cart.AddItem(item);

        _cartRepository.Save(cart);
    }

    public void RemoveItemFromCart(int cartId, int itemId)
    {
        var cart = _cartRepository.GetById(cartId);
        cart.RemoveItem(itemId);
        _cartRepository.Save(cart);
    }

    public void UpdateItemQuantity(int cartId, int itemId, int newQuantity)
    {
        if (newQuantity <= 0)
        {
            throw new ArgumentException("Quantity must be a positive integer.");
        }

        var cart = _cartRepository.GetById(cartId);
        var item = cart.Items.FirstOrDefault(i => i.Id == itemId);

        if (item == null)
        {
            throw new InvalidOperationException("Item not found in cart.");
        }

        item.UpdateQuantity(newQuantity);
        _cartRepository.Save(cart);
    }

    public void ClearCart(int cartId)
    {
        var cart = _cartRepository.GetById(cartId);
        cart.ClearItems(); 
        _cartRepository.Save(cart);
    }
}