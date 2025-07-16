using AutoMapper;
using CanBeYours.Application.Dtos;
using CanBeYours.Core.Domain.DemoThings;
using CodeBlock.DevKit.Application.Queries;
using CodeBlock.DevKit.Contracts.Dtos;
using CodeBlock.DevKit.Contracts.Services;
using CodeBlock.DevKit.Core.Extensions;
using CodeBlock.DevKit.Core.Helpers;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CanBeYours.Application.UseCases.DemoThings.SearchDemoThings;

internal class SearchDemoThingsUseCase : BaseQueryHandler, IRequestHandler<SearchDemoThingsRequest, SearchOutputDto<GetDemoThingDto>>
{
    private readonly IDemoThingRepository _demoThingRepository;
    private readonly IUserAccessorService _userAccessorService;

    public SearchDemoThingsUseCase(
        IDemoThingRepository demoThingRepository,
        IMapper mapper,
        ILogger<SearchDemoThingsUseCase> logger,
        IUserAccessorService userAccessorService
    )
        : base(mapper, logger)
    {
        _demoThingRepository = demoThingRepository;
        _userAccessorService = userAccessorService;
    }

    public async Task<SearchOutputDto<GetDemoThingDto>> Handle(SearchDemoThingsRequest request, CancellationToken cancellationToken)
    {
        // Replace the searched user email with user Id if the term is a valid email
        // This allows searching by user email as well as user Id
        var term = await ReplaceSearchedUserEmailWithUserId(request.Term);

        var demoThings = await _demoThingRepository.SearchAsync(
            term,
            request.Type,
            request.PageNumber,
            request.RecordsPerPage,
            request.SortOrder,
            request.FromDateTime,
            request.ToDateTime
        );

        var totalRecords = await _demoThingRepository.CountAsync(request.Term, request.Type, request.FromDateTime, request.ToDateTime);

        var demoThingsDto = _mapper.Map<IEnumerable<GetDemoThingDto>>(demoThings);

        // Fetch the email associated with the user Id
        foreach (var demoThingDto in demoThingsDto)
            demoThingDto.UserEmail = await _userAccessorService.GetEmailByUserIdIfExists(demoThingDto.UserId);

        return new SearchOutputDto<GetDemoThingDto> { TotalRecords = totalRecords, Items = demoThingsDto };
    }

    private async Task<string> ReplaceSearchedUserEmailWithUserId(string term)
    {
        if (term.IsNullOrEmptyOrWhiteSpace())
            return string.Empty;

        term = term.Trim();

        if (term.IsValidEmail())
        {
            var userId = await _userAccessorService.GetUserIdByEmailIfExists(term);
            if (!userId.IsNullOrEmptyOrWhiteSpace())
                term = userId;
        }

        return term;
    }
}
