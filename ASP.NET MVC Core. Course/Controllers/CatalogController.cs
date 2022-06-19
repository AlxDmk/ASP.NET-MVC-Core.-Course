
using ASP.NET_MVC_Core._Course.Models.Dtos;
using ASP.NET_MVC_Core._Course.Models.Entities;
using ASP.NET_MVC_Core._Course.Models.Repository.IRepositories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NET_MVC_Core._Course.Controllers;

public class CatalogController : Controller
{
    
    private readonly IMapper _mapper;
    private readonly IRepository<Category> _repository;

    public CatalogController(IRepository<Category> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    [HttpGet]
    public IActionResult Categories()
    {
       List<Category> categories = _repository.GetAll();

       var response = new AllCategoriesDto()
       {
           CatalogDto = new List<CategoryDto>()
       };

       foreach (var cat in categories)
       {
           response.CatalogDto.Add(_mapper.Map<CategoryDto>(cat));
       }
       return View(response);
    }

    [HttpPost]
    public IActionResult Categories([FromForm] CategoryDto model)
    {
        var result = _mapper.Map<Category>(model);
        _repository.Add(result);

        return RedirectToAction(nameof(Categories));
    }

    [HttpGet("Catalog/Products")]
    public IActionResult Products()
    {
        return RedirectPermanent("/Category/Products");
    }
}