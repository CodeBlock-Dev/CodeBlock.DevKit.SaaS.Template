using CanBeYours.Core.Domain.DemoThings;
using CodeBlock.DevKit.Application.Srvices;
using CodeBlock.DevKit.Test.TestBase;
using NSubstitute;

namespace CanBeYours.Application.Tests.Unit.Fixtures;

public abstract class TestsBaseFixture : UnitTestsBase
{
    protected IRequestDispatcher RequestDispatcher;
    protected IDemoThingRepository DemoThingRepository;
    protected List<DemoThing> DemoThings = new List<DemoThing>();
    protected ICurrentUser CurrentUser;

    protected override void FixtureSetup()
    {
        CommonFixtureSetup();

        TestClassFixtureSetup();
    }

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
    /// Each test class should setup its own fixture
    /// </summary>
    protected abstract void TestClassFixtureSetup();

    /// <summary>
    ///
    /// </summary>
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
