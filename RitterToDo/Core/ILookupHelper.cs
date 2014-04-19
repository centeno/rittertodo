using System.Collections.Generic;
using RitterToDo.Models;

namespace RitterToDo.Core
{
    public interface ILookupHelper<TEntity, TModel> where TEntity : class, IOwnedEntity
    {
        IEnumerable<TModel> GetAll();
    }
}