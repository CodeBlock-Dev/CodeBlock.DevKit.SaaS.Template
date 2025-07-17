using CanBeYours.Application.Tests.Integration.Fixtures;
using CanBeYours.Application.UseCases.DemoThings.UpdateDemoThing;
using CanBeYours.Core.Domain.DemoThings;
using FluentAssertions;
using Microsoft.Extensions.Logging;

namespace CanBeYours.Application.Tests.Integration.UseCases.DemoThings;

[Collection(nameof(DemoThingsCollectionFixture))]
public class UpdateDemoThingTests
{
    private readonly DemoThingsCollectionFixture _fixture;
    private readonly UpdateDemoThingUseCase _updateDemoThingUseCase;

    public UpdateDemoThingTests(DemoThingsCollectionFixture fixture)
    {
        _fixture = fixture;
        _updateDemoThingUseCase = GetUpdateDemoThingUseCase();
    }

    [Fact]
    public async Task DemoThing_is_updated()
    {
        //Arrange
        var demoThing = DemoThing.Create("Test Name", "Test Description", DemoThingType.DemoType1, _fixture._currentUser.GetUserId());
        await _fixture.SeedDemoThingAsync(demoThing);
        var request = new UpdateDemoThingRequest(demoThing.Id, "Updated Name", "Updated Description", DemoThingType.DemoType2);

        //Act
        var result = await _updateDemoThingUseCase.Handle(request, CancellationToken.None);

        //Assert
        result.EntityId.Should().NotBeNull();
        var updatedDemoThing = await _fixture.GetDemoThingAsync(result.EntityId);
        updatedDemoThing.Name.Should().Be("Updated Name");
        updatedDemoThing.Description.Should().Be("Updated Description");
        updatedDemoThing.Type.Should().Be(DemoThingType.DemoType2);
    }

    private UpdateDemoThingUseCase GetUpdateDemoThingUseCase()
    {
        var logger = _fixture.GetRequiredService<ILogger<UpdateDemoThingUseCase>>();
        return new UpdateDemoThingUseCase(_fixture._demoThingRepository, _fixture._requestDispatcher, logger);
    }
}
