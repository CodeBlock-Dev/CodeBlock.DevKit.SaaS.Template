using CanBeYours.Core.Domain.DemoThings;
using CodeBlock.DevKit.Contracts.Dtos;

namespace CanBeYours.Application.Dtos;

public class GetDemoThingDto : GetEntityDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public DemoThingType Type { get; set; }
    public string UserId { get; set; }
}
