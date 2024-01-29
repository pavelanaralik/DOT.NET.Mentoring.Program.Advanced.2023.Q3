using Microsoft.IdentityModel.Tokens;

namespace Service.Catalog.WebApi.Extensions;

public static class ServiceConfigExtensions
{
    public static void RegisterAuth(this IServiceCollection services)
    {
        services.AddAuthentication("Bearer")
            .AddJwtBearer("Bearer", options =>
            {
                options.Authority = "https://localhost:7254";
                options.RequireHttpsMetadata = false;
                options.Audience = "catalog";
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false,
                };
            });

        // Add Authorization services
        services.AddAuthorization(options =>
        {
            options.AddPolicy("ReadPolicy", policy => policy.RequireRole("Buyer", "Manager"));
            options.AddPolicy("UpdatePolicy", policy => policy.RequireRole("Manager"));
        });
    }
}