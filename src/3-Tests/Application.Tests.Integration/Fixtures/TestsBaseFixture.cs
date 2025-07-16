using AutoMapper;
using CanBeYours.Core.Domain.DemoThings;
using CanBeYours.Infrastructure;
using CodeBlock.DevKit.Application.Srvices;
using CodeBlock.DevKit.Clients.AdminPanel;
using CodeBlock.DevKit.Test.TestBase;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace CanBeYours.Application.Tests.Integration.Fixtures;

public abstract class TestsBaseFixture : IntegrationTestsBase
{
    public readonly IRequestDispatcher _requestDispatcher;
    public readonly IMapper _mapper;
    public readonly IDemoThingRepository _demoThingRepository;
    public readonly ICurrentUser _currentUser;
    public readonly ILogger _logger;

    protected TestsBaseFixture(string dbNameSuffix)
        : base(dbNameSuffix)
    {
        _demoThingRepository = GetRequiredService<IDemoThingRepository>();
        _mapper = GetRequiredService<IMapper>();
        _currentUser = GetRequiredService<ICurrentUser>();
        _logger = GetRequiredService<ILogger>();
        _requestDispatcher = Substitute.For<IRequestDispatcher>();
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

    /// <summary>
    ///
    /// </summary>
    public override IServiceProvider GetServiceProvider(string dbNameSuffix)
    {
        var services = new ServiceCollection();

        var configuration = new ConfigurationBuilder()
            //Copy from AdminPanel during the build event
            .AddJsonFile("appsettings.json", reloadOnChange: true, optional: false)
            .AddJsonFile("appsettings.Development.json", optional: true)
            .AddInMemoryCollection([new KeyValuePair<string, string?>("MongoDB:DatabaseName", $"Test_DemoThings_DB_{dbNameSuffix}")])
            .Build();

        services.AddSingleton<IConfiguration>(provider =>
        {
            return configuration;
        });

        var builder = WebApplication.CreateBuilder();

        builder.Services.Add(services);

        builder.AddAdminPanelClientModule(typeof(TestsBaseFixture));
        builder.Services.AddInfrastructureModule();

        return services.BuildServiceProvider();
    }
}
