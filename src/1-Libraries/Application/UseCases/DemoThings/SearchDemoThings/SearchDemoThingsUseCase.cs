using AutoMapper;
using CanBeYours.Application.Dtos.DemoThings;
using CanBeYours.Core.Domain.DemoThings;
using CodeBlock.DevKit.Application.Queries;
using CodeBlock.DevKit.Contracts.Dtos;
using CodeBlock.DevKit.Contracts.Services;
using CodeBlock.DevKit.Core.Extensions;
using CodeBlock.DevKit.Core.Helpers;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CanBeYours.Application.UseCases.DemoThings.SearchDemoThings;

/// <summary>
/// Use case for searching DemoThing entities with advanced filtering and pagination.
/// This class demonstrates how to implement search query handlers that include:
/// - Complex search criteria with multiple filters
/// - Pagination and sorting
/// - User email to user ID conversion for enhanced search capabilities
/// - AutoMapper integration for DTO mapping
/// - Enriching results with additional user information
/// The DemoThing functionality shown here is just an example to help you learn how to implement
/// your own unique features into the current codebase.
/// </summary>
internal class SearchDemoThingsUseCase : BaseQueryHandler, IRequestHandler<SearchDemoThingsRequest, SearchOutputDto<GetDemoThingDto>>
{
    private readonly IDemoThingRepository _demoThingRepository;
    private readonly IUserAccessorService _userAccessorService;

    /// <summary>
    /// Initializes a new instance of the SearchDemoThingsUseCase with the required dependencies.
    /// </summary>
    /// <param name="demoThingRepository">The repository for demo thing operations</param>
    /// <param name="mapper">The AutoMapper instance for object mapping</param>
    /// <param name="logger">The logger for this use case</param>
    /// <param name="userAccessorService">The service for accessing user information</param>
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

    /// <summary>
    /// Handles the search for demo things based on various criteria.
    /// This method demonstrates a complete search flow including:
    /// 1. Converting user email search terms to user IDs for enhanced search
    /// 2. Executing the search with multiple filters and pagination
    /// 3. Mapping results to DTOs
    /// 4. Enriching DTOs with user email information
    /// </summary>
    /// <param name="request">The search request containing all search criteria</param>
    /// <param name="cancellationToken">Cancellation token for the operation</param>
    /// <returns>Search results with pagination information and enriched user data</returns>
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

    /// <summary>
    /// Converts a user email search term to a user ID if the term is a valid email address.
    /// This method enhances search capabilities by allowing users to search by email
    /// while maintaining the underlying search by user ID.
    /// </summary>
    /// <param name="term">The search term that might be an email address</param>
    /// <returns>The original term or the converted user ID if the term was an email</returns>
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
