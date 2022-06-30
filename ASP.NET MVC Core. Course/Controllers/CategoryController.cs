﻿using ASP.NET_MVC_Core._Course.ViewModels;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Interfaces.IRepositories;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NET_MVC_Core._Course.Controllers;
public class CategoryController: Controller
{
    private readonly IRepository<Product> _repository;
    private readonly IMapper _mapper;
    private readonly IEmailService _emailService;
    private readonly ILogger<CategoryController> _logger;

    public CategoryController(
        IRepository<Product> repository, 
        IMapper mapper, 
        IEmailService emailService, 
        ILogger<CategoryController> logger)
    {
        ArgumentNullException.ThrowIfNull(repository);
        _repository = repository;
        _mapper = mapper;
        _emailService = emailService;
        _logger = logger;
    }
    
    
    [HttpGet]
    public IActionResult Products()
    {
       IReadOnlyCollection<Product> result = _repository.GetAll();

       var response = new AllProductsModel()
        {
            AllProductDtos = new List<ProductModel>()
        };

        foreach (var product in result)
        {
            Console.WriteLine(product.ImageUrl);
            response.AllProductDtos.Add(_mapper.Map<ProductModel>(product));
        }
        return View(response);
    }
 
    [HttpPost]
    public async Task<IActionResult> Products([FromForm] string name, string price, string url)
    {
        ProductModel response = new ProductModel()
        {
            Id = Guid.NewGuid(),
            Name = name,
            Price = price,
            ImageUrl = url,
        };
        var result = _mapper.Map<Product>(response);
        _repository.Add(result);
        await _emailService.SendAsync(new Message() {
                Subject = "Добавление товара в католог",
                Content = $"Товар {result.Id} добавлен в каталог!"
            }
        );

        return RedirectToAction("Products");
    }
    
    [HttpGet("Category/Products/Del/{id}")]
    public IActionResult Delete([FromRoute]Guid id)
    {
        _repository.Remove(id);
        return RedirectToAction(nameof(Products));
    }
}