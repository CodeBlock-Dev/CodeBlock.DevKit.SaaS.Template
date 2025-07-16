namespace CanBeYours.Application.Tests.Integration.Fixtures;

/// <summary>
///
/// </summary>
[CollectionDefinition(nameof(DemoThingsCollectionFixture))]
public class DemoThingCollectionFixtureDefinition : ICollectionFixture<DemoThingsCollectionFixture>
{
    // This class has no code, and is never created. Its purpose is simply
    // to be the place to apply [CollectionDefinition] and all the
    // ICollectionFixture<> interfaces.
}

/// <summary>
///
/// </summary>
public class DemoThingsCollectionFixture : TestsBaseFixture
{
    public DemoThingsCollectionFixture()
        : base(dbNameSuffix: nameof(DemoThingsCollectionFixture)) { }
}
