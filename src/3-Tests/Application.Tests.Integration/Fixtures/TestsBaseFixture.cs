using AutoMapper;
using CanBeYours.Core.Domain.DemoThings;
using CanBeYours.Infrastructure;
using CodeBlock.DevKit.Application.Srvices;
using CodeBlock.DevKit.Clients.AdminPanel;
using CodeBlock.DevKit.Contracts.Services;
using CodeBlock.DevKit.Test.TestBase;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace CanBeYours.Application.Tests.Integration.Fixtures;

/// <summary>
/// Base test fixture for integration tests that provides common setup and utilities.
/// This class demonstrates how to create a robust test infrastructure for integration testing
/// with dependency injection, database setup, and mock services. The DemoThing functionality
/// shown here is just an example to help you learn how to implement your own unique features
/// into the current codebase. You can use this pattern to test your actual business logic.
/// </summary>
public abstract class TestsBaseFixture : IntegrationTestsBase
{
    public readonly IRequestDispatcher _requestDispatcher;
    public readonly IMapper _mapper;
    public readonly IDemoThingRepository _demoThingRepository;
    public readonly ICurrentUser _currentUser;
    public readonly IUserAccessorService _userAccessorService;

    /// <summary>
    /// Initializes the test fixture with a unique database suffix and sets up common services.
    /// This constructor demonstrates how to configure test dependencies and mock services
    /// for integration testing scenarios.
    /// </summary>
    /// <param name="dbNameSuffix">Unique suffix for the test database to ensure test isolation</param>
    protected TestsBaseFixture(string dbNameSuffix)
        : base(dbNameSuffix)
    {
        _demoThingRepository = GetRequiredService<IDemoThingRepository>();
        _userAccessorService = GetRequiredService<IUserAccessorService>();
        _mapper = GetRequiredService<IMapper>();
        _requestDispatcher = Substitute.For<IRequestDispatcher>();
        _currentUser = Substitute.For<ICurrentUser>();
        _currentUser.GetUserId().Returns(Guid.NewGuid().ToString());
    }

    /// <summary>
    /// Initializes the test database. Override this method if you need custom database setup.
    /// This method is called by the base class to prepare the database for testing.
    /// </summary>
    public override void InitialDatabase() { }

    /// <summary>
    /// Drops the test database after each test run to ensure clean test isolation.
    /// This method is used in the base class to clean up resources between tests.
    /// </summary>
    public override void DropDatabase()
    {
        _serviceProvider.DropTestDatabase();
    }

    /// <summary>
    /// Seeds the test database with a DemoThing entity for testing purposes.
    /// This method demonstrates how to prepare test data for integration tests.
    /// Example usage: await fixture.SeedDemoThingAsync(demoThing);
    /// </summary>
    /// <param name="demoThing">The DemoThing entity to seed in the test database</param>
    public async Task SeedDemoThingAsync(DemoThing demoThing)
    {
        await _demoThingRepository.AddAsync(demoThing);
    }

    /// <summary>
    /// Retrieves a DemoThing entity from the test database by its ID.
    /// This method is useful for verifying that entities were properly created or updated
    /// during test execution.
    /// Example usage: var result = await fixture.GetDemoThingAsync(entityId);
    /// </summary>
    /// <param name="id">The unique identifier of the DemoThing to retrieve</param>
    /// <returns>The DemoThing entity if found, null otherwise</returns>
    public async Task<DemoThing> GetDemoThingAsync(string id)
    {
        return await _demoThingRepository.GetByIdAsync(id);
    }

    /// <summary>
    /// Gets a required service from the test service provider.
    /// This method provides access to the configured services for testing purposes.
    /// </summary>
    /// <typeparam name="T">The type of service to retrieve</typeparam>
    /// <returns>The requested service instance</returns>
    public new T GetRequiredService<T>()
    {
        return _serviceProvider.GetRequiredService<T>();
    }

    /// <summary>
    /// Configures and builds the test service provider with all necessary dependencies.
    /// This method demonstrates how to set up a complete dependency injection container
    /// for integration testing, including database configuration, logging, and module registration.
    /// </summary>
    /// <param name="dbNameSuffix">Unique suffix for the test database name</param>
    /// <returns>A configured service provider ready for testing</returns>
    public override IServiceProvider GetServiceProvider(string dbNameSuffix)
    {
        var builder = WebApplication.CreateBuilder();

        // Configure the test configuration
        builder
            .Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile("appsettings.Development.json", optional: true)
            .AddInMemoryCollection([new("MongoDB:DatabaseName", $"Test_DemoThings_DB_{dbNameSuffix}")]);

        // Add logging services
        builder.Services.AddLogging(logging =>
        {
            logging.AddConsole();
            logging.SetMinimumLevel(LogLevel.Debug);
        });

        // Register AdminPanel client module
        builder.AddAdminPanelClientModule(typeof(TestsBaseFixture));

        // Register infrastructure services first
        builder.Services.AddInfrastructureModule();

        // Build the service provider
        return builder.Services.BuildServiceProvider();
    }
}
