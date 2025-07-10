using CodeBlock.DevKit.Clients.AdminPanel;

namespace CanBeYours.AdminPanel;

internal static class Startup
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.AddAdminPanelClientModule(typeof(Startup));

        return builder.Build();
    }

    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        app.UseAdminPanelClientModule();

        return app;
    }
}
