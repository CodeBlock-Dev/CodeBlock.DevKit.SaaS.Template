using CanBeYours.Application.Dtos;
using CodeBlock.DevKit.Contracts.Dtos;
using CodeBlock.DevKit.Core.Helpers;

namespace CanBeYours.Application.Services.DemoThings;

public interface IDemoThingService
{
    Task<Result<GetDemoThingDto>> GetDemoThing(string id);
    Task<Result<CommandResult>> CreateDemoThing(CreateDemoThingDto input);
    Task<Result<CommandResult>> UpdateDemoThing(string id, UpdateDemoThingDto input);
    Task<Result<SearchOutputDto<GetDemoThingDto>>> SearchDemoThings(SearchDemoThingsInputDto input);
}
