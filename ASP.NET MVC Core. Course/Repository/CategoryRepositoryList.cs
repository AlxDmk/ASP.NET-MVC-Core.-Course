using System.Collections.Concurrent;
using Domain.Entities;
using Domain.Interfaces.IRepositories;
using Serilog;

namespace ASP.NET_MVC_Core._Course.Repository;

public class CategoryRepositoryList:ICategoryRepository
{
    private readonly ConcurrentDictionary<Guid, Category> _categories = new ();
    public IReadOnlyCollection<Category> GetAll() => _categories.Values.ToList();
    public void Add(Category entity, CancellationToken token = default)
    {
        try
        {
            token.ThrowIfCancellationRequested() ;
            _categories.TryAdd(Guid.NewGuid(), entity);
            Log.Warning("Категория {@Entity} создана", entity);

        }
        catch (Exception e)
        {
            Log.Warning(e, "Категория  {@Entity} не создана", entity);
        }
        finally
        {
            Log.CloseAndFlush();
        }
        
    }

    public Task AddAsync(Category entity, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }

    public void Remove(Guid id) =>
         _categories.TryRemove(_categories.FirstOrDefault(x => x.Value.Id == id));

}