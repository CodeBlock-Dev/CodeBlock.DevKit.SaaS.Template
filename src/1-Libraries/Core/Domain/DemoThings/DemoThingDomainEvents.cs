using CodeBlock.DevKit.Domain.Events;

namespace CanBeYours.Core.Domain.DemoThings;

/// <summary>
/// Domain events for DemoThing entities. These events are published when significant changes
/// occur to DemoThing entities, allowing other parts of the system to react to these changes.
/// 
/// This is an example implementation showing domain event patterns. Replace with your actual
/// domain events that represent important business occurrences in your domain.
/// 
/// Key features demonstrated:
/// - Domain event records for immutable event data
/// - Event publishing when entities are created or updated
/// - Integration with the domain event system for decoupled communication
/// 
/// Usage examples:
/// - Audit logging when entities are modified
/// - Notification systems for entity changes
/// - Integration with external systems
/// - CQRS read model updates
/// </summary>

/// <summary>
/// Event raised when a new DemoThing entity is created. This event contains the essential
/// information about the newly created entity that other parts of the system might need.
/// 
/// Example usage in event handlers:
/// public class DemoThingCreatedHandler : IDomainEventHandler<DemoThingCreated>
/// {
///     public async Task HandleAsync(DemoThingCreated @event)
///     {
///         // Handle the creation event (e.g., send notifications, update caches)
///     }
/// }
/// </summary>
/// <param name="Id">Unique identifier of the created DemoThing</param>
/// <param name="Name">Name of the created DemoThing</param>
public record DemoThingCreated(string Id, string Name) : IDomainEvent;

/// <summary>
/// Event raised when an existing DemoThing entity is updated. This event contains the essential
/// information about the updated entity that other parts of the system might need.
/// 
/// Example usage in event handlers:
/// public class DemoThingUpdatedHandler : IDomainEventHandler<DemoThingUpdated>
/// {
///     public async Task HandleAsync(DemoThingUpdated @event)
///     {
///         // Handle the update event (e.g., send notifications, update caches)
///     }
/// }
/// </summary>
/// <param name="Id">Unique identifier of the updated DemoThing</param>
/// <param name="Name">Updated name of the DemoThing</param>
public record DemoThingUpdated(string Id, string Name) : IDomainEvent;
