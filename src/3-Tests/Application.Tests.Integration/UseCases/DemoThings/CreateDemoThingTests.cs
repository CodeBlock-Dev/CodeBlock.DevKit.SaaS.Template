using CanBeYours.Application.Tests.Integration.Fixtures;
using CanBeYours.Application.UseCases.DemoThings.CreateDemoThing;
using CanBeYours.Core.Domain.DemoThings;
using FluentAssertions;
using Microsoft.Extensions.Logging;

namespace CanBeYours.Application.Tests.Integration.UseCases.DemoThings;

[Collection(nameof(DemoThingsCollectionFixture))]
public class CreateDemoThingTests
{
    private readonly DemoThingsCollectionFixture _fixture;
    private readonly CreateDemoThingUseCase _createDemoThingUseCase;

    public CreateDemoThingTests(DemoThingsCollectionFixture fixture)
    {
        _fixture = fixture;
        _createDemoThingUseCase = GetCreateDemoThingUseCase();
    }

    [Fact]
    public async Task DemoThing_is_created()
    {
        //Arrange
        var request = new CreateDemoThingRequest(name: "Test Name", description: "Test Description", type: DemoThingType.DemoType1);

        //Act
        var result = await _createDemoThingUseCase.Handle(request, CancellationToken.None);

        //Assert
        result.EntityId.Should().NotBeNull();
    }

    private CreateDemoThingUseCase GetCreateDemoThingUseCase()
    {
        var logger = _fixture.GetRequiredService<ILogger<CreateDemoThingUseCase>>();

        return new CreateDemoThingUseCase(_fixture._demoThingRepository, _fixture._requestDispatcher, logger, _fixture._currentUser);
    }
}
