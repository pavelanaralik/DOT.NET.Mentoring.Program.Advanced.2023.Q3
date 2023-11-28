using System.Diagnostics.CodeAnalysis;
using Service.Carting.Application.MessagesBrokers;
using Service.Carting.Application.Services;
using Service.Carting.Infrastructure.Repositories;

namespace Service.Carting.WebApi.Setup;

[ExcludeFromCodeCoverage]
public static class IocConfig
{
    private const string QueueName = "catalog";
    private const string ConnectionString = @"";

    public static IServiceCollection AddWebApiIocConfig(this IServiceCollection services)
    {
        services
            .AddScoped<ICartAppService, CartAppService>()
            .AddScoped<ICartRepository, CartRepository>();

        services.AddSingleton<MessageListenerService>(provider =>
            new MessageListenerService(ConnectionString, QueueName, provider.GetService<CartAppService>()));

        services.AddHostedService(provider => provider.GetService<MessageListenerService>());

        return services;
    }
}