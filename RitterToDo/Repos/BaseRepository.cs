using RitterToDo.Core;
using RitterToDo.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RitterToDo.Repos
{
    public class BaseRepository<T> : IRepository<T> where T : class, IOwnedEntity
    {
        public BaseRepository(IIdentityHelper idHelper, IApplicationDbContext dbContext)
        {
            IdHelper = idHelper;
            DbContext = dbContext;
        }

        public virtual IEnumerable<T> GetAll()
        {
            var userId = IdHelper.GetUserId();

            return GetByOwner(userId);
        }

        public IIdentityHelper IdHelper { get; private set; }

        public IApplicationDbContext DbContext { get; private set; }


        public virtual T GetById(Guid id)
        {
            return DbContext.GetById<T>(id);
        }

        public void Update(T entity)
        {
            DbContext.Update(entity);
        }

        public virtual IEnumerable<T> GetByOwner(string userId)
        {
            var q = from t in DbContext.GetEntitySet<T>()
                    where t.OwnerId == userId
                    select t;

            return q;
        }
    }
}
