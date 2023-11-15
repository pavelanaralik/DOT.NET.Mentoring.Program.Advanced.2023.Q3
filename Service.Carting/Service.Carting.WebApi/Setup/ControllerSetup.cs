using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.Versioning.Conventions;

namespace Service.Carting.WebApi.Setup;

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
            .AddXmlDataContractSerializerFormatters();

        services.AddVersionedApiExplorer(o =>
        {
            o.GroupNameFormat = "'v'VVV";
            o.SubstituteApiVersionInUrl = true;
        });

        services.AddApiVersioning(options =>
        {
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.ReportApiVersions = true;
        });

        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "Cart API", Version = "v1" });
            options.SwaggerDoc("v2", new OpenApiInfo { Title = "Cart API", Version = "v2" });
            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
        });

        return services;
    }
}