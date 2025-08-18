using CanBeYours.Infrastructure;
using CodeBlock.DevKit.Clients.WebApp;

namespace CanBeYours.WebApp;

/// <summary>
/// Static configuration class for the WebApp client application.
/// This class demonstrates how to configure services and middleware pipeline using CodeBlock.DevKit.
/// The current setup shows a basic configuration - you can extend this with your own services,
/// authentication, logging, or other middleware while maintaining the same structure.
/// </summary>
internal static class Startup
{
    /// <summary>
    /// Configures the application services including WebApp client module and infrastructure.
    /// This method demonstrates how to add CodeBlock.DevKit modules and your own services.
    /// </summary>
    /// <param name="builder">The web application builder instance</param>
    /// <returns>Configured web application instance</returns>
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.AddWebAppClientModule(typeof(Startup));
        builder.Services.AddInfrastructureModule();

        return builder.Build();
    }

    /// <summary>
    /// Configures the application middleware pipeline including WebApp client module and infrastructure.
    /// This method demonstrates how to set up the request processing pipeline.
    /// </summary>
    /// <param name="app">The web application instance</param>
    /// <returns>Configured web application instance</returns>
    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        app.UseWebAppClientModule();
        app.Services.UseInfrastructureModule();
        return app;
    }
}
