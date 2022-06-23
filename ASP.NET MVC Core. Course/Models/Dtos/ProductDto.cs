namespace ASP.NET_MVC_Core._Course.Models.Dtos;

public class ProductDto
{
    public int Id { get; set; }
    public int CategoriID { get; set; }
    public string Name { get; set; }
    public string ImageUrl { get; set; }
    public float Price { get; set; }
}