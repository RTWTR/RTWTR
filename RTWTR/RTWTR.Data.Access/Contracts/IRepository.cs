using System.Linq;

namespace RTWTR.Data.Access.Contracts
{
    public interface IRepository<T> 
    {

        IQueryable<T> All { get; }

        IQueryable<T> AllAndDeleted { get; }

        void Add(T entity);

        void Delete(T entity);

        void Delete(string id);

        void Update(T entity);
    }
}
