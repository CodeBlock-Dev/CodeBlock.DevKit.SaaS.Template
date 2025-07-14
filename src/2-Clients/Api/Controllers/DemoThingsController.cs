using CanBeYours.Application.Dtos;
using CanBeYours.Application.Services.DemoThings;
using CodeBlock.DevKit.Contracts.Dtos;
using CodeBlock.DevKit.Core.Helpers;
using CodeBlock.DevKit.Web.Api.Filters;
using CodeBlock.DevKit.Web.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CanBeYours.Api.Controllers;

[Tags("DemoThings")]
[Route("demo-things")]
[Authorize(Policies.ADMIN_ROLE)]
public class DemoThingsController : BaseApiController
{
    private readonly IDemoThingService _demoThingService;

    public DemoThingsController(IDemoThingService demoThingService)
    {
        _demoThingService = demoThingService;
    }

    /// <summary>
    /// Get demo thing by id
    /// </summary>
    [HttpGet]
    [Route("{id}")]
    public async Task<Result<GetDemoThingDto>> Get(string id)
    {
        return await _demoThingService.GetDemoThing(id);
    }

    /// <summary>
    /// Create demo thing
    /// </summary>
    [HttpPost]
    public async Task<Result<CommandResult>> Post([FromBody] CreateDemoThingDto input)
    {
        return await _demoThingService.CreateDemoThing(input);
    }

    /// <summary>
    /// Update demo thing
    /// </summary>
    [Route("{id}")]
    [HttpPut]
    public async Task<Result<CommandResult>> Put(string id, [FromBody] UpdateDemoThingDto input)
    {
        return await _demoThingService.UpdateDemoThing(id, input);
    }

    /// <summary>
    /// Search demo things
    /// </summary>
    [HttpGet]
    [Route("{pageNumber}/{recordsPerPage}/{sortOrder}")]
    public async Task<Result<SearchOutputDto<GetDemoThingDto>>> Get(
        int pageNumber,
        int recordsPerPage,
        SortOrder sortOrder,
        [FromQuery] string term = null
    )
    {
        var dto = new SearchDemoThingsInputDto
        {
            Term = term,
            PageNumber = pageNumber,
            RecordsPerPage = recordsPerPage,
            SortOrder = sortOrder,
        };
        return await _demoThingService.SearchDemoThings(dto);
    }
}
