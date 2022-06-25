using System.Collections.Concurrent;
using Domain.Entities;
using Domain.Interfaces.IRepositories;

namespace Data.Repository;

public class CategoryRepositoryList:ICategoryRepository
{
    private readonly ConcurrentDictionary<Guid, Category> _categories = new();
    public IReadOnlyCollection<Category> GetAll() => _categories.Values.ToList();
    public void Add(Category entity) => _categories.TryAdd(Guid.NewGuid(), entity);
    public void Remove(Guid id) =>
         _categories.TryRemove(_categories.FirstOrDefault(x => x.Value.Id == id));

}