using System.ComponentModel.DataAnnotations;

namespace ASP.NET_MVC_Core._Course.ViewModels;

public class ProductModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string ImageUrl { get; set; }
    public string Price { get; set; }
}