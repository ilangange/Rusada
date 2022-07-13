using AutoMapper;
using Rusada.Aviation.Core.Contracts.Requests;
using Rusada.Aviation.Core.Entities;

namespace Rusada.Aviation.Core.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Sighting, SightingModel>().ReverseMap();
        }
    }
}
