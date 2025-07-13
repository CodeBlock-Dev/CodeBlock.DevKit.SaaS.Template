using CanBeYours.Application.Dtos;
using CanBeYours.Core.Domain.DemoThings;
using CodeBlock.DevKit.Application.Queries;
using CodeBlock.DevKit.Application.Srvices;
using CodeBlock.DevKit.Contracts.Dtos;
using CodeBlock.DevKit.Core.Helpers;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CanBeYours.Application.UseCases.DemoThings.SearchDemoThings;

internal class SearchDemoThingsUseCase : BaseQueryHandler, IRequestHandler<SearchDemoThingsRequest, Result<SearchOutputDto<GetDemoThingDto>>>
{
    private readonly IDemoThingRepository _demoThingRepository;

    public SearchDemoThingsUseCase(IDemoThingRepository demoThingRepository, IRequestDispatcher requestDispatcher, ILogger<SearchDemoThingsUseCase> logger)
        : base(requestDispatcher, logger)
    {
        _demoThingRepository = demoThingRepository;
    }

    public async Task<Result<SearchOutputDto<GetDemoThingDto>>> Handle(SearchDemoThingsRequest request, CancellationToken cancellationToken)
    {
        var count = await _demoThingRepository.CountAsync(request.Term);
        if (count == 0)
            return Result<SearchOutputDto<GetDemoThingDto>>.Success(SearchOutputDto<GetDemoThingDto>.Empty());

        var demoThings = await _demoThingRepository.SearchAsync(request.Term, request.PageNumber, request.RecordsPerPage);
        var dtos = demoThings.Select(MapToDto);

        return Result<SearchOutputDto<GetDemoThingDto>>.Success(
            new SearchOutputDto<GetDemoThingDto>
            {
                Items = dtos,
                TotalCount = count,
                PageNumber = request.PageNumber,
                RecordsPerPage = request.RecordsPerPage
            }
        );
    }

    private static GetDemoThingDto MapToDto(DemoThing demoThing)
    {
        return new GetDemoThingDto
        {
            Id = demoThing.Id,
            Name = demoThing.Name,
            Description = demoThing.Description,
            CreationTime = demoThing.CreationTime
        };
    }
} 