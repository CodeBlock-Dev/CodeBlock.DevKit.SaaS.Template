using CanBeYours.Application.Dtos;
using CanBeYours.Application.UseCases.DemoThings.CreateDemoThing;
using CanBeYours.Application.UseCases.DemoThings.GetDemoThing;
using CanBeYours.Application.UseCases.DemoThings.SearchDemoThings;
using CanBeYours.Application.UseCases.DemoThings.UpdateDemoThing;
using CodeBlock.DevKit.Application.Srvices;
using CodeBlock.DevKit.Contracts.Dtos;
using CodeBlock.DevKit.Core.Helpers;

namespace CanBeYours.Application.Services.DemoThings;

internal class DemoThingService : ApplicationService, IDemoThingService
{
    public DemoThingService(IRequestDispatcher requestDispatcher)
        : base(requestDispatcher) { }

    public async Task<Result<GetDemoThingDto>> GetDemoThing(string id)
    {
        return await _requestDispatcher.SendQuery(new GetDemoThingRequest(id));
    }

    public async Task<Result<CommandResult>> CreateDemoThing(CreateDemoThingDto input)
    {
        return await _requestDispatcher.SendCommand(new CreateDemoThingRequest(input.Name, input.Description, input.Type));
    }

    public async Task<Result<CommandResult>> UpdateDemoThing(string id, UpdateDemoThingDto input)
    {
        return await _requestDispatcher.SendCommand(new UpdateDemoThingRequest(id, input.Name, input.Description, input.Type));
    }

    public async Task<Result<SearchOutputDto<GetDemoThingDto>>> SearchDemoThings(SearchDemoThingsInputDto input)
    {
        return await _requestDispatcher.SendQuery(new SearchDemoThingsRequest(input.Term, input.PageNumber, input.RecordsPerPage, input.SortOrder));
    }
}
