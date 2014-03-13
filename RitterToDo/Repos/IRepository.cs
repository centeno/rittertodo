using RitterToDo.Models;
using System;
using System.Collections.Generic;
using System.Security.Principal;

namespace RitterToDo.Repos
{
    public interface IRepository<T> where T : class, IOwnedEntity
    {
        IEnumerable<T> GetAll();
    }
}
