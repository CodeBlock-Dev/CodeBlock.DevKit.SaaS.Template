using CanBeYours.Infrastructure;
using CodeBlock.DevKit.Clients.AdminPanel;

namespace CanBeYours.AdminPanel;

/// <summary>
/// Static class containing extension methods for configuring the AdminPanel application.
/// This demonstrates the recommended pattern for organizing startup configuration in the CodeBlock.DevKit template.
/// The current functionality is just for learning purposes to show you how to implement
/// your own unique features into the existing codebase.
/// </summary>
internal static class Startup
{
    /// <summary>
    /// Configures application services by adding the AdminPanel client module and infrastructure module.
    /// This extension method provides a clean way to organize service configuration.
    /// </summary>
    /// <param name="builder">The WebApplicationBuilder instance</param>
    /// <returns>Configured WebApplication instance</returns>
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.AddAdminPanelClientModule(typeof(Startup));
        builder.Services.AddInfrastructureModule();

        return builder.Build();
    }

    /// <summary>
    /// Configures the application pipeline by using the AdminPanel client module and infrastructure module.
    /// This extension method provides a clean way to organize middleware configuration.
    /// </summary>
    /// <param name="app">The WebApplication instance</param>
    /// <returns>Configured WebApplication instance</returns>
    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        app.UseAdminPanelClientModule();
        app.Services.UseInfrastructureModule();

        return app;
    }
}
