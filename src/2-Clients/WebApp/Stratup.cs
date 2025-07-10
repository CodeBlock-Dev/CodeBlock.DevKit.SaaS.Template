using CodeBlock.DevKit.Clients.Website;

namespace CanBeYours.WebApp;

internal static class Startup
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.AddWebsiteClientModule(typeof(Startup));

        return builder.Build();
    }

    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        app.UseWebsiteClientModule();

        return app;
    }
}
