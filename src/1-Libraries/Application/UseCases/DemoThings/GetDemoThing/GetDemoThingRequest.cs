using CanBeYours.Application.Dtos;
using CodeBlock.DevKit.Application.Queries;
using CodeBlock.DevKit.Core.Helpers;

namespace CanBeYours.Application.UseCases.DemoThings.GetDemoThing;

internal class GetDemoThingRequest : BaseQuery<GetDemoThingDto>
{
    public GetDemoThingRequest(string id, QueryOptions options = null)
        : base(options)
    {
        Id = id;
    }

    public string Id { get; set; }
}
