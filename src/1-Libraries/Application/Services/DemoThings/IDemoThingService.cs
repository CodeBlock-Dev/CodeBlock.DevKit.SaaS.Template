using CanBeYours.Application.Dtos;
using CodeBlock.DevKit.Contracts.Dtos;
using CodeBlock.DevKit.Core.Helpers;

namespace CanBeYours.Application.Services.DemoThings;

/// <summary>
/// Service interface for managing DemoThing entities.
/// This interface demonstrates how to define service contracts with proper return types
/// and async operations following CQRS patterns.
/// The DemoThing functionality shown here is just an example to help you learn how to implement
/// your own unique features into the current codebase.
/// </summary>
public interface IDemoThingService
{
    /// <summary>
    /// Retrieves a demo thing by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the demo thing</param>
    /// <returns>A result containing the demo thing data or an error</returns>
    Task<Result<GetDemoThingDto>> GetDemoThing(string id);
    
    /// <summary>
    /// Creates a new demo thing with the specified data.
    /// </summary>
    /// <param name="input">The data for creating the new demo thing</param>
    /// <returns>A result containing the command execution result or an error</returns>
    Task<Result<CommandResult>> CreateDemoThing(CreateDemoThingDto input);
    
    /// <summary>
    /// Updates an existing demo thing with new data.
    /// </summary>
    /// <param name="id">The unique identifier of the demo thing to update</param>
    /// <param name="input">The new data for the demo thing</param>
    /// <returns>A result containing the command execution result or an error</returns>
    Task<Result<CommandResult>> UpdateDemoThing(string id, UpdateDemoThingDto input);
    
    /// <summary>
    /// Searches for demo things based on specified criteria.
    /// </summary>
    /// <param name="input">The search criteria and pagination parameters</param>
    /// <returns>A result containing the search results with pagination information</returns>
    Task<Result<SearchOutputDto<GetDemoThingDto>>> SearchDemoThings(SearchDemoThingsInputDto input);
}
