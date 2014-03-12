using RitterToDo.Core;
using RitterToDo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;

namespace RitterToDo.Repos
{
    public class BaseRepository<T> : IRepository<T> where T : class, IOwnedEntity
    {
        public BaseRepository(IIdentityHelper idHelper)
        {
            this.IdHelper = idHelper;
            this.DbContext = new ApplicationDbContext();
        }

        public IEnumerable<T> GetAll(IPrincipal user)
        {
            var userId = IdHelper.GetUserId(user);

            var q = from t in this.DbContext.GetEntitySet<T>()
                    where t.Owner.Id == userId
                    select t;

            return q;
        }

        public IIdentityHelper IdHelper { get; private set; }

        public IApplicationDbContext DbContext { get; private set; }
    }
}
