using RitterToDo.Models;
using System;
using System.Collections.Generic;

namespace RitterToDo.Repos
{
    public interface IRepository<T> where T : class, IOwnedEntity
    {
        IEnumerable<T> GetAll();
        T GetById(Guid id);
        void Update(T entity);
    }
}
