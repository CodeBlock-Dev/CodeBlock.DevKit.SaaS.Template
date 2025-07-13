using CodeBlock.DevKit.Contracts.Dtos;

namespace CanBeYours.Application.Dtos;

public class GetDemoThingDto : BaseDto
{
    public string Name { get; set; }
    public string Description { get; set; }
}
