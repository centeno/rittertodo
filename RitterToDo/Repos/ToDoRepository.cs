using Moo.Extenders;
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

        public override ToDo GetById(Guid id)
        {
            return base.GetAll().AsQueryable().Include(p => p.Category).FirstOrDefault(p => p.Id == id);
        }

    }
}