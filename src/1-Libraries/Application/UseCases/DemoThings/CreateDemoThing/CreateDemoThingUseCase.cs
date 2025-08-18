using CanBeYours.Core.Domain.DemoThings;
using CodeBlock.DevKit.Application.Commands;
using CodeBlock.DevKit.Application.Srvices;
using CodeBlock.DevKit.Core.Helpers;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CanBeYours.Application.UseCases.DemoThings.CreateDemoThing;

/// <summary>
/// Use case for creating a new DemoThing entity.
/// This class demonstrates how to implement command handlers that follow CQRS patterns,
/// handle domain events, and integrate with repositories and user context.
/// The DemoThing functionality shown here is just an example to help you learn how to implement
/// your own unique features into the current codebase.
/// </summary>
internal class CreateDemoThingUseCase : BaseCommandHandler, IRequestHandler<CreateDemoThingRequest, CommandResult>
{
    private readonly IDemoThingRepository _demoThingRepository;
    private readonly ICurrentUser _currentUser;

    /// <summary>
    /// Initializes a new instance of the CreateDemoThingUseCase with the required dependencies.
    /// </summary>
    /// <param name="demoThingRepository">The repository for demo thing operations</param>
    /// <param name="requestDispatcher">The request dispatcher for handling domain events</param>
    /// <param name="logger">The logger for this use case</param>
    /// <param name="currentUser">The current user context</param>
    public CreateDemoThingUseCase(
        IDemoThingRepository demoThingRepository,
        IRequestDispatcher requestDispatcher,
        ILogger<CreateDemoThingUseCase> logger,
        ICurrentUser currentUser
    )
        : base(requestDispatcher, logger)
    {
        _demoThingRepository = demoThingRepository;
        _currentUser = currentUser;
    }

    /// <summary>
    /// Handles the creation of a new demo thing.
    /// This method demonstrates the complete flow of creating an entity:
    /// 1. Creating the domain entity with proper user context
    /// 2. Persisting to the repository
    /// 3. Publishing domain events for side effects
    /// </summary>
    /// <param name="request">The command request containing the demo thing data</param>
    /// <param name="cancellationToken">Cancellation token for the operation</param>
    /// <returns>A command result with the ID of the created entity</returns>
    public async Task<CommandResult> Handle(CreateDemoThingRequest request, CancellationToken cancellationToken)
    {
        var demoThing = DemoThing.Create(request.Name, request.Description, request.Type, _currentUser.GetUserId());

        await _demoThingRepository.AddAsync(demoThing);

        await PublishDomainEventsAsync(demoThing.GetDomainEvents());

        return CommandResult.Create(entityId: demoThing.Id);
    }
}
