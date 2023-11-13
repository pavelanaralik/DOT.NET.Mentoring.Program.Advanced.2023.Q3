using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc;

namespace Service.Catalog.WebApi.Setup;

internal static class ControllerSetup
{
    internal static IServiceCollection ConfigureControllers(this IServiceCollection services)
    {
        services.AddControllers(options =>
            {
                options.ReturnHttpNotAcceptable = true;
                options.InputFormatters.Add(new XmlSerializerInputFormatter(options));
                options.CacheProfiles.Add("Default10", new CacheProfile() { Duration = 10 }); // 10 second cache
            })
            .AddXmlDataContractSerializerFormatters()
            ;

        return services;
    }
}