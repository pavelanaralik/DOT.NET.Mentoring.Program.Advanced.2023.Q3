using Service.Carting.Application.DTOs;
using Service.Carting.Domain.Entities;
using Service.Carting.Domain.ValueObjects;
using Service.Carting.Infrastructure.Repositories;

namespace Service.Carting.Application.Services;

public class CartAppService : ICartAppService
{
    private readonly ICartRepository _cartRepository;

    public CartAppService(ICartRepository cartRepository)
    {
        _cartRepository = cartRepository;
    }

    public async Task<CartDto> GetCartAsync(int cartId)
    {
        var cart = await _cartRepository.GetByIdAsync(cartId);

        if (cart is null) return null;

        return new CartDto
        {
            Id = cart.Id,
            Items = cart.Items.Select(item => new CartItemDto
            {
                Id = item.Id,
                Name = item.Name,
                ImageUrl = item.Image?.Url,
                ImageAltText = item.Image?.AltText,
                Price = item.Price,
                Quantity = item.Quantity
            }).ToList()
        };
    }

    public async Task<List<CartItemDto>> GetAllItemsFromCartAsync(int cartId)
    {
        var cart = await _cartRepository.GetByIdAsync(cartId);

        if (cart is null) return null;

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

    public async Task AddItemToCartAsync(int cartId, CartItemDto itemDto)
    {
        var cart = await _cartRepository.GetByIdAsync(cartId);

        var item = new CartItem(itemDto.Id, itemDto.Name, itemDto.Price, itemDto.Quantity,
            new ImageInfo(itemDto.ImageUrl, itemDto.ImageAltText));

        cart.AddItem(item);

        await _cartRepository.SaveAsync(cart);
    }

    public async Task RemoveItemFromCartAsync(int cartId, int itemId)
    {
        var cart = await _cartRepository.GetByIdAsync(cartId);
        cart.RemoveItem(itemId);
        await _cartRepository.SaveAsync(cart);
    }

    public async Task UpdateItemQuantityAsync(int cartId, int itemId, int newQuantity)
    {
        if (newQuantity <= 0)
        {
            throw new ArgumentException("Quantity must be a positive integer.");
        }

        var cart = await _cartRepository.GetByIdAsync(cartId);
        var item = cart.Items.FirstOrDefault(i => i.Id == itemId);

        if (item == null)
        {
            throw new InvalidOperationException("Item not found in cart.");
        }

        item.UpdateQuantity(newQuantity);
        await _cartRepository.SaveAsync(cart);
    }

    public async Task ClearCartAsync(int cartId)
    {
        var cart = await _cartRepository.GetByIdAsync(cartId);
        cart.ClearItems(); 
        await _cartRepository.SaveAsync(cart);
    }
}