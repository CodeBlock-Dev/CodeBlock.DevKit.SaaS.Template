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
        var demoThings = await _demoThingRepository.SearchAsync(
            request.Term,
            request.Type,
            request.PageNumber,
            request.RecordsPerPage,
            request.SortOrder,
            request.FromDateTime,
            request.ToDateTime
        );

        var totalRecords = await _demoThingRepository.CountAsync(request.Term, request.Type, request.FromDateTime, request.ToDateTime);

        var demoThingdto = _mapper.Map<IEnumerable<GetDemoThingDto>>(demoThings);

        //foreach (var demoThing in demoThingdto)
        //{
        //    demoThing.OrdersCount = await _orderAccessorService.PaidOrdersCountByDemoThingId(demoThing.Id);
        //    demoThing.PlansCount = await _planRepository.CountByDemoThingId(demoThing.Id);
        //}

        return new SearchOutputDto<GetDemoThingDto> { TotalRecords = totalRecords, Items = demoThingdto };
    }
}
