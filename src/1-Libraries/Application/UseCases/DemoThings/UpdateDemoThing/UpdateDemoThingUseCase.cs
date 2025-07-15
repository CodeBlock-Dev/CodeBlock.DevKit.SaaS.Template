using CanBeYours.Application.Exceptions;
using CanBeYours.Core.Domain.DemoThings;
using CodeBlock.DevKit.Application.Commands;
using CodeBlock.DevKit.Application.Srvices;
using CodeBlock.DevKit.Core.Helpers;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CanBeYours.Application.UseCases.DemoThings.UpdateDemoThing;

internal class UpdateDemoThingUseCase : BaseCommandHandler, IRequestHandler<UpdateDemoThingRequest, CommandResult>
{
    private readonly IDemoThingRepository _demoThingRepository;

    public UpdateDemoThingUseCase(
        IDemoThingRepository demoThingRepository,
        IRequestDispatcher requestDispatcher,
        ILogger<UpdateDemoThingUseCase> logger
    )
        : base(requestDispatcher, logger)
    {
        _demoThingRepository = demoThingRepository;
    }

    public async Task<CommandResult> Handle(UpdateDemoThingRequest request, CancellationToken cancellationToken)
    {
        var demoThing = await _demoThingRepository.GetByIdAsync(request.Id);
        if (demoThing == null)
            throw DemoThingApplicationExceptions.DemoThingNotFound(request.Id);

        var loadedVersion = demoThing.Version;

        demoThing.Update(request.Name, request.Description, request.Type);

        await _demoThingRepository.ConcurrencySafeUpdateAsync(demoThing, loadedVersion);

        await PublishDomainEventsAsync(demoThing.GetDomainEvents());

        return CommandResult.Create(entityId: demoThing.Id);
    }
}
