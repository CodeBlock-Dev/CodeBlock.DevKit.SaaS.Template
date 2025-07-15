using System.ComponentModel.DataAnnotations;
using CanBeYours.Core.Resources;
using CodeBlock.DevKit.Core.Extensions;
using CodeBlock.DevKit.Domain.Entities;

namespace CanBeYours.Core.Domain.DemoThings;

public sealed class DemoThing : AggregateRoot
{
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

    public string Name { get; private set; }
    public string Description { get; private set; }
    public DemoThingType Type { get; private set; }
    public string UserId { get; private set; }

    public static DemoThing Create(string name, string description, DemoThingType type, string userId)
    {
        return new DemoThing(name, description, type, userId);
    }

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

    protected override void CheckInvariants() { }

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

public enum DemoThingType
{
    [Display(Name = nameof(SharedResource.DemoThingType_DemoType1), ResourceType = typeof(SharedResource))]
    DemoType1 = 0,

    [Display(Name = nameof(SharedResource.DemoThingType_DemoType2), ResourceType = typeof(SharedResource))]
    DemoType2 = 1,

    [Display(Name = nameof(SharedResource.DemoThingType_DemoType3), ResourceType = typeof(SharedResource))]
    DemoType3 = 2,
}
