using ASP.NET_MVC_Core._Course.Models.Entities;
using ASP.NET_MVC_Core._Course.Models.Dtos;
using AutoMapper;

namespace ASP.NET_MVC_Core._Course.Models.Mapping;


public class MapperProfile: Profile
{
    public MapperProfile()
    {
        CreateMap<Category, CategoryDto>();
        CreateMap<CategoryDto, Category>();
        CreateMap<Product, ProductDto>();
        CreateMap<ProductDto, Product>();

    }
}