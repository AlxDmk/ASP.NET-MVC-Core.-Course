using System.Collections.Concurrent;
using ASP.NET_MVC_Core._Course.ViewModels;
using Domain.Entities;
using Domain.Interfaces.IRepositories;

namespace ASP.NET_MVC_Core._Course.Repository;

public class ProductRepositoryList: IRepository<Product>
{
    private readonly ConcurrentDictionary<Guid, Product> _products = new();
    private readonly ILogger<ProductRepositoryList> _logger;

    public ProductRepositoryList(ILogger<ProductRepositoryList> logger)
    {
        _logger = logger;
    }
    
    public IReadOnlyCollection<Product> GetAll() => _products.Values.ToList();
    public void Add(Product entity, CancellationToken token = default)
    {
        try
        {
            token.ThrowIfCancellationRequested();
            _products.TryAdd(Guid.NewGuid(), entity);
            _logger.LogWarning("Товар {@Entity} добавлен", entity);

        }
        catch(Exception e)
        {
            _logger.LogError(e, "Ошибка добавления товара {@Entity}", entity);
        }
    }
    public void Remove(Guid id) =>
        _products.TryRemove(_products.FirstOrDefault(x => x.Value.Id == id));
}