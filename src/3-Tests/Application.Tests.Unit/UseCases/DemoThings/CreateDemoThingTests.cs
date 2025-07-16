using Application.Tests.Unit.Fixtures;
using CanBeYours.Core.Domain.DemoThings;
using FluentAssertions;
using NSubstitute;

namespace Application.Tests.Unit.UseCases.DemoThings;

public class CreateDemoThingTests : TestsBaseFixture
{
    private CreateDemoThingUseCase createDemoThingUseCase;

    public CreateDemoThingTests() { }

    [Fact]
    public async Task DemoThing_is_added()
    {
        //Arrange
        var request = new CreateDemoThingRequest(name: "Test Name", description: "Test Description", type: DemoThingType.DemoType1);

        //Act
        var result = await createDemoThingUseCase.Handle(addDemoThingRequest, CancellationToken.None);

        //Assert
        result.EntityId.Should().NotBeNull();
        var addedDemoThing = DemoThings.FirstOrDefault(u => u.Id == result.EntityId);
        addedDemoThing.Name.Should().Be(request.Name);
        await RequestDispatcher!.Received(1).PublishEvent(Arg.Any<DemoThingCreated>());
    }

    protected override void TestClassFixtureSetup()
    {
        createDemoThingUseCase = new CreateDemoThingUseCase(DemoThingRepository, RequestDispatcher, Logger, CurrentUser);
    }
}
