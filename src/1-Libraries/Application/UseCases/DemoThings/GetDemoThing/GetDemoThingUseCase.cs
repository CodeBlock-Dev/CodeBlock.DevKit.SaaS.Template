using AutoMapper;
using CanBeYours.Application.Dtos.DemoThings;
using CanBeYours.Application.Exceptions;
using CanBeYours.Application.Helpers;
using CanBeYours.Core.Domain.DemoThings;
using CodeBlock.DevKit.Application.Queries;
using CodeBlock.DevKit.Application.Srvices;
using CodeBlock.DevKit.Contracts.Services;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CanBeYours.Application.UseCases.DemoThings.GetDemoThing;

/// <summary>
/// Use case for retrieving a DemoThing entity by its identifier.
/// This class demonstrates how to implement query handlers that include:
/// - Permission-based access control
/// - User context validation
/// - AutoMapper integration for DTO mapping
/// - Proper error handling with custom exceptions
/// The DemoThing functionality shown here is just an example to help you learn how to implement
/// your own unique features into the current codebase.
/// </summary>
internal class GetDemoThingUseCase : BaseQueryHandler, IRequestHandler<GetDemoThingRequest, GetDemoThingDto>
{
    private readonly IDemoThingRepository _demoThingRepository;
    private readonly ICurrentUser _currentUser;
    private readonly IUserAccessorService _userAccessorService;

    /// <summary>
    /// Initializes a new instance of the GetDemoThingUseCase with the required dependencies.
    /// </summary>
    /// <param name="demoThingRepository">The repository for demo thing operations</param>
    /// <param name="mapper">The AutoMapper instance for object mapping</param>
    /// <param name="logger">The logger for this use case</param>
    /// <param name="currentUser">The current user context</param>
    /// <param name="userAccessorService">The service for accessing user information</param>
    public GetDemoThingUseCase(
        IDemoThingRepository demoThingRepository,
        IMapper mapper,
        ILogger<GetDemoThingUseCase> logger,
        ICurrentUser currentUser,
        IUserAccessorService userAccessorService
    )
        : base(mapper, logger)
    {
        _demoThingRepository = demoThingRepository;
        _currentUser = currentUser;
        _userAccessorService = userAccessorService;
    }

    /// <summary>
    /// Handles the retrieval of a demo thing by its identifier.
    /// This method demonstrates a complete query flow including:
    /// 1. Entity retrieval from repository
    /// 2. Permission-based access control
    /// 3. DTO mapping with AutoMapper
    /// 4. Enriching DTO with additional user information
    /// </summary>
    /// <param name="request">The query request containing the demo thing identifier</param>
    /// <param name="cancellationToken">Cancellation token for the operation</param>
    /// <returns>The demo thing DTO with enriched user information</returns>
    /// <exception cref="DemoThingApplicationExceptions">Thrown when the demo thing is not found</exception>
    public async Task<GetDemoThingDto> Handle(GetDemoThingRequest request, CancellationToken cancellationToken)
    {
        var demoThing = await _demoThingRepository.GetByIdAsync(request.Id);
        if (demoThing == null)
            throw DemoThingApplicationExceptions.DemoThingNotFound(request.Id);

        // Ensures that the current user has permission to access the specified data
        EnsureUserHasAccess(demoThing.UserId, _currentUser, Permissions.Demo.DEMO_THINGS);

        var demoThingDto = _mapper.Map<GetDemoThingDto>(demoThing);

        // Fetch the email associated with the user Id
        demoThingDto.UserEmail = await _userAccessorService.GetEmailByUserIdIfExists(demoThing.UserId);

        return demoThingDto;
    }
}
