using AutoMapper;
using CanBeYours.Application.Dtos;
using CanBeYours.Core.Domain.DemoThings;

namespace CanBeYours.Infrastructure.Mapping;

/// <summary>
/// AutoMapper profile for DemoThing entity mapping configurations.
/// This class demonstrates how to set up object-to-object mapping between
/// domain entities and DTOs using AutoMapper.
/// 
/// IMPORTANT: This is an example implementation for learning purposes. Replace
/// DemoThing mappings with your actual business domain entity mappings.
/// 
/// Key features demonstrated:
/// - Domain entity to DTO mapping
/// - AutoMapper profile configuration
/// - Clean separation between domain and presentation layers
/// </summary>
internal class DemoThingMappingProfile : Profile
{
    /// <summary>
    /// Initializes the mapping profile with DemoThing entity mappings.
    /// This constructor sets up the mapping rules between DemoThing domain entities
    /// and their corresponding DTOs.
    /// 
    /// Example: Maps DemoThing entity to GetDemoThingDto for API responses.
    /// </summary>
    public DemoThingMappingProfile()
    {
        CreateMap<DemoThing, GetDemoThingDto>();
    }
}
