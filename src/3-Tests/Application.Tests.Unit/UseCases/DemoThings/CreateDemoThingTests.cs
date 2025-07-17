using CanBeYours.Application.Tests.Unit.Fixtures;
using CanBeYours.Application.UseCases.DemoThings.CreateDemoThing;
using CanBeYours.Core.Domain.DemoThings;
using CanBeYours.Core.Resources;
using CodeBlock.DevKit.Core.Exceptions;
using CodeBlock.DevKit.Core.Resources;
using CodeBlock.DevKit.Domain.Exceptions;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace CanBeYours.Application.Tests.Unit.UseCases.DemoThings;

public class CreateDemoThingTests : TestsBaseFixture
{
    private CreateDemoThingUseCase createDemoThingUseCase;

    public CreateDemoThingTests() { }

    [Fact]
    public async Task DemoThing_is_created()
    {
        //Arrange
        var request = new CreateDemoThingRequest(name: "Test Name", description: "Test Description", type: DemoThingType.DemoType1);

        //Act
        var result = await createDemoThingUseCase.Handle(request, CancellationToken.None);

        //Assert
        result.EntityId.Should().NotBeNull();
        await RequestDispatcher!.Received(1).PublishEvent(Arg.Any<DemoThingCreated>());
    }

    [Fact]
    public async Task DemoThing_is_not_created_if_name_is_not_provided()
    {
        //Arrange
        var expectedError = new DomainException(
            nameof(CoreResource.Required),
            typeof(CoreResource),
            new List<MessagePlaceholder> { MessagePlaceholder.CreateResource(SharedResource.DemoThing_Name, typeof(SharedResource)) }
        );
        var existedDemoThing = DemoThings.FirstOrDefault();
        var request = new CreateDemoThingRequest(name: "", "Test_Description", DemoThingType.DemoType3);

        //Act
        Func<Task> act = async () => await createDemoThingUseCase.Handle(request, CancellationToken.None);

        //Assert
        await act.Should().ThrowAsync<DomainException>().Where(e => e.Message.Equals(expectedError.Message));
    }

    [Fact]
    public async Task DemoThing_is_not_created_if_description_is_not_provided()
    {
        //Arrange
        var expectedError = new DomainException(
            nameof(CoreResource.Required),
            typeof(CoreResource),
            new List<MessagePlaceholder> { MessagePlaceholder.CreateResource(SharedResource.DemoThing_Description, typeof(SharedResource)) }
        );
        var existedDemoThing = DemoThings.FirstOrDefault();
        var request = new CreateDemoThingRequest(name: "Test Name", description: "", type: DemoThingType.DemoType3);

        //Act
        Func<Task> act = async () => await createDemoThingUseCase.Handle(request, CancellationToken.None);

        //Assert
        await act.Should().ThrowAsync<DomainException>().Where(e => e.Message.Equals(expectedError.Message));
    }

    protected override void TestClassFixtureSetup()
    {
        var logger = Substitute.For<ILogger<CreateDemoThingUseCase>>();

        createDemoThingUseCase = new CreateDemoThingUseCase(DemoThingRepository, RequestDispatcher, logger, CurrentUser);
    }
}
