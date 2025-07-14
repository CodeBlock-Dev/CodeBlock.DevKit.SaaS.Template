using CanBeYours.Core.Domain.DemoThings;
using CodeBlock.DevKit.Application.Commands;
using CodeBlock.DevKit.Application.Srvices;
using CodeBlock.DevKit.Core.Helpers;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CanBeYours.Application.UseCases.DemoThings.CreateDemoThing;

internal class CreateDemoThingUseCase : BaseCommandHandler, IRequestHandler<CreateDemoThingRequest, CommandResult>
{
    private readonly IDemoThingRepository _demoThingRepository;

    public CreateDemoThingUseCase(
        IDemoThingRepository demoThingRepository,
        IRequestDispatcher requestDispatcher,
        ILogger<CreateDemoThingUseCase> logger
    )
        : base(requestDispatcher, logger)
    {
        _demoThingRepository = demoThingRepository;
    }

    public async Task<CommandResult> Handle(CreateDemoThingRequest request, CancellationToken cancellationToken)
    {
        var demoThing = DemoThing.Create(request.Name, request.Description);

        await _demoThingRepository.AddAsync(demoThing);

        await PublishDomainEventsAsync(demoThing.GetDomainEvents());

        return CommandResult.Create(entityId: demoThing.Id);
    }
}
