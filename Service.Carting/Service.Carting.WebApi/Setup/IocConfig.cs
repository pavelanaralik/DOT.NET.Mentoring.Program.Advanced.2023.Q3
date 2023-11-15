using System.Diagnostics.CodeAnalysis;
using Service.Carting.Application.Services;
using Service.Carting.Infrastructure.Repositories;

namespace Service.Carting.WebApi.Setup;

[ExcludeFromCodeCoverage]
public static class IocConfig
{
    public static IServiceCollection AddWebApiIocConfig(this IServiceCollection services)
    {
        services
             .AddScoped<ICartAppService, CartAppService>()
             .AddScoped<ICartRepository, CartRepository>();

        return services;
    }
}