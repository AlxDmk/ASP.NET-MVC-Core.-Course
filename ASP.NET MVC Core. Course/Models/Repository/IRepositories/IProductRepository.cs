using ASP.NET_MVC_Core._Course.Models.Entities;

namespace ASP.NET_MVC_Core._Course.Models.Repository.IRepositories;

public interface IProductRepository: IRepository<Product>
{
    public List<Product> GetProductsByCategoryId(int categoryId);
}