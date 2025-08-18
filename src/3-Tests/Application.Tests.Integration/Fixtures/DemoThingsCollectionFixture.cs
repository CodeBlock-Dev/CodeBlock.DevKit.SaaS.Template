namespace CanBeYours.Application.Tests.Integration.Fixtures;

/// <summary>
/// Collection definition for DemoThings integration tests that enables test isolation and shared fixture usage.
/// This class has no code and is never created. Its purpose is simply to be the place to apply 
/// [CollectionDefinition] and all the ICollectionFixture<> interfaces. This pattern demonstrates
/// how to organize integration tests that share common setup and teardown logic.
/// The DemoThing functionality shown here is just an example to help you learn how to implement
/// your own unique features into the current codebase.
/// </summary>
[CollectionDefinition(nameof(DemoThingsCollectionFixture))]
public class DemoThingCollectionFixtureDefinition : ICollectionFixture<DemoThingsCollectionFixture>
{
    // This class has no code, and is never created. Its purpose is simply
    // to be the place to apply [CollectionDefinition] and all the
    // ICollectionFixture<> interfaces.
}

/// <summary>
/// Collection fixture for DemoThings integration tests that provides shared test infrastructure.
/// This class extends TestsBaseFixture to create a dedicated test collection with its own database
/// instance, ensuring test isolation while allowing tests within the collection to share common setup.
/// The DemoThing functionality shown here is just an example to help you learn how to implement
/// your own unique features into the current codebase. You can use this pattern to organize
/// tests for your actual business entities.
/// </summary>
public class DemoThingsCollectionFixture : TestsBaseFixture
{
    /// <summary>
    /// Initializes the DemoThings collection fixture with a unique database suffix.
    /// This constructor ensures that all tests in this collection use the same database instance,
    /// allowing for shared test data and proper cleanup between test runs.
    /// </summary>
    public DemoThingsCollectionFixture()
        : base(dbNameSuffix: nameof(DemoThingsCollectionFixture)) { }
}
