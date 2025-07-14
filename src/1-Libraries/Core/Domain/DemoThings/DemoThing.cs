using CodeBlock.DevKit.Core.Extensions;
using CodeBlock.DevKit.Domain.Entities;

namespace CanBeYours.Core.Domain.DemoThings;

public sealed class DemoThing : AggregateRoot
{
    private DemoThing(string name, string description)
    {
        Name = name;
        Description = description;

        CheckPolicies();

        AddDomainEvent(new DemoThingCreated(Id, Name));
        TrackChange(nameof(DemoThingCreated));
    }

    public string Name { get; private set; }
    public string Description { get; private set; }

    public static DemoThing Create(string name, string description)
    {
        return new DemoThing(name, description);
    }

    public void Update(string name, string description)
    {
        if (Name == name && Description == description)
            return;

        Name = name;
        Description = description;

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
    }
}
