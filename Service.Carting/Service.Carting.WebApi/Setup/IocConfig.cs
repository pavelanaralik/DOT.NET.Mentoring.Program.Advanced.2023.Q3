using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
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
            .AddSingleton<ICartAppService, CartAppService>()
            .AddSingleton<ICartRepository, CartRepository>();

        services.AddSingleton<MessageListenerService>(provider =>
            new MessageListenerService(ConnectionString, QueueName, provider.GetService<ICartAppService>(), provider.GetService<ILogger<MessageListenerService>>()));

        services.AddHostedService(provider => provider.GetService<MessageListenerService>());

        return services;
    }
}