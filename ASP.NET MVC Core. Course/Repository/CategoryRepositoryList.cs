using System.Collections.Concurrent;
using Domain.Entities;
using Domain.Interfaces.IRepositories;
using Serilog;

namespace ASP.NET_MVC_Core._Course.Repository;

public class CategoryRepositoryList:ICategoryRepository
{
    private readonly ConcurrentDictionary<Guid, Category> _categories = new ();
    public IReadOnlyCollection<Category> GetAll() => _categories.Values.ToList();
    public void Add(Category entity)
    {
        try
        {
            _categories.TryAdd(Guid.NewGuid(), entity);
            Log.Warning("Товар {@Entity} попал в базу", entity);

        }
        catch (Exception e)
        {
            Log.Warning(e, "Товар {@Entity} не попал в базу", entity);
        }
        finally
        {
            Log.CloseAndFlush();
        }
        
    }
    public void Remove(Guid id) =>
         _categories.TryRemove(_categories.FirstOrDefault(x => x.Value.Id == id));

}