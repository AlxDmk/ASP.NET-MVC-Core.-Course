namespace ASP.NET_MVC_Core._Course.Models.Repository.IRepositories;

public interface IRepository<TEntity>
{
    public List<TEntity> GetAll();
    public void Add(TEntity entity);
}