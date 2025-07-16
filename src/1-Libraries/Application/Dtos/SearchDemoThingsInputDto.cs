using CanBeYours.Core.Domain.DemoThings;
using CodeBlock.DevKit.Contracts.Dtos;

namespace CanBeYours.Application.Dtos;

public class SearchDemoThingsInputDto : SearchInputDto
{
    public SearchDemoThingsInputDto()
    {
        RecordsPerPage = 5;
    }

    public DemoThingType? Type { get; set; }
}
