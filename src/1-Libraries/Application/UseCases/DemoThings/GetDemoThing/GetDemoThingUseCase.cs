using AutoMapper;
using CanBeYours.Application.Dtos;
using CanBeYours.Application.Exceptions;
using CanBeYours.Application.Helpers;
using CanBeYours.Core.Domain.DemoThings;
using CodeBlock.DevKit.Application.Queries;
using CodeBlock.DevKit.Application.Srvices;
using CodeBlock.DevKit.Contracts.Services;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CanBeYours.Application.UseCases.DemoThings.GetDemoThing;

internal class GetDemoThingUseCase : BaseQueryHandler, IRequestHandler<GetDemoThingRequest, GetDemoThingDto>
{
    private readonly IDemoThingRepository _demoThingRepository;
    private readonly ICurrentUser _currentUser;
    private readonly IUserAccessorService _userAccessorService;

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
