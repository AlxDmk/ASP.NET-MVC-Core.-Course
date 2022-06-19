using ASP.NET_MVC_Core._Course.Models.Data;
using ASP.NET_MVC_Core._Course.Models.Entities;
using ASP.NET_MVC_Core._Course.Models.Repository.IRepositories;

namespace ASP.NET_MVC_Core._Course.Models.Repository;

public class CategoryRepository: ICategoryRepository
{
    private readonly AppDbContext _context;
    private readonly object _locker = new();

    public CategoryRepository(AppDbContext context)
    {
        _context = context;
    }
    public List<Category> GetAll()
    {
        lock(_locker)
        {
            return _context.Categories.ToList();
        }
    } 

    public void Add(Category entity)
    {
        lock (_locker)
        {
            _context.Categories.Add(entity);
            _context.SaveChanges();
        }
    }
}