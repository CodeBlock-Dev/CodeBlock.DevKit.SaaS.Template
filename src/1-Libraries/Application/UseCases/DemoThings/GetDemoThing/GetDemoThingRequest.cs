using CanBeYours.Application.Dtos;
using CodeBlock.DevKit.Application.Queries;

namespace CanBeYours.Application.UseCases.DemoThings.GetDemoThing;

internal class GetDemoThingRequest : BaseQuery<GetDemoThingDto>
{
    public GetDemoThingRequest(string id)
    {
        Id = id;
    }

    public string Id { get; }
} 