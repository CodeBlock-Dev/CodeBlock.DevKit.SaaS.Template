using CanBeYours.Infrastructure;
using CodeBlock.DevKit.Clients.Api;

namespace CanBeYours.Api;

/// <summary>
/// Static class responsible for configuring services and pipeline for the API application.
/// This class demonstrates the standard pattern for organizing startup configuration
/// and serves as an example of how to structure your own startup logic.
/// </summary>
internal static class Startup
{
    /// <summary>
    /// Configures and registers all required services for the application.
    /// Adds API client module and infrastructure services to the service collection.
    /// </summary>
    /// <param name="builder">WebApplicationBuilder instance</param>
    /// <returns>Configured WebApplication instance</returns>
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.AddApiClientModule(typeof(Startup));
        builder.Services.AddInfrastructureModule();

        return builder.Build();
    }

    /// <summary>
    /// Configures the HTTP request pipeline for the application.
    /// Sets up middleware and infrastructure modules in the correct order.
    /// </summary>
    /// <param name="app">WebApplication instance</param>
    /// <returns>Configured WebApplication instance</returns>
    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        app.UseApiClientModule();
        app.Services.UseInfrastructureModule();

        return app;
    }
}
