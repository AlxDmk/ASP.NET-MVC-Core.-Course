using System;

namespace Domain.Entities;

public class Product
{   public Guid Id { get; set; }
    
    public string Name { get; set; }
    public string ImageUrl { get; set; }
    
    public decimal Price { get; set; }
}