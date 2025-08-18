using CanBeYours.Application.Exceptions;
using CanBeYours.Core.Domain.DemoThings;
using CodeBlock.DevKit.Application.Commands;
using CodeBlock.DevKit.Application.Srvices;
using CodeBlock.DevKit.Core.Helpers;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CanBeYours.Application.UseCases.DemoThings.UpdateDemoThing;

/// <summary>
/// Use case for updating an existing DemoThing entity.
/// This class demonstrates how to implement update command handlers that include:
/// - Entity existence validation with custom exceptions
/// - Concurrency-safe updates using version control
/// - Domain event publishing for side effects
/// - Proper error handling patterns
/// The DemoThing functionality shown here is just an example to help you learn how to implement
/// your own unique features into the current codebase.
/// </summary>
internal class UpdateDemoThingUseCase : BaseCommandHandler, IRequestHandler<UpdateDemoThingRequest, CommandResult>
{
    private readonly IDemoThingRepository _demoThingRepository;

    /// <summary>
    /// Initializes a new instance of the UpdateDemoThingUseCase with the required dependencies.
    /// </summary>
    /// <param name="demoThingRepository">The repository for demo thing operations</param>
    /// <param name="requestDispatcher">The request dispatcher for handling domain events</param>
    /// <param name="logger">The logger for this use case</param>
    public UpdateDemoThingUseCase(
        IDemoThingRepository demoThingRepository,
        IRequestDispatcher requestDispatcher,
        ILogger<UpdateDemoThingUseCase> logger
    )
        : base(requestDispatcher, logger)
    {
        _demoThingRepository = demoThingRepository;
    }

    /// <summary>
    /// Handles the update of an existing demo thing.
    /// This method demonstrates a complete update flow including:
    /// 1. Entity retrieval and existence validation
    /// 2. Concurrency-safe updates using version control
    /// 3. Domain event publishing for side effects
    /// 4. Proper error handling with custom exceptions
    /// </summary>
    /// <param name="request">The command request containing the update data</param>
    /// <param name="cancellationToken">Cancellation token for the operation</param>
    /// <returns>A command result with the ID of the updated entity</returns>
    /// <exception cref="DemoThingApplicationExceptions">Thrown when the demo thing is not found</exception>
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
