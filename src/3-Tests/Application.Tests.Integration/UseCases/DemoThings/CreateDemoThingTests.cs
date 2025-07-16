using CanBeYours.Application.Tests.Integration.Fixtures;
using CanBeYours.Core.Domain.DemoThings;
using FluentAssertions;

namespace CanBeYours.Application.Tests.Integration.UseCases.DemoThings;

[Collection(nameof(DemoThingsCollectionFixture))]
public class CreateDemoThingTests
{
    private readonly DemoThingsCollectionFixture _fixture;

    public CreateDemoThingTests(DemoThingsCollectionFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task DemoThing_is_added()
    {
        //Arrange
        var request = new CreateDemoThingRequest(name: "Test Name", description: "Test Description", type: DemoThingType.DemoType1);
        var createDemoThingUseCase = new CreateDemoThingUseCase(
            _fixture._demoThingRepository,
            _fixture._requestDispatcher,
            _fixture._logger,
            _fixture._currentUser
        );

        //Act
        var result = await createDemoThingUseCase.Handle(request, CancellationToken.None);

        //Assert
        result.EntityId.Should().NotBeNull();
    }
}
