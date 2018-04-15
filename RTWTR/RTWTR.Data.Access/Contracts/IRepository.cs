using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RTWTR.Data.Access.Contracts
{
    interface IRepository<T> 
    {
        T GetById(string id);

        IQueryable<T> All { get; }

        IQueryable<T> AllAndDeleted { get; }

        void Add(T entity);

        void Delete(T entity);

        void Delete(string id);

        void Update(T entity);

    }
}
