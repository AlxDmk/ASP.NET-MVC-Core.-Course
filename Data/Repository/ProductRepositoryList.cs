using System.Collections.Concurrent;
using Domain.Entities;
using Domain.Interfaces.IRepositories;

namespace Data.Repository;

public class ProductRepositoryList: IRepository<Product>
{
    private readonly ConcurrentDictionary<Guid, Product> _products = new();
    public IReadOnlyCollection<Product> GetAll() => _products.Values.ToList();
    public void Add(Product entity) => _products.TryAdd(Guid.NewGuid(), entity);
    public void Remove(Guid id) =>
        _products.TryRemove(_products.FirstOrDefault(x => x.Value.Id == id));

}