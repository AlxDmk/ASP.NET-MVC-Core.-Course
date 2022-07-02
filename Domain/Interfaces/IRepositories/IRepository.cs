using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Interfaces.IRepositories;

public interface IRepository<TEntity>
{
    IReadOnlyCollection<TEntity> GetAll();
    void Add(TEntity entity, CancellationToken token = default);
    Task AddAsync(TEntity entity, CancellationToken token = default);
    public void Remove(Guid id);
}