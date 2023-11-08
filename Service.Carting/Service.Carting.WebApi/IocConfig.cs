using System.Diagnostics.CodeAnalysis;
using Service.Carting.Application.Services;
using Service.Carting.Infrastructure.Repositories;

namespace Service.Carting.WebApi;

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