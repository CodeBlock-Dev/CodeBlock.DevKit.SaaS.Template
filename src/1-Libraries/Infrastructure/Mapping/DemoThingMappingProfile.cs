using AutoMapper;
using CanBeYours.Application.Dtos;
using CanBeYours.Core.Domain.DemoThings;

namespace CanBeYours.Infrastructure.Mapping;

internal class DemoThingMappingProfile : Profile
{
    public DemoThingMappingProfile()
    {
        CreateMap<DemoThing, GetDemoThingDto>();
    }
}
