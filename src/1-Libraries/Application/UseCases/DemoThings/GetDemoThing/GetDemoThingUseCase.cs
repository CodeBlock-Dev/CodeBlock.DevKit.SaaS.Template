using AutoMapper;
using CanBeYours.Application.Dtos;
using CanBeYours.Application.Exceptions;
using CanBeYours.Application.Helpers;
using CanBeYours.Core.Domain.DemoThings;
using CodeBlock.DevKit.Application.Queries;
using CodeBlock.DevKit.Application.Srvices;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CanBeYours.Application.UseCases.DemoThings.GetDemoThing;

internal class GetDemoThingUseCase : BaseQueryHandler, IRequestHandler<GetDemoThingRequest, GetDemoThingDto>
{
    private readonly IDemoThingRepository _demoThingRepository;
    private readonly ICurrentUser _currentUser;

    public GetDemoThingUseCase(
        IDemoThingRepository demoThingRepository,
        IMapper mapper,
        ILogger<GetDemoThingUseCase> logger,
        ICurrentUser currentUser
    )
        : base(mapper, logger)
    {
        _demoThingRepository = demoThingRepository;
        _currentUser = currentUser;
    }

    public async Task<GetDemoThingDto> Handle(GetDemoThingRequest request, CancellationToken cancellationToken)
    {
        var demoThing = await _demoThingRepository.GetByIdAsync(request.Id);
        if (demoThing == null)
            throw DemoThingApplicationExceptions.DemoThingNotFound(request.Id);

        EnsureUserHasAccess(demoThing.UserId, _currentUser, Permissions.Demo.DEMO_THINGS);

        return _mapper.Map<GetDemoThingDto>(demoThing);
    }
}
