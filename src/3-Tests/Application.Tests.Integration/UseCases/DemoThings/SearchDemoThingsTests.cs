using CanBeYours.Application.Tests.Integration.Fixtures;
using CanBeYours.Application.UseCases.DemoThings.SearchDemoThings;
using CanBeYours.Core.Domain.DemoThings;
using CodeBlock.DevKit.Core.Helpers;
using FluentAssertions;
using Microsoft.Extensions.Logging;

namespace CanBeYours.Application.Tests.Integration.UseCases.DemoThings;

/// <summary>
/// Integration tests for the SearchDemoThings use case that verify the complete search flow with filtering and pagination.
/// This test class demonstrates how to test complex query operations that include multiple search criteria, sorting,
/// pagination, and result mapping. The DemoThing functionality shown here is just an example to help you learn how to
/// implement your own unique features into the current codebase. You can use this testing pattern to verify your
/// actual search logic works correctly with real repositories and complex query scenarios.
/// </summary>
[Collection(nameof(DemoThingsCollectionFixture))]
public class SearchDemoThingsTests
{
    private readonly DemoThingsCollectionFixture _fixture;
    private readonly SearchDemoThingsUseCase _searchDemoThingsUseCase;

    /// <summary>
    /// Initializes the test class with the shared fixture and creates the use case instance.
    /// This constructor demonstrates how to set up test dependencies using the collection fixture
    /// to ensure proper test isolation and shared infrastructure.
    /// </summary>
    /// <param name="fixture">The shared fixture providing test infrastructure and dependencies</param>
    public SearchDemoThingsTests(DemoThingsCollectionFixture fixture)
    {
        _fixture = fixture;
        _searchDemoThingsUseCase = GetSearchDemoThingsUseCase();
    }

    /// <summary>
    /// Tests that DemoThing entities can be successfully searched using various criteria and returned with pagination.
    /// This test verifies the complete search flow including database querying with filters, sorting, pagination,
    /// and result mapping to DTOs. It demonstrates how to test complex search logic in an integration environment
    /// with real data persistence and multiple search parameters.
    /// 
    /// Example test scenario: Searching for demo things with specific criteria should return paginated results
    /// containing matching entities with proper sorting and filtering applied.
    /// </summary>
    [Fact]
    public async Task DemoThings_are_searched()
    {
        //Arrange
        var demoThing = DemoThing.Create("Test Name", "Test Description", DemoThingType.DemoType1, _fixture._currentUser.GetUserId());
        await _fixture.SeedDemoThingAsync(demoThing);
        var request = new SearchDemoThingsRequest(
            term: "Test",
            type: DemoThingType.DemoType1,
            pageNumber: 1,
            recordsPerPage: 10,
            sortOrder: SortOrder.Desc,
            fromDateTime: null,
            toDateTime: null
        );

        //Act
        var result = await _searchDemoThingsUseCase.Handle(request, CancellationToken.None);

        //Assert
        result.Should().NotBeNull();
        result.Items.Should().NotBeEmpty();
        result.Items.Should().Contain(x => x.Id == demoThing.Id);
    }

    /// <summary>
    /// Creates and configures the SearchDemoThingsUseCase instance with all required dependencies.
    /// This method demonstrates how to manually construct use case instances for testing purposes,
    /// including how to resolve services from the test fixture and set up all required dependencies
    /// such as repositories, mappers, and user access services for search operations.
    /// </summary>
    /// <returns>A configured SearchDemoThingsUseCase instance ready for testing</returns>
    private SearchDemoThingsUseCase GetSearchDemoThingsUseCase()
    {
        var logger = _fixture.GetRequiredService<ILogger<SearchDemoThingsUseCase>>();
        return new SearchDemoThingsUseCase(_fixture._demoThingRepository, _fixture._mapper, logger, _fixture._userAccessorService);
    }
}
