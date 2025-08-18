using System.ComponentModel.DataAnnotations;
using CanBeYours.Core.Resources;
using CodeBlock.DevKit.Core.Extensions;
using CodeBlock.DevKit.Domain.Entities;

namespace CanBeYours.Core.Domain.DemoThings;

/// <summary>
/// DemoThing is an example domain entity that demonstrates how to implement a complete domain model
/// using the CodeBlock.DevKit framework. This class serves as a learning example and should be
/// replaced with your actual business entities.
/// 
/// Key features demonstrated:
/// - Domain-driven design with aggregate root pattern
/// - Business rule validation through policies
/// - Domain event publishing
/// - Immutable properties with controlled modification
/// - Factory method pattern for creation
/// </summary>
public sealed class DemoThing : AggregateRoot
{
    /// <summary>
    /// Private constructor ensures DemoThing can only be created through the Create factory method
    /// and enforces business rules during instantiation.
    /// </summary>
    /// <param name="name">The name of the demo thing</param>
    /// <param name="description">Detailed description of the demo thing</param>
    /// <param name="type">Classification type of the demo thing</param>
    /// <param name="userId">Identifier of the user who owns this demo thing</param>
    private DemoThing(string name, string description, DemoThingType type, string userId)
    {
        Name = name;
        Description = description;
        Type = type;
        UserId = userId;

        CheckPolicies();

        AddDomainEvent(new DemoThingCreated(Id, Name));
        TrackChange(nameof(DemoThingCreated));
    }

    /// <summary>
    /// The display name of the demo thing. Required field that cannot be empty.
    /// </summary>
    public string Name { get; private set; }
    
    /// <summary>
    /// Detailed description providing additional context about the demo thing. Required field.
    /// </summary>
    public string Description { get; private set; }
    
    /// <summary>
    /// Classification type that categorizes the demo thing. Required field.
    /// </summary>
    public DemoThingType Type { get; private set; }
    
    /// <summary>
    /// Identifier of the user who owns this demo thing. Required field for ownership tracking.
    /// </summary>
    public string UserId { get; private set; }

    /// <summary>
    /// Factory method to create a new DemoThing instance. This method enforces business rules
    /// and ensures proper initialization of the domain entity.
    /// 
    /// Example usage:
    /// var demoThing = DemoThing.Create("My Demo", "A sample demo thing", DemoThingType.DemoType1, "user123");
    /// </summary>
    /// <param name="name">The name of the demo thing</param>
    /// <param name="description">Detailed description of the demo thing</param>
    /// <param name="type">Classification type of the demo thing</param>
    /// <param name="userId">Identifier of the user who owns this demo thing</param>
    /// <returns>A new DemoThing instance with validated business rules</returns>
    public static DemoThing Create(string name, string description, DemoThingType type, string userId)
    {
        return new DemoThing(name, description, type, userId);
    }

    /// <summary>
    /// Updates the properties of an existing DemoThing. Only modifies the entity if changes are detected,
    /// ensuring efficient domain event publishing and change tracking.
    /// 
    /// Example usage:
    /// demoThing.Update("Updated Name", "New description", DemoThingType.DemoType2);
    /// </summary>
    /// <param name="name">New name for the demo thing</param>
    /// <param name="description">New description for the demo thing</param>
    /// <param name="type">New classification type for the demo thing</param>
    public void Update(string name, string description, DemoThingType type)
    {
        if (Name == name && Description == description && Type == type)
            return;

        Name = name;
        Description = description;
        Type = type;

        CheckPolicies();

        AddDomainEvent(new DemoThingUpdated(Id, Name));
        TrackChange(nameof(DemoThingUpdated));
    }

    /// <summary>
    /// Override point for domain invariant validation. Currently empty as this example
    /// focuses on basic business rule validation through policies.
    /// </summary>
    protected override void CheckInvariants() { }

    /// <summary>
    /// Validates business rules and policies for the DemoThing entity. Throws domain exceptions
    /// if any required fields are missing or invalid.
    /// 
    /// Business rules enforced:
    /// - Name must not be null, empty, or whitespace
    /// - Description must not be null, empty, or whitespace  
    /// - UserId must not be null, empty, or whitespace
    /// </summary>
    private void CheckPolicies()
    {
        if (Name.IsNullOrEmptyOrWhiteSpace())
            throw DemoThingDomainExceptions.NameIsRequired();

        if (Description.IsNullOrEmptyOrWhiteSpace())
            throw DemoThingDomainExceptions.DescriptionIsRequired();

        if (UserId.IsNullOrEmptyOrWhiteSpace())
            throw DemoThingDomainExceptions.UserIdIsRequired();
    }
}

/// <summary>
/// Enumeration defining the available classification types for DemoThing entities.
/// This demonstrates how to use localized display names for enum values.
/// 
/// Note: This is an example implementation. Replace with your actual business classification types.
/// </summary>
public enum DemoThingType
{
    [Display(Name = nameof(SharedResource.DemoThingType_DemoType1), ResourceType = typeof(SharedResource))]
    DemoType1 = 0,

    [Display(Name = nameof(SharedResource.DemoThingType_DemoType2), ResourceType = typeof(SharedResource))]
    DemoType2 = 1,

    [Display(Name = nameof(SharedResource.DemoThingType_DemoType3), ResourceType = typeof(SharedResource))]
    DemoType3 = 2,
}
