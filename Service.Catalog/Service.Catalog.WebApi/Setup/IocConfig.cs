using Service.Catalog.Application.Services;
using Service.Catalog.Infrastructure.Repositories;
using Service.Catalog.WebApi.Models;

namespace Service.Catalog.WebApi.Setup;

public static class IocConfig
{
    public static IServiceCollection AddWebApiIocConfig(this IServiceCollection services)
    {
        services
            .AddScoped<IProductItemRepository, ProductItemRepository>()
            .AddScoped<ICategoryRepository, CategoryRepository>()
            .AddScoped<ICatalogService, CatalogService>()
            .AddScoped<IProductItemService, ProductItemService>()
            .AddScoped<ItemResourceFactory>()
            .AddScoped<CategoryResourceFactory>();

        return services;
    }
}