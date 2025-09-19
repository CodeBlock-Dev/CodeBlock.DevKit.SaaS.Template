using CanBeYours.Application.Dtos.DemoThings;
using CanBeYours.Application.Services.DemoThings;
using CanBeYours.Core.Domain.DemoThings;
using CodeBlock.DevKit.Contracts.Dtos;
using CodeBlock.DevKit.Core.Helpers;
using CodeBlock.DevKit.Web.Api.Filters;
using CodeBlock.DevKit.Web.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CanBeYours.Api.Controllers;

/// <summary>
/// API controller for managing DemoThings - serves as an example of how to implement
/// CRUD operations in your own controllers. This controller demonstrates:
/// - Standard REST API patterns (GET, POST, PUT)
/// - Authorization using policies
/// - Input validation and DTOs
/// - Service layer integration
///
/// The current functionality is just for you to learn and understand how to implement
/// your own unique features into the current codebase.
/// </summary>
[Tags("DemoThings")]
[Route("demo-things")]
[Authorize(Policies.ADMIN_ROLE)]
public class DemoThingsController : BaseApiController
{
    private readonly IDemoThingService _demoThingService;

    /// <summary>
    /// Initializes a new instance of the DemoThingsController.
    /// </summary>
    /// <param name="demoThingService">Service for managing demo things</param>
    public DemoThingsController(IDemoThingService demoThingService)
    {
        _demoThingService = demoThingService;
    }

    /// <summary>
    /// Retrieves a demo thing by its unique identifier.
    /// Example: GET /demo-things/123
    /// </summary>
    /// <param name="id">Unique identifier of the demo thing</param>
    /// <returns>Demo thing data wrapped in a Result object</returns>
    [HttpGet]
    [Route("{id}")]
    public async Task<Result<GetDemoThingDto>> Get(string id)
    {
        return await _demoThingService.GetDemoThing(id);
    }

    /// <summary>
    /// Creates a new demo thing.
    /// Example: POST /demo-things with CreateDemoThingDto in body
    /// </summary>
    /// <param name="input">Data for creating the demo thing</param>
    /// <returns>Command result indicating success/failure</returns>
    [HttpPost]
    public async Task<Result<CommandResult>> Post([FromBody] CreateDemoThingDto input)
    {
        return await _demoThingService.CreateDemoThing(input);
    }

    /// <summary>
    /// Updates an existing demo thing by its identifier.
    /// Example: PUT /demo-things/123 with UpdateDemoThingDto in body
    /// </summary>
    /// <param name="id">Unique identifier of the demo thing to update</param>
    /// <param name="input">Updated data for the demo thing</param>
    /// <returns>Command result indicating success/failure</returns>
    [Route("{id}")]
    [HttpPut]
    public async Task<Result<CommandResult>> Put(string id, [FromBody] UpdateDemoThingDto input)
    {
        return await _demoThingService.UpdateDemoThing(id, input);
    }

    /// <summary>
    /// Searches for demo things with pagination, sorting, and filtering options.
    /// Example: GET /demo-things/1/10/asc?term=search&type=Type1&fromDateTime=2024-01-01
    /// </summary>
    /// <param name="pageNumber">Page number for pagination (1-based)</param>
    /// <param name="recordsPerPage">Number of records per page</param>
    /// <param name="sortOrder">Sort order (asc/desc)</param>
    /// <param name="term">Search term for filtering by name/description</param>
    /// <param name="type">Optional filter by demo thing type</param>
    /// <param name="fromDateTime">Optional start date filter</param>
    /// <param name="toDateTime">Optional end date filter</param>
    /// <returns>Paginated search results with demo things</returns>
    [HttpGet]
    [Route("{pageNumber}/{recordsPerPage}/{sortOrder}")]
    public async Task<Result<SearchOutputDto<GetDemoThingDto>>> Get(
        int pageNumber,
        int recordsPerPage,
        SortOrder sortOrder,
        [FromQuery] string term = null,
        [FromQuery] DemoThingType? type = null,
        [FromQuery] DateTime? fromDateTime = null,
        [FromQuery] DateTime? toDateTime = null
    )
    {
        var dto = new SearchDemoThingsInputDto
        {
            Term = term,
            PageNumber = pageNumber,
            RecordsPerPage = recordsPerPage,
            FromDateTime = fromDateTime,
            ToDateTime = toDateTime,
            SortOrder = sortOrder,
            Type = type,
        };
        return await _demoThingService.SearchDemoThings(dto);
    }
}
