using ASP.NET_MVC_Core._Course.ViewModels;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.IRepositories;
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
       IReadOnlyCollection<Category> categories = _repository.GetAll();

       var response = new AllCategoriesModel()
       {
           CatalogDto = new List<CategoryModel>()
       };

       foreach (var cat in categories)
       {
           response.CatalogDto.Add(_mapper.Map<CategoryModel>(cat));
       }
       return View(response);
    }

    [HttpPost]
    public IActionResult Categories([FromForm] string name)
    {
        CategoryModel model = new() {Id = Guid.NewGuid(), Name = name};
        
        var result = _mapper.Map<Category>(model);
        _repository.Add(result);

        return RedirectToAction(nameof(Categories));
    }

    [HttpGet("Catalog/Categories/Del/{id}")]
    public IActionResult Delete([FromRoute]Guid id)
    {
        _repository.Remove(id);
        return RedirectToAction(nameof(Categories));
    }

    [HttpGet("Catalog/Products")]
    public IActionResult Products()
    {
        return RedirectPermanent("/Category/Products");
    }
}