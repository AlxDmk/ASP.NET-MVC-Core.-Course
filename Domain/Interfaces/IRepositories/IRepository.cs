using System;
using System.Collections.Generic;
using System.Threading;

namespace Domain.Interfaces.IRepositories;

public interface IRepository<TEntity>
{
    public IReadOnlyCollection<TEntity> GetAll();
    public void Add(TEntity entity, CancellationToken token = default);

    public void Remove(Guid id);
}