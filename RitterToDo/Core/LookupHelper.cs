using System.Collections.Generic;
using Moo;
using RitterToDo.Models;
using RitterToDo.Repos;

namespace RitterToDo.Core
{
    public class LookupHelper<TEntity, TModel> where TEntity : class, IOwnedEntity
    {
        public IRepository<TEntity> Repo { get; private set; }
        public IMappingRepository MappingRepo { get; private set; }

        public LookupHelper(IRepository<TEntity> repo, IMappingRepository mappingRepo)
        {
            Repo = repo;
            MappingRepo = mappingRepo;
        }

        public IEnumerable<TModel> GetAll()
        {
            var mapper = MappingRepo.ResolveMapper<TEntity, TModel>();
            return mapper.MapMultiple(Repo.GetAll());
        }
    }
}