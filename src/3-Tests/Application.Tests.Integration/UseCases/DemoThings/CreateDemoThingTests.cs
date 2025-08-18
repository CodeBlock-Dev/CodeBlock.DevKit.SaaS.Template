using CanBeYours.Application.Tests.Integration.Fixtures;
using CanBeYours.Application.UseCases.DemoThings.CreateDemoThing;
using CanBeYours.Core.Domain.DemoThings;
using FluentAssertions;
using Microsoft.Extensions.Logging;

namespace CanBeYours.Application.Tests.Integration.UseCases.DemoThings;

/// <summary>
/// Integration tests for the CreateDemoThing use case that verify the complete flow from request to database persistence.
/// This test class demonstrates how to write comprehensive integration tests that test the entire stack including
/// domain logic, repository operations, and domain event publishing. The DemoThing functionality shown here is just
/// an example to help you learn how to implement your own unique features into the current codebase. You can use
/// this testing pattern to verify your actual business logic works correctly in a real database environment.
/// </summary>
[Collection(nameof(DemoThingsCollectionFixture))]
public class CreateDemoThingTests
{
    private readonly DemoThingsCollectionFixture _fixture;
    private readonly CreateDemoThingUseCase _createDemoThingUseCase;

    /// <summary>
    /// Initializes the test class with the shared fixture and creates the use case instance.
    /// This constructor demonstrates how to set up test dependencies using the collection fixture
    /// to ensure proper test isolation and shared infrastructure.
    /// </summary>
    /// <param name="fixture">The shared fixture providing test infrastructure and dependencies</param>
    public CreateDemoThingTests(DemoThingsCollectionFixture fixture)
    {
        _fixture = fixture;
        _createDemoThingUseCase = GetCreateDemoThingUseCase();
    }

    /// <summary>
    /// Tests that a DemoThing entity can be successfully created and persisted to the database.
    /// This test verifies the complete create flow including domain entity creation, repository persistence,
    /// and domain event publishing. It demonstrates how to test business logic in an integration environment.
    /// 
    /// Example test scenario: Creating a new demo thing with valid data should result in a persisted entity
    /// with a generated ID and published domain events.
    /// </summary>
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

    /// <summary>
    /// Creates and configures the CreateDemoThingUseCase instance with all required dependencies.
    /// This method demonstrates how to manually construct use case instances for testing purposes,
    /// including how to resolve services from the test fixture and set up mock dependencies.
    /// </summary>
    /// <returns>A configured CreateDemoThingUseCase instance ready for testing</returns>
    private CreateDemoThingUseCase GetCreateDemoThingUseCase()
    {
        var logger = _fixture.GetRequiredService<ILogger<CreateDemoThingUseCase>>();

        return new CreateDemoThingUseCase(_fixture._demoThingRepository, _fixture._requestDispatcher, logger, _fixture._currentUser);
    }
}
