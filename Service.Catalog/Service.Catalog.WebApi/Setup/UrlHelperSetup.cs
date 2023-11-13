using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;

namespace Service.Catalog.WebApi.Setup;

internal static class UrlHelperSetup
{
    internal static IServiceCollection ConfigureUrlHelper(this IServiceCollection services)
    {
        return services.AddSingleton<IActionContextAccessor, ActionContextAccessor>()
            .AddScoped<IUrlHelper>(provider =>
            {
                var actionContext = provider.GetRequiredService<IActionContextAccessor>().ActionContext;
                var factory = provider.GetRequiredService<IUrlHelperFactory>();
                return factory.GetUrlHelper(actionContext);
            });
    }
}