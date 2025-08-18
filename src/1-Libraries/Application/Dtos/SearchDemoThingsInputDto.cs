using CanBeYours.Core.Domain.DemoThings;
using CodeBlock.DevKit.Contracts.Dtos;

namespace CanBeYours.Application.Dtos;

/// <summary>
/// Data Transfer Object for search input parameters when searching DemoThing entities.
/// This class demonstrates how to create search DTOs that extend base search DTOs and include
/// custom filtering options with sensible defaults.
/// The DemoThing functionality shown here is just an example to help you learn how to implement
/// your own unique features into the current codebase.
/// </summary>
public class SearchDemoThingsInputDto : SearchInputDto
{
    /// <summary>
    /// Initializes a new instance with a default page size of 5 records per page.
    /// This demonstrates how to set sensible defaults for search parameters.
    /// </summary>
    public SearchDemoThingsInputDto()
    {
        RecordsPerPage = 5;
    }

    /// <summary>
    /// Optional filter by demo thing type. When null, all types are included in the search.
    /// </summary>
    public DemoThingType? Type { get; set; }
}
