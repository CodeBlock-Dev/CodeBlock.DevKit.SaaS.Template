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

    public UpdateDemoThingUseCase(IDemoThingRepository demoThingRepository, IRequestDispatcher requestDispatcher, ILogger<UpdateDemoThingUseCase> logger)
        : base(requestDispatcher, logger)
    {
        _demoThingRepository = demoThingRepository;
    }

    public async Task<CommandResult> Handle(UpdateDemoThingRequest request, CancellationToken cancellationToken)
    {
        var demoThing = await _demoThingRepository.GetByIdAsync(request.Id);
        if (demoThing == null)
            return CommandResult.NotFound();

        demoThing.Update(request.Name, request.Description);

        await _demoThingRepository.UpdateAsync(demoThing);

        await PublishDomainEventsAsync(demoThing.GetDomainEvents());

        return CommandResult.Create(entityId: demoThing.Id);
    }
} 