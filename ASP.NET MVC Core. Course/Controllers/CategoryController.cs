using ASP.NET_MVC_Core._Course.Models.Dtos;
using ASP.NET_MVC_Core._Course.Models.Entities;
using ASP.NET_MVC_Core._Course.Models.Repository.IRepositories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NET_MVC_Core._Course.Controllers;
public class CategoryController: Controller
{
    private readonly IProductRepository _repository;
    private readonly IMapper _mapper;

    public CategoryController(IProductRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    [HttpGet]
    public IActionResult Products()
    {
       var result = _repository.GetAll();

        var response = new AllProductsDto()
        {
            AllProductDtos = new List<ProductDto>()
        };

        foreach (var product in result)
        {
            response.AllProductDtos.Add(_mapper.Map<ProductDto>(product));
        }
        return View(response);
    }
 
    [HttpPost]
    public IActionResult Products([FromForm] ProductDto model)
    {
        _repository.Add(_mapper.Map<Product>(model));
        return RedirectToAction("Products");
    }
    
}