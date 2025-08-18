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

/// <summary>
/// Unit tests for the CreateDemoThing use case that verify business logic and validation rules.
/// This test class demonstrates how to write comprehensive unit tests for command handlers
/// including success scenarios, validation failures, and domain event publishing. The DemoThing
/// functionality shown here is just an example to help you learn how to implement your own
/// unique features into the current codebase. You can use this testing pattern to verify your
/// actual business logic works correctly in isolation.
/// </summary>
public class CreateDemoThingTests : TestsBaseFixture
{
    /// <summary>
    /// The use case instance being tested
    /// </summary>
    private CreateDemoThingUseCase createDemoThingUseCase;

    /// <summary>
    /// Default constructor for the test class
    /// </summary>
    public CreateDemoThingTests() { }

    /// <summary>
    /// Tests that a DemoThing entity can be successfully created with valid input.
    /// This test verifies the happy path scenario where all required data is provided
    /// and the entity is created, persisted, and domain events are published.
    /// Example: Creating a new product, user, or any business entity with valid data.
    /// </summary>
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

    /// <summary>
    /// Tests that a DemoThing entity cannot be created when the name is empty or null.
    /// This test verifies that the domain validation rules are enforced and appropriate
    /// domain exceptions are thrown for invalid input. Example: Attempting to create
    /// a user without a username or a product without a name.
    /// </summary>
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

    /// <summary>
    /// Tests that a DemoThing entity cannot be created when the description is empty or null.
    /// This test verifies that the domain validation rules are enforced and appropriate
    /// domain exceptions are thrown for invalid input. Example: Attempting to create
    /// a product without a description or a document without content.
    /// </summary>
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

    /// <summary>
    /// Sets up the test-specific fixture by creating the use case instance with mocked dependencies.
    /// This method is called by the base fixture setup to prepare the specific test environment.
    /// Example: Creating a service instance with mocked repositories and external dependencies.
    /// </summary>
    protected override void TestClassFixtureSetup()
    {
        var logger = Substitute.For<ILogger<CreateDemoThingUseCase>>();

        createDemoThingUseCase = new CreateDemoThingUseCase(DemoThingRepository, RequestDispatcher, logger, CurrentUser);
    }
}
