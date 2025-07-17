using CanBeYours.Application.Tests.Integration.Fixtures;
using CanBeYours.Application.UseCases.DemoThings.GetDemoThing;
using CanBeYours.Core.Domain.DemoThings;
using FluentAssertions;
using Microsoft.Extensions.Logging;

namespace CanBeYours.Application.Tests.Integration.UseCases.DemoThings;

[Collection(nameof(DemoThingsCollectionFixture))]
public class GetDemoThingTests
{
    private readonly DemoThingsCollectionFixture _fixture;
    private readonly GetDemoThingUseCase _getDemoThingUseCase;

    public GetDemoThingTests(DemoThingsCollectionFixture fixture)
    {
        _fixture = fixture;
        _getDemoThingUseCase = GetGetDemoThingUseCase();
    }

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

    private GetDemoThingUseCase GetGetDemoThingUseCase()
    {
        var logger = _fixture.GetRequiredService<ILogger<GetDemoThingUseCase>>();
        return new GetDemoThingUseCase(_fixture._demoThingRepository, _fixture._mapper, logger, _fixture._currentUser, _fixture._userAccessorService);
    }
}
