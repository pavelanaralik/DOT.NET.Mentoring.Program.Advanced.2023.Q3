﻿namespace Service.Catalog.Application.DTOs;

public class CartItemDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string ImageUrl { get; set; }
    public string ImageAltText { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}