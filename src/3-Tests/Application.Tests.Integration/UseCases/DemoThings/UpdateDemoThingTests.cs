using CanBeYours.Application.Tests.Integration.Fixtures;
using CanBeYours.Application.UseCases.DemoThings.UpdateDemoThing;
using CanBeYours.Core.Domain.DemoThings;
using FluentAssertions;
using Microsoft.Extensions.Logging;

namespace CanBeYours.Application.Tests.Integration.UseCases.DemoThings;

/// <summary>
/// Integration tests for the UpdateDemoThing use case that verify the complete update flow with concurrency control.
/// This test class demonstrates how to test update operations that include entity retrieval, business rule validation,
/// concurrency-safe updates, and domain event publishing. The DemoThing functionality shown here is just an example
/// to help you learn how to implement your own unique features into the current codebase. You can use this testing
/// pattern to verify your actual update logic works correctly with real repositories and concurrency scenarios.
/// </summary>
[Collection(nameof(DemoThingsCollectionFixture))]
public class UpdateDemoThingTests
{
    private readonly DemoThingsCollectionFixture _fixture;
    private readonly UpdateDemoThingUseCase _updateDemoThingUseCase;

    /// <summary>
    /// Initializes the test class with the shared fixture and creates the use case instance.
    /// This constructor demonstrates how to set up test dependencies using the collection fixture
    /// to ensure proper test isolation and shared infrastructure.
    /// </summary>
    /// <param name="fixture">The shared fixture providing test infrastructure and dependencies</param>
    public UpdateDemoThingTests(DemoThingsCollectionFixture fixture)
    {
        _fixture = fixture;
        _updateDemoThingUseCase = GetUpdateDemoThingUseCase();
    }

    /// <summary>
    /// Tests that a DemoThing entity can be successfully updated with new data and persisted to the database.
    /// This test verifies the complete update flow including entity retrieval, business rule validation,
    /// concurrency-safe updates using version control, domain event publishing, and database persistence.
    /// It demonstrates how to test update logic in an integration environment with real data persistence.
    /// 
    /// Example test scenario: Updating an existing demo thing should modify the entity properties,
    /// increment the version, publish domain events, and persist changes to the database.
    /// </summary>
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

    /// <summary>
    /// Creates and configures the UpdateDemoThingUseCase instance with all required dependencies.
    /// This method demonstrates how to manually construct use case instances for testing purposes,
    /// including how to resolve services from the test fixture and set up all required dependencies
    /// such as repositories and request dispatchers for update operations.
    /// </summary>
    /// <returns>A configured UpdateDemoThingUseCase instance ready for testing</returns>
    private UpdateDemoThingUseCase GetUpdateDemoThingUseCase()
    {
        var logger = _fixture.GetRequiredService<ILogger<UpdateDemoThingUseCase>>();
        return new UpdateDemoThingUseCase(_fixture._demoThingRepository, _fixture._requestDispatcher, logger);
    }
}
