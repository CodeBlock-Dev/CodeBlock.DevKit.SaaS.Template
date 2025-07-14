using AutoMapper;
using CanBeYours.Application.Dtos;
using CanBeYours.Core.Domain.DemoThings;
using CodeBlock.DevKit.Application.Queries;
using CodeBlock.DevKit.Contracts.Dtos;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CanBeYours.Application.UseCases.DemoThings.SearchDemoThings;

internal class SearchDemoThingsUseCase : BaseQueryHandler, IRequestHandler<SearchDemoThingsRequest, SearchOutputDto<GetDemoThingDto>>
{
    private readonly IDemoThingRepository _demoThingRepository;

    public SearchDemoThingsUseCase(IDemoThingRepository demoThingRepository, IMapper mapper, ILogger<SearchDemoThingsUseCase> logger)
        : base(mapper, logger)
    {
        _demoThingRepository = demoThingRepository;
    }

    public async Task<SearchOutputDto<GetDemoThingDto>> Handle(SearchDemoThingsRequest request, CancellationToken cancellationToken)
    {
        var demoThings = await _demoThingRepository.SearchAsync(request.Term, request.PageNumber, request.RecordsPerPage);
        var totalRecords = await _demoThingRepository.CountAsync(request.Term);

        var demoThingsDto = _mapper.Map<IEnumerable<GetDemoThingDto>>(demoThings);

        //foreach (var dto in demoThingsDto)
        //{
        //
        //}

        return new SearchOutputDto<GetDemoThingDto> { TotalRecords = totalRecords, Items = demoThingsDto };
    }
}
