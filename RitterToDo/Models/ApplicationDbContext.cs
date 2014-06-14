using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace RitterToDo.Models
{
    public partial class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
    {
        public DbSet<UserApiKey> UserApiKeys { get; set; }

        public DbSet<ToDo> ToDos { get; set;}

        public DbSet<ToDoCategory> ToDoCategories { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder
                .Properties()
                .Where(p => p.Name == "Id")
                .Configure(pa => pa.HasColumnName(pa.ClrPropertyInfo.DeclaringType.Name + "Id"));
        }

        public IDbSet<T> GetEntitySet<T>() where T : class
        {
            return Set<T>();
        }

        public T GetById<T>(Guid id) where T : class, IEntity
        {
            return Set<T>().Find(id);
        }

        public void Delete<T>(T entity) where T : class, IEntity
        {
            throw new NotImplementedException();
        }

        public void Add<T>(T entity) where T : class, IEntity
        {
            throw new NotImplementedException();
        }

        public void Update<T>(T entity) where T : class, IEntity
        {
            Set<T>().Add(entity);
            Entry(entity).State = EntityState.Modified;
            SaveChanges();
        }

        public IEnumerable<T> GetEntitySet<T>(System.Linq.Expressions.Expression<Func<T, bool>> expr) where T : class, IEntity
        {
            throw new NotImplementedException();
        }
    }
}