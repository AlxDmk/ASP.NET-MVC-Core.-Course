using System.Globalization;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.SignalR;

namespace ASP.NET_MVC_Core._Course.ViewModels.Mapping;


public class MapperProfile: Profile
{
  
    public MapperProfile()
    {
        CreateMap<Category, CategoryModel>();
        CreateMap<CategoryModel, Category>();
        CreateMap<Product, ProductModel>()
            .ForMember(dest => dest.Price,
                opt =>
                    opt.MapFrom(src => src.Price.ToString()));
        CreateMap<ProductModel, Product>()
            .ForMember(dest => dest.Price, opt => 
                opt.MapFrom(src => Decimal.Parse(src.Price)));

    }
}