using AutoMapper;
using CanBeYours.Core.Domain.DemoThings;
using CanBeYours.Infrastructure;
using CodeBlock.DevKit.Application.Srvices;
using CodeBlock.DevKit.Clients.AdminPanel;
using CodeBlock.DevKit.Test.TestBase;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace CanBeYours.Application.Tests.Integration.Fixtures;

public abstract class TestsBaseFixture : IntegrationTestsBase
{
    public readonly IRequestDispatcher _requestDispatcher;
    public readonly IMapper _mapper;
    public readonly IDemoThingRepository _demoThingRepository;
    public readonly ICurrentUser _currentUser;

    protected TestsBaseFixture(string dbNameSuffix)
        : base(dbNameSuffix)
    {
        _demoThingRepository = GetRequiredService<IDemoThingRepository>();
        _mapper = GetRequiredService<IMapper>();
        _requestDispatcher = Substitute.For<IRequestDispatcher>();

        _currentUser = Substitute.For<ICurrentUser>();
        _currentUser.GetUserId().Returns(Guid.NewGuid().ToString());
    }

    /// <summary>
    ///
    /// </summary>
    public override void InitialDatabase() { }

    /// <summary>
    ///
    /// </summary>
    public override void DropDatabase()
    {
        _serviceProvider.DropTestDatabase();
    }

    /// <summary>
    ///
    /// </summary>
    public async Task SeedDemoThingAsync(DemoThing demoThing)
    {
        await _demoThingRepository.AddAsync(demoThing);
    }

    /// <summary>
    ///
    /// </summary>
    public async Task<DemoThing> GetDemoThingAsync(string id)
    {
        return await _demoThingRepository.GetByIdAsync(id);
    }

    public new T GetRequiredService<T>()
    {
        return _serviceProvider.GetRequiredService<T>();
    }

    /// <summary>
    ///
    /// </summary>
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
