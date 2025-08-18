using CanBeYours.Application.Dtos;
using CanBeYours.Application.UseCases.DemoThings.CreateDemoThing;
using CanBeYours.Application.UseCases.DemoThings.GetDemoThing;
using CanBeYours.Application.UseCases.DemoThings.SearchDemoThings;
using CanBeYours.Application.UseCases.DemoThings.UpdateDemoThing;
using CodeBlock.DevKit.Application.Srvices;
using CodeBlock.DevKit.Contracts.Dtos;
using CodeBlock.DevKit.Core.Helpers;

namespace CanBeYours.Application.Services.DemoThings;

/// <summary>
/// Application service for managing DemoThing entities.
/// This class demonstrates how to implement application services that coordinate between
/// different use cases and provide a clean API for the presentation layer.
/// The DemoThing functionality shown here is just an example to help you learn how to implement
/// your own unique features into the current codebase.
/// </summary>
internal class DemoThingService : ApplicationService, IDemoThingService
{
    /// <summary>
    /// Initializes a new instance of the DemoThingService with the required dependencies.
    /// </summary>
    /// <param name="requestDispatcher">The request dispatcher for sending commands and queries</param>
    public DemoThingService(IRequestDispatcher requestDispatcher)
        : base(requestDispatcher) { }

    /// <summary>
    /// Retrieves a demo thing by its unique identifier.
    /// This method demonstrates how to delegate to use cases using the request dispatcher.
    /// </summary>
    /// <param name="id">The unique identifier of the demo thing</param>
    /// <returns>A result containing the demo thing data or an error</returns>
    public async Task<Result<GetDemoThingDto>> GetDemoThing(string id)
    {
        return await _requestDispatcher.SendQuery(new GetDemoThingRequest(id));
    }

    /// <summary>
    /// Creates a new demo thing with the specified data.
    /// This method demonstrates how to delegate to use cases using the request dispatcher.
    /// </summary>
    /// <param name="input">The data for creating the new demo thing</param>
    /// <returns>A result containing the command execution result or an error</returns>
    public async Task<Result<CommandResult>> CreateDemoThing(CreateDemoThingDto input)
    {
        return await _requestDispatcher.SendCommand(new CreateDemoThingRequest(input.Name, input.Description, input.Type));
    }

    /// <summary>
    /// Updates an existing demo thing with new data.
    /// This method demonstrates how to delegate to use cases using the request dispatcher.
    /// </summary>
    /// <param name="id">The unique identifier of the demo thing to update</param>
    /// <param name="input">The new data for the demo thing</param>
    /// <returns>A result containing the command execution result or an error</returns>
    public async Task<Result<CommandResult>> UpdateDemoThing(string id, UpdateDemoThingDto input)
    {
        return await _requestDispatcher.SendCommand(new UpdateDemoThingRequest(id, input.Name, input.Description, input.Type));
    }

    /// <summary>
    /// Searches for demo things based on specified criteria.
    /// This method demonstrates how to delegate to use cases using the request dispatcher
    /// and map complex input parameters.
    /// </summary>
    /// <param name="input">The search criteria and pagination parameters</param>
    /// <returns>A result containing the search results with pagination information</returns>
    public async Task<Result<SearchOutputDto<GetDemoThingDto>>> SearchDemoThings(SearchDemoThingsInputDto input)
    {
        return await _requestDispatcher.SendQuery(
            new SearchDemoThingsRequest(
                input.Term,
                input.Type,
                input.PageNumber,
                input.RecordsPerPage,
                input.SortOrder,
                input.FromDateTime,
                input.ToDateTime
            )
        );
    }
}
