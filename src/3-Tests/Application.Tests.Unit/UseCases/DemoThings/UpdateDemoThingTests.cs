using CanBeYours.Application.Tests.Unit.Fixtures;
using CanBeYours.Application.UseCases.DemoThings.UpdateDemoThing;
using CanBeYours.Core.Domain.DemoThings;
using CanBeYours.Core.Resources;
using CodeBlock.DevKit.Core.Exceptions;
using CodeBlock.DevKit.Core.Resources;
using CodeBlock.DevKit.Domain.Exceptions;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using ApplicationException = CodeBlock.DevKit.Application.Exceptions.ApplicationException;

namespace CanBeYours.Application.Tests.Unit.UseCases.DemoThings;

public class UpdateDemoThingTests : TestsBaseFixture
{
    private UpdateDemoThingUseCase updateDemoThingUseCase;

    public UpdateDemoThingTests() { }

    [Fact]
    public async Task DemoThing_is_updated()
    {
        //Arrange
        var existedDemoThing = DemoThings.FirstOrDefault();
        var request = new UpdateDemoThingRequest(existedDemoThing.Id, "Updated Name", "Updated Description", DemoThingType.DemoType2);

        //Act
        var result = await updateDemoThingUseCase.Handle(request, CancellationToken.None);

        //Assert
        result.EntityId.Should().NotBeNull();
        await RequestDispatcher!.Received(1).PublishEvent(Arg.Any<DemoThingUpdated>());
    }

    [Fact]
    public async Task DemoThing_is_not_updated_if_name_is_not_provided()
    {
        //Arrange
        var expectedError = new DomainException(
            nameof(CoreResource.Required),
            typeof(CoreResource),
            new List<MessagePlaceholder> { MessagePlaceholder.CreateResource(SharedResource.DemoThing_Name, typeof(SharedResource)) }
        );
        var existedDemoThing = DemoThings.FirstOrDefault();
        var request = new UpdateDemoThingRequest(existedDemoThing.Id, "", "Updated Description", DemoThingType.DemoType2);

        //Act
        Func<Task> act = async () => await updateDemoThingUseCase.Handle(request, CancellationToken.None);

        //Assert
        await act.Should().ThrowAsync<DomainException>().Where(e => e.Message.Equals(expectedError.Message));
    }

    [Fact]
    public async Task DemoThing_is_not_updated_if_description_is_not_provided()
    {
        //Arrange
        var expectedError = new DomainException(
            nameof(CoreResource.Required),
            typeof(CoreResource),
            new List<MessagePlaceholder> { MessagePlaceholder.CreateResource(SharedResource.DemoThing_Description, typeof(SharedResource)) }
        );
        var existedDemoThing = DemoThings.FirstOrDefault();
        var request = new UpdateDemoThingRequest(existedDemoThing.Id, "Updated Name", "", DemoThingType.DemoType2);

        //Act
        Func<Task> act = async () => await updateDemoThingUseCase.Handle(request, CancellationToken.None);

        //Assert
        await act.Should().ThrowAsync<DomainException>().Where(e => e.Message.Equals(expectedError.Message));
    }

    [Fact]
    public async Task DemoThing_is_not_updated_if_id_not_existed()
    {
        //Arrange
        var notExistedId = Guid.NewGuid().ToString();
        var request = new UpdateDemoThingRequest(notExistedId, "Updated Name", "Updated Description", DemoThingType.DemoType2);
        var expectedError = new ApplicationException(
            nameof(CoreResource.Record_Not_Found),
            typeof(CoreResource),
            new List<MessagePlaceholder>
            {
                MessagePlaceholder.CreateResource(SharedResource.DemoThing, typeof(SharedResource)),
                MessagePlaceholder.CreatePlainText(notExistedId),
            }
        );

        //Act
        Func<Task> act = async () => await updateDemoThingUseCase.Handle(request, CancellationToken.None);

        //Assert
        await act.Should().ThrowAsync<ApplicationException>().Where(e => e.Message.Equals(expectedError.Message));
    }

    protected override void TestClassFixtureSetup()
    {
        var logger = Substitute.For<ILogger<UpdateDemoThingUseCase>>();
        updateDemoThingUseCase = new UpdateDemoThingUseCase(DemoThingRepository, RequestDispatcher, logger);
    }
}
