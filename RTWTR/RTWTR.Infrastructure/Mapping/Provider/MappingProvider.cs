using AutoMapper;
using AutoMapper.QueryableExtensions;
using System.Linq;

namespace RTWTR.Infrastructure.Mapping.Provider
{
    public class MappingProvider : IMappingProvider
    {
        private IMapper mapper;

        public MappingProvider()
        {
            this.mapper = AutoMapperConfig.Instance;
        }

        public TDestination MapTo<TDestination>(object source)
        {
            return this.mapper.Map<TDestination>(source);
        }

        public IQueryable<TDestination> ProjectTo<TDestination>(IQueryable<object> source)
        {
            return source.ProjectTo<TDestination>();
        }
    }
}
