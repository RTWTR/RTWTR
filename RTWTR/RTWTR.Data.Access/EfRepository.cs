using System;
using System.Linq;
using RTWTR.Data.Access.Contracts;
using RTWTR.Data.Models.Contracts;

namespace RTWTR.Data.Access
{
    public class EfRepository<T> : IRepository<T> where T : class, IDeletable
    {

        public IQueryable<T> All {
            
        }

        public IQueryable<T> AllAndDeleted => throw new NotImplementedException();

        public void Add(T entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(string id)
        {
            throw new NotImplementedException();
        }

        public T GetById(string id)
        {
            throw new NotImplementedException();
        }

        public void Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
