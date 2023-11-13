using AutoMapper;
using Service.Catalog.Application;

namespace Service.Catalog.WebApi.Setup;

public static class AutoMapperConfig
{
    public static IServiceCollection AddAutoMapperProfiles(this IServiceCollection services)
    {
        var mappingConfig = new MapperConfiguration(mc => mc.AddProfile(new CatalogProfile()));

        var mapper = mappingConfig.CreateMapper();
        services.AddSingleton(mapper);

        return services;
    }
}

