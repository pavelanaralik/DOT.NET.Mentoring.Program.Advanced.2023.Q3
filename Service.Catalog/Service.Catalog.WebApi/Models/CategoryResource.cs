using Microsoft.AspNetCore.Mvc;
using System.Runtime.Serialization;
using Service.Catalog.Application.DTOs;

namespace Service.Catalog.WebApi.Models;

[DataContract(Name = "Category", Namespace = "")]
[KnownType(typeof(ResourceBase))]
public sealed class CategoryResource : ResourceBase
{
    public CategoryResource(IUrlHelper urlHelper) : base(urlHelper)
    {
    }

    public CategoryResource(IUrlHelper urlHelper, CategoryDto category) : base(urlHelper)
    {
        if (category is null)
            throw new System.ArgumentNullException(nameof(category));

        Id = category.Id;
        Name = category.Name;
        ImageUrl = category.ImageUrl;
        ImageAltText = category.ImageAltText;
        ParentCategoryId = category.ParentCategoryId;
    }

    [DataMember(Order = 1)]
    public int Id { get; set; }

    [DataMember(Order = 2)]
    public string Name { get; set; }

    [DataMember(Order = 3)]
    public string ImageUrl { get; set; }

    [DataMember(Order = 4)]
    public string ImageAltText { get; set; }

    [DataMember(Order = 5)]
    public int? ParentCategoryId { get; set; }
}