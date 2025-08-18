using CanBeYours.Application.Dtos;
using CodeBlock.DevKit.Application.Queries;
using CodeBlock.DevKit.Core.Helpers;

namespace CanBeYours.Application.UseCases.DemoThings.GetDemoThing;

/// <summary>
/// Query request for retrieving a DemoThing entity by its identifier.
/// This class demonstrates how to implement query requests that extend base query classes
/// and include optional query options for flexible data retrieval.
/// The DemoThing functionality shown here is just an example to help you learn how to implement
/// your own unique features into the current codebase.
/// </summary>
internal class GetDemoThingRequest : BaseQuery<GetDemoThingDto>
{
    /// <summary>
    /// Initializes a new instance of the GetDemoThingRequest with the required identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the demo thing to retrieve</param>
    /// <param name="options">Optional query options for customizing the retrieval behavior</param>
    public GetDemoThingRequest(string id, QueryOptions options = null)
        : base(options)
    {
        Id = id;
    }

    /// <summary>
    /// The unique identifier of the demo thing to retrieve.
    /// </summary>
    public string Id { get; set; }
}
