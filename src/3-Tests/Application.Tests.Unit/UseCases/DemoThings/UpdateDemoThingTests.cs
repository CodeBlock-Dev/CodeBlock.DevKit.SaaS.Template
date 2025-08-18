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

/// <summary>
/// Unit tests for the UpdateDemoThing use case that verify business logic, validation rules, and error handling.
/// This test class demonstrates how to write comprehensive unit tests for update command handlers
/// including success scenarios, validation failures, entity existence checks, and domain event publishing.
/// The DemoThing functionality shown here is just an example to help you learn how to implement your own
/// unique features into the current codebase. You can use this testing pattern to verify your actual
/// business logic works correctly in isolation.
/// </summary>
public class UpdateDemoThingTests : TestsBaseFixture
{
    /// <summary>
    /// The use case instance being tested
    /// </summary>
    private UpdateDemoThingUseCase updateDemoThingUseCase;

    /// <summary>
    /// Default constructor for the test class
    /// </summary>
    public UpdateDemoThingTests() { }

    /// <summary>
    /// Tests that a DemoThing entity can be successfully updated with valid input.
    /// This test verifies the happy path scenario where an existing entity is updated,
    /// persisted, and domain events are published. Example: Updating a product name,
    /// user profile, or any business entity with valid data.
    /// </summary>
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

    /// <summary>
    /// Tests that a DemoThing entity cannot be updated when the name is empty or null.
    /// This test verifies that the domain validation rules are enforced and appropriate
    /// domain exceptions are thrown for invalid input. Example: Attempting to update
    /// a product with an empty name or a user without a username.
    /// </summary>
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

    /// <summary>
    /// Tests that a DemoThing entity cannot be updated when the description is empty or null.
    /// This test verifies that the domain validation rules are enforced and appropriate
    /// domain exceptions are thrown for invalid input. Example: Attempting to update
    /// a product with an empty description or a document without content.
    /// </summary>
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

    /// <summary>
    /// Tests that a DemoThing entity cannot be updated when the specified ID does not exist.
    /// This test verifies that the application layer properly handles entity not found scenarios
    /// and throws appropriate application exceptions. Example: Attempting to update a user
    /// that has been deleted or a product that doesn't exist in the system.
    /// </summary>
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

    /// <summary>
    /// Sets up the test-specific fixture by creating the use case instance with mocked dependencies.
    /// This method is called by the base fixture setup to prepare the specific test environment.
    /// Example: Creating a service instance with mocked repositories and external dependencies.
    /// </summary>
    protected override void TestClassFixtureSetup()
    {
        var logger = Substitute.For<ILogger<UpdateDemoThingUseCase>>();
        updateDemoThingUseCase = new UpdateDemoThingUseCase(DemoThingRepository, RequestDispatcher, logger);
    }
}
