using CanBeYours.Core.Domain.DemoThings;
using CodeBlock.DevKit.Application.Srvices;
using CodeBlock.DevKit.Test.TestBase;
using NSubstitute;

namespace CanBeYours.Application.Tests.Unit.Fixtures;

/// <summary>
/// Base test fixture for unit tests that provides common setup and mock dependencies.
/// This class demonstrates how to create a robust test infrastructure for unit testing
/// with mocked repositories, request dispatchers, and user context. The DemoThing functionality
/// shown here is just an example to help you learn how to implement your own unique features
/// into the current codebase. You can use this pattern to test your actual business logic.
/// </summary>
public abstract class TestsBaseFixture : UnitTestsBase
{
    /// <summary>
    /// Mock request dispatcher for testing domain event publishing
    /// </summary>
    protected IRequestDispatcher RequestDispatcher;
    
    /// <summary>
    /// Mock repository for DemoThing entities with in-memory test data
    /// </summary>
    protected IDemoThingRepository DemoThingRepository;
    
    /// <summary>
    /// In-memory list of DemoThing entities for testing repository operations
    /// </summary>
    protected List<DemoThing> DemoThings = new List<DemoThing>();
    
    /// <summary>
    /// Mock current user context for testing user-specific operations
    /// </summary>
    protected ICurrentUser CurrentUser;

    /// <summary>
    /// Sets up the test fixture by calling common setup and test-specific setup.
    /// This method is called by the base class to prepare the test environment.
    /// </summary>
    protected override void FixtureSetup()
    {
        CommonFixtureSetup();

        TestClassFixtureSetup();
    }

    /// <summary>
    /// Sets up common test dependencies including mocked services and test data.
    /// This method configures the mock repository with in-memory test data and
    /// sets up the request dispatcher and current user mocks.
    /// </summary>
    private void CommonFixtureSetup()
    {
        RequestDispatcher = Substitute.For<IRequestDispatcher>();
        CurrentUser = Substitute.For<ICurrentUser>();
        CurrentUser.GetUserId().Returns(Guid.NewGuid().ToString());

        DemoThings = GenerateDemoThingsList();

        DemoThingRepository = Substitute.For<IDemoThingRepository>();
        DemoThingRepository
            .GetByIdAsync(Arg.Is<string>(x => DemoThings.Any(o => o.Id == x)))
            .Returns(args => DemoThings.First(u => u.Id == (string)args[0]));
        DemoThingRepository
            .AddAsync(Arg.Any<DemoThing>())
            .Returns(args =>
            {
                DemoThings.Add((DemoThing)args[0]);
                return Task.CompletedTask;
            });
        DemoThingRepository
            .UpdateAsync(Arg.Is<DemoThing>(x => DemoThings.Any(o => o.Id == x.Id)))
            .Returns(args =>
            {
                var existDemoThing = DemoThings.FirstOrDefault(u => u.Id == ((DemoThing)args[0]).Id);
                if (existDemoThing != null)
                {
                    DemoThings.Remove(existDemoThing);
                    DemoThings.Add((DemoThing)args[0]);
                }

                return Task.CompletedTask;
            });

        //DemoThingRepository
        //    .ConcurrencySafeUpdateAsync(Arg.Is<DemoThing>(x => DemoThings.Any(o => o.Id == x.Id)), Arg.Is<string>(x => x.Any()))
        //    .Returns(args =>
        //    {
        //        var existDemoThing = DemoThings.FirstOrDefault(u => u.Id == ((DemoThing)args[0]).Id && u.Version == (string)args[1]);
        //        if (existDemoThing != null)
        //        {
        //            DemoThings.Remove(existDemoThing);
        //            DemoThings.Add((DemoThing)args[0]);
        //        }

        //        return Task.CompletedTask;
        //    });

        DemoThingRepository
            .DeleteAsync(Arg.Is<string>(x => DemoThings.Any(o => o.Id == x)))
            .Returns(args =>
            {
                var demoThing = DemoThings.FirstOrDefault(u => u.Id == (string)args[0]);
                if (demoThing != null)
                    DemoThings.Remove(demoThing);

                return Task.CompletedTask;
            });
    }

    /// <summary>
    /// Abstract method that each test class must implement to set up its own specific fixture.
    /// This allows test classes to configure additional dependencies or test data as needed.
    /// </summary>
    protected abstract void TestClassFixtureSetup();

    /// <summary>
    /// Generates a list of sample DemoThing entities for testing purposes.
    /// This method creates test data that demonstrates different DemoThing types and configurations.
    /// Example usage: var testData = GenerateDemoThingsList();
    /// </summary>
    /// <returns>A list of DemoThing entities with different types and descriptions</returns>
    private List<DemoThing> GenerateDemoThingsList()
    {
        return new List<DemoThing>
        {
            DemoThing.Create("Demo Thing 1", "Description 1", DemoThingType.DemoType1, "User1"),
            DemoThing.Create("Demo Thing 2", "Description 2", DemoThingType.DemoType2, "User2"),
            DemoThing.Create("Demo Thing 3", "Description 3", DemoThingType.DemoType3, "User3"),
        };
    }
}
