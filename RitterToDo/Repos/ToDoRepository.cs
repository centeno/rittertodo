using RitterToDo.Core;
using RitterToDo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Data.Entity;

namespace RitterToDo.Repos
{
    public class ToDoRepository : BaseRepository<ToDo>
    {
        public ToDoRepository(IIdentityHelper idHelper, IApplicationDbContext dbContext)
            : base(idHelper, dbContext)
        {
        }

        public override IEnumerable<ToDo> GetAll()
        {
            return base.GetAll().AsQueryable().Include(p => p.Category);
        }
    }
}