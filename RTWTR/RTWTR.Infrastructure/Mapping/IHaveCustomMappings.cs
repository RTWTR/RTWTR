using AutoMapper;

namespace RTWTR.Infrastructure.Mapping
{
    public interface IHaveCustomMappings
    {
        void CreateMappings(IMapperConfigurationExpression configuration);
    }
}
