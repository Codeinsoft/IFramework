using AutoMapper;

using IFramework.Infrastructure.Transversal.Map.Abstract;

namespace IFramework.Infrastructure.Transversal.Mapper.AutoMapper
{
    public class AutoMapperMap : IMap
    {
        private IMapper _mapper;
        public AutoMapperMap(IMapper mapper)
        {
            _mapper = mapper; //new Mapper((IConfigurationProvider)Initialize());
        }


        public T Mapping<T>(object obje)
        {
            return _mapper.Map<T>(obje);
        }
    }
}
