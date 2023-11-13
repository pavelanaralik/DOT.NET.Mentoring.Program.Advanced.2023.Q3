using Microsoft.AspNetCore.Mvc;
using System.Runtime.Serialization;
using Service.Catalog.Application.DTOs;

namespace Service.Catalog.WebApi.Models;

[DataContract(Name = "Item", Namespace = "")]
[KnownType(typeof(ResourceBase))]
public sealed class ItemResource : ResourceBase
{
    public ItemResource(IUrlHelper urlHelper) : base(urlHelper)
    {
    }

    public ItemResource(IUrlHelper urlHelper, ProductItemDto item) : base(urlHelper)
    {
        if (item is null)
            throw new System.ArgumentNullException(nameof(item));

        Id = item.Id;
        Name = item.Name;
        ImageUrl = item.ImageUrl;
        ImageAltText = item.ImageAltText;
        Description = item.Description;
        Price = item.Price;
        Amount = item.Amount;
        CategoryId = item.CategoryId;
    }

    [DataMember(Order = 1)]
    public int Id { get; set; }

    [DataMember(Order = 2)]
    public string Name { get; set; }

    [DataMember(Order = 3)]
    public string Description { get; set; }

    [DataMember(Order = 4)]
    public string ImageUrl { get; set; }

    [DataMember(Order = 5)]
    public string ImageAltText { get; set; }

    [DataMember(Order = 6)]
    public decimal Price { get; set; }

    [DataMember(Order = 7)]
    public int Amount { get; set; }

    [DataMember(Order = 8)]
    public int CategoryId { get; set; }
}