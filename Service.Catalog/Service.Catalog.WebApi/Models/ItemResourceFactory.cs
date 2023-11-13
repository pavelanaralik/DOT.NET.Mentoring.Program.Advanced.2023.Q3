using Microsoft.AspNetCore.Mvc;
using Service.Catalog.Application.DTOs;

namespace Service.Catalog.WebApi.Models;

public sealed class ItemResourceFactory
{
    private readonly IUrlHelper _urlHelper;

    public ItemResourceFactory(IUrlHelper urlHelper)
    {
        _urlHelper = urlHelper ?? throw new ArgumentNullException(nameof(urlHelper));
    }

    public ItemResource CreateItemResource(ProductItemDto item)
    {
        if (item is null)
            throw new ArgumentNullException(nameof(item));

        return (ItemResource)new ItemResource(_urlHelper, item)
            .AddDelete("delete-item", "DeleteItem", new { productId = item.Id })
            .AddGet("items", "GetItems", new PagedQueryParams())
            .AddGet("item", "GetItemById", new { productId = item.Id })
            .AddPost("create-item", "AddItem")
            .AddPut("update-item", "UpdateItem", new { productId = item.Id });
    }

    public ResourceBase CreateItemResourceList(IPagedCollection<ProductItemDto> items, PagedQueryParams query)
    {
        var userResources = items
            .Select(CreateItemResource)
            .ToList();

        var routeName = "GetItems";

        return new ItemResourceList(_urlHelper, userResources)
            .AddCurrentPage(routeName, items, query)
            .AddNextPage(routeName, items, query)
            .AddPreviousPage(routeName, items, query)
            .AddFirstPage(routeName, items, query)
            .AddLastPage(routeName, items, query);
    }
}