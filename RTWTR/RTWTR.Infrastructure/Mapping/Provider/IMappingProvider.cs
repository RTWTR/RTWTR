using System.Linq;

namespace RTWTR.Infrastructure.Mapping.Provider
{
    public interface IMappingProvider
    {
        TDestination MapTo<TDestination>(object source);

        IQueryable<TDestination> ProjectTo<TDestination>(IQueryable<object> source);
    }
}
