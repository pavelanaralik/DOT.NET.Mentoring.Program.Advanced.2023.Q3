﻿using Service.Catalog.Domain.DomainValidation;
using Service.Catalog.Domain.Entities;

namespace Service.Catalog.Domain.Aggregates;

public class Cart
{
    public int Id { get; set; }

    private readonly List<CartItem> _items = new List<CartItem>();

    public IReadOnlyList<CartItem> Items => _items.AsReadOnly();

    public void AddItem(CartItem item)
    {
        item.Validate();
        _items.Add(item);
    }

    public void RemoveItem(int itemId)
    {
        var item = _items.FirstOrDefault(i => i.Id == itemId);
        if (item != null)
            _items.Remove(item);
    }

    public void ClearItems()
    {
        _items.Clear();
    }
}