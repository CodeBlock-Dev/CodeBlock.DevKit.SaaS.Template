using CanBeYours.Application.Tests.Integration.Fixtures;
using CanBeYours.Application.UseCases.DemoThings.SearchDemoThings;
using CanBeYours.Core.Domain.DemoThings;
using CodeBlock.DevKit.Core.Helpers;
using FluentAssertions;
using Microsoft.Extensions.Logging;

namespace CanBeYours.Application.Tests.Integration.UseCases.DemoThings;

[Collection(nameof(DemoThingsCollectionFixture))]
public class SearchDemoThingsTests
{
    private readonly DemoThingsCollectionFixture _fixture;
    private readonly SearchDemoThingsUseCase _searchDemoThingsUseCase;

    public SearchDemoThingsTests(DemoThingsCollectionFixture fixture)
    {
        _fixture = fixture;
        _searchDemoThingsUseCase = GetSearchDemoThingsUseCase();
    }

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

    private SearchDemoThingsUseCase GetSearchDemoThingsUseCase()
    {
        var logger = _fixture.GetRequiredService<ILogger<SearchDemoThingsUseCase>>();
        return new SearchDemoThingsUseCase(_fixture._demoThingRepository, _fixture._mapper, logger, _fixture._userAccessorService);
    }
}
