using AutoMapper;
using Service.Catalog.Application.DTOs;
using Service.Catalog.Domain.Entities;
using Service.Catalog.Domain.ValueObjects;

namespace Service.Catalog.Application;

public class CatalogProfile : Profile
{
    public CatalogProfile()
    {
        // Category mappings
        CreateMap<CategoryDto, Category>()
            .ForMember(dest => dest.Image,
                act => act.MapFrom(src => new ImageInfo(src.ImageUrl, src.ImageAltText)));

        CreateMap<Category, CategoryDto > ()
            .ForMember(dest => dest.ImageUrl,
                act => act.MapFrom(src => src.Image.Url))
            .ForMember(dest => dest.ImageAltText,
                act => act.MapFrom(src => src.Image.AltText));


        // Item mappings
        CreateMap<ProductItemDto, ProductItem>()
            .ForMember(dest => dest.Image,
                act => act.MapFrom(src => new ImageInfo(src.ImageUrl, src.ImageAltText)));

        CreateMap<ProductItem, ProductItemDto>()
            .ForMember(dest => dest.ImageUrl,
                act => act.MapFrom(src => src.Image.Url))
            .ForMember(dest => dest.ImageAltText,
                act => act.MapFrom(src => src.Image.AltText));
    }
}