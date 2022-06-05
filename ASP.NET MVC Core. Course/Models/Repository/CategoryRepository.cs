using ASP.NET_MVC_Core._Course.Models.Data;
using ASP.NET_MVC_Core._Course.Models.Entities;
using ASP.NET_MVC_Core._Course.Models.Repository.IRepositories;

namespace ASP.NET_MVC_Core._Course.Models.Repository;

public class CategoryRepository: ICategoryRepository
{
    private readonly AppDbContext _context;

    public CategoryRepository(AppDbContext context)
    {
        _context = context;
    }
    public List<Category> GetAll() => _context.Categories.ToList();

    public void Add(Category entity)
    {
        _context.Categories.Add(entity);
        _context.SaveChanges();
    }
}