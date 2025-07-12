using CodeBlock.DevKit.Domain.Events;

namespace CanBeYours.Core.Domain.DemoThings;

public record DemoThingCreated(string Id, string Name) : IDomainEvent;

public record DemoThingUpdated(string Id, string Name) : IDomainEvent;
