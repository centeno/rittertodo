using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq.Expressions;
namespace RitterToDo.Models
{
    public interface IApplicationDbContext
    {
        IDbSet<T> GetEntitySet<T>() where T : class;

        T GetById<T>(Guid id) where T : class, IEntity;

        void Delete<T>(T entity) where T : class, IEntity;

        void Add<T>(T entity) where T : class, IEntity;

        void Update<T>(T entity) where T : class, IEntity;

        IEnumerable<T> GetEntitySet<T>(Expression<Func<T, bool>> expr) where T : class, IEntity;

    }
}
