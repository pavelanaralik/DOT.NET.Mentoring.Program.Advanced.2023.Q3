using System.Diagnostics.CodeAnalysis;
using Service.Catalog.Application.Services;
using Service.Catalog.Infrastructure.Repositories;

namespace Service.Catalog.WebApi;

[ExcludeFromCodeCoverage]
public static class IocConfig
{
    public static void AddWebApiIocConfig(this WebApplicationBuilder builder)
    {

        var services = builder.Services;

        services
             .AddScoped<ICartAppService, CartAppService>()
             .AddScoped<ICartRepository, CartRepository>();
    }
}