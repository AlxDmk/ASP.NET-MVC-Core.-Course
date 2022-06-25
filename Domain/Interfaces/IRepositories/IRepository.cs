using System;
using System.Collections.Generic;

namespace Domain.Interfaces.IRepositories;

public interface IRepository<TEntity>
{
    public IReadOnlyCollection<TEntity> GetAll();
    public void Add(TEntity entity);

    public void Remove(Guid id);
}