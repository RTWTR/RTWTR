using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using RTWTR.Data.Access.Contracts;
using RTWTR.Data.Models.Contracts;
using System;
using System.Linq;

namespace RTWTR.Data.Access
{
    public class EfRepository<T> : IRepository<T> where T : class, IDeletable
    {
        private readonly RTWTRDbContext dbContext;
        public EfRepository(RTWTRDbContext dbContext)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public IQueryable<T> All => this.dbContext.Set<T>().Where(x => !x.IsDeleted);

        public IQueryable<T> AllAndDeleted => this.dbContext.Set<T>();

        public void Add(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            EntityEntry entityEntry = this.dbContext.Entry(entity);

            if (entityEntry.State != EntityState.Detached)
            {
                entityEntry.State = EntityState.Added;
            }
            else
            {
                this.dbContext.Set<T>().Add(entity);
            }
        }

        public void Delete(string id)
        {
            T entity = this.dbContext.Set<T>().Find(id);

            this.Delete(entity);
        }

        public void Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            entity.IsDeleted = true;
            entity.DeletedOn = DateTime.Now;

            EntityEntry entityEntry = this.dbContext.Entry(entity);
            entityEntry.State = EntityState.Modified;
        }

        public void Update(T entity)
        {
            EntityEntry entityEntry = this.dbContext.Entry(entity);

            if (entityEntry.State == EntityState.Detached)
            {
                this.dbContext.Set<T>().Attach(entity);
            }

            entityEntry.State = EntityState.Modified;
        }
    }
}
