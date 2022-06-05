using ASP.NET_MVC_Core._Course.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace ASP.NET_MVC_Core._Course.Models.Data;

public class AppDbContext: DbContext
{
    public   DbSet<Category> Categories { get; set; }
    public   DbSet<Product> Products { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }
}