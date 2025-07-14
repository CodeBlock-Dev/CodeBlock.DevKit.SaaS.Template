using CodeBlock.DevKit.Contracts.Dtos;

namespace CanBeYours.Application.Dtos;

public class GetDemoThingDto : GetEntityDto
{
    public string Name { get; set; }
    public string Description { get; set; }
}
