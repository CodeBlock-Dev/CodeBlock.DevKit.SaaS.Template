using CanBeYours.Application.Tests.Unit.Fixtures;
using CanBeYours.Application.UseCases.DemoThings.CreateDemoThing;
using CanBeYours.Core.Domain.DemoThings;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace CanBeYours.Application.Tests.Unit.UseCases.DemoThings;

public class CreateDemoThingTests : TestsBaseFixture
{
    private CreateDemoThingUseCase createDemoThingUseCase;

    public CreateDemoThingTests() { }

    [Fact]
    public async Task DemoThing_is_created()
    {
        //Arrange
        var request = new CreateDemoThingRequest(name: "Test Name", description: "Test Description", type: DemoThingType.DemoType1);

        //Act
        var result = await createDemoThingUseCase.Handle(request, CancellationToken.None);

        //Assert
        result.EntityId.Should().NotBeNull();
        await RequestDispatcher!.Received(1).PublishEvent(Arg.Any<DemoThingCreated>());
    }

    protected override void TestClassFixtureSetup()
    {
        var logger = Substitute.For<ILogger<CreateDemoThingUseCase>>();

        createDemoThingUseCase = new CreateDemoThingUseCase(DemoThingRepository, RequestDispatcher, logger, CurrentUser);
    }
}
