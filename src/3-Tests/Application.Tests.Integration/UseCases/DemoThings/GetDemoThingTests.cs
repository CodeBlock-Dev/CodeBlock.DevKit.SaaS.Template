using CanBeYours.Application.Tests.Integration.Fixtures;
using CanBeYours.Application.UseCases.DemoThings.GetDemoThing;
using CanBeYours.Core.Domain.DemoThings;
using FluentAssertions;
using Microsoft.Extensions.Logging;

namespace CanBeYours.Application.Tests.Integration.UseCases.DemoThings;

/// <summary>
/// Integration tests for the GetDemoThing use case that verify the complete flow from request to DTO response.
/// This test class demonstrates how to test query operations that include permission checks, user context validation,
/// and AutoMapper integration. The DemoThing functionality shown here is just an example to help you learn how to
/// implement your own unique features into the current codebase. You can use this testing pattern to verify your
/// actual query logic works correctly with real repositories and mapping configurations.
/// </summary>
[Collection(nameof(DemoThingsCollectionFixture))]
public class GetDemoThingTests
{
    private readonly DemoThingsCollectionFixture _fixture;
    private readonly GetDemoThingUseCase _getDemoThingUseCase;

    /// <summary>
    /// Initializes the test class with the shared fixture and creates the use case instance.
    /// This constructor demonstrates how to set up test dependencies using the collection fixture
    /// to ensure proper test isolation and shared infrastructure.
    /// </summary>
    /// <param name="fixture">The shared fixture providing test infrastructure and dependencies</param>
    public GetDemoThingTests(DemoThingsCollectionFixture fixture)
    {
        _fixture = fixture;
        _getDemoThingUseCase = GetGetDemoThingUseCase();
    }

    /// <summary>
    /// Tests that a DemoThing entity can be successfully retrieved from the database and mapped to a DTO.
    /// This test verifies the complete retrieval flow including database query, permission validation,
    /// AutoMapper transformation, and proper response formatting. It demonstrates how to test query logic
    /// in an integration environment with real data persistence.
    /// 
    /// Example test scenario: Retrieving an existing demo thing should return a properly mapped DTO
    /// with all expected properties matching the original entity.
    /// </summary>
    [Fact]
    public async Task DemoThing_is_retrieved()
    {
        //Arrange
        var demoThing = DemoThing.Create("Test Name", "Test Description", DemoThingType.DemoType1, _fixture._currentUser.GetUserId());
        await _fixture.SeedDemoThingAsync(demoThing);
        var request = new GetDemoThingRequest(demoThing.Id);

        //Act
        var result = await _getDemoThingUseCase.Handle(request, CancellationToken.None);

        //Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(demoThing.Id);
        result.Name.Should().Be(demoThing.Name);
    }

    /// <summary>
    /// Creates and configures the GetDemoThingUseCase instance with all required dependencies.
    /// This method demonstrates how to manually construct use case instances for testing purposes,
    /// including how to resolve services from the test fixture and set up all required dependencies
    /// such as repositories, mappers, and user access services.
    /// </summary>
    /// <returns>A configured GetDemoThingUseCase instance ready for testing</returns>
    private GetDemoThingUseCase GetGetDemoThingUseCase()
    {
        var logger = _fixture.GetRequiredService<ILogger<GetDemoThingUseCase>>();
        return new GetDemoThingUseCase(_fixture._demoThingRepository, _fixture._mapper, logger, _fixture._currentUser, _fixture._userAccessorService);
    }
}
