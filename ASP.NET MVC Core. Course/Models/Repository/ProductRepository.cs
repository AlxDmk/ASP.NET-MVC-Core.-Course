using ASP.NET_MVC_Core._Course.Models.Data;
using ASP.NET_MVC_Core._Course.Models.Entities;
using ASP.NET_MVC_Core._Course.Models.Repository.IRepositories;

namespace ASP.NET_MVC_Core._Course.Models.Repository;

public class ProductRepository: IProductRepository
{
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context)
    {
        _context = context;
    }
    public List<Product> GetAll() => _context.Products.ToList();

    public void Add(Product entity)
    {
        _context.Products.Add(entity);
        _context.SaveChanges();
    }

    public List<Product> GetProductsByCategoryId(int categoryId) =>
        _context.Products.Where(x => x.CategoriID == categoryId).ToList();
    
}