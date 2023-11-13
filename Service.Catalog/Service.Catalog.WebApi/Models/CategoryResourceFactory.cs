using Microsoft.AspNetCore.Mvc;
using Service.Catalog.Application.DTOs;

namespace Service.Catalog.WebApi.Models;

public sealed class CategoryResourceFactory
{
    private readonly IUrlHelper _urlHelper;

    public CategoryResourceFactory(IUrlHelper urlHelper)
    {
        _urlHelper = urlHelper ?? throw new ArgumentNullException(nameof(urlHelper));
    }

    public CategoryResource CreateCategoryResource(CategoryDto category)
    {
        if (category is null)
            throw new ArgumentNullException(nameof(category));

        return (CategoryResource)new CategoryResource(_urlHelper, category)
            .AddDelete("delete-category", "DeleteCategory", new { categoryId = category.Id })
            .AddGet("categories", "GetCategories", new PagedQueryParams())
            .AddGet("category", "GetCategory", new { categoryId = category.Id })
            .AddOptions("GetCategoryOptions")
            .AddPost("create-category", "AddCategory")
            .AddPut("update-category", "UpdateCategory", new { categoryId = category.Id });
    }

    public ResourceBase CreateCategoryResourceList(IPagedCollection<CategoryDto> categories, PagedQueryParams query)
    {
        var userResources = categories
            .Select(CreateCategoryResource)
            .ToList();

        var routeName = "GetCategories";

        return new CategoryResourceList(_urlHelper, userResources)
            .AddCurrentPage(routeName, categories, query)
            .AddNextPage(routeName, categories, query)
            .AddPreviousPage(routeName, categories, query)
            .AddFirstPage(routeName, categories, query)
            .AddLastPage(routeName, categories, query);
    }
}