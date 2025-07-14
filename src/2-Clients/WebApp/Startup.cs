using CanBeYours.Infrastructure;
using CodeBlock.DevKit.Application.Extensions;
using CodeBlock.DevKit.Clients.WebApp;

namespace CanBeYours.WebApp;

internal static class Startup
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.AddWebAppClientModule(typeof(Startup));
        builder.Services.AddInfrastructureModule();
        builder.Services.RegisterHandlers(typeof(Startup));
        builder.Services.RegisterUIModule(typeof(Startup).Assembly);

        return builder.Build();
    }

    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        app.UseWebAppClientModule();
        app.Services.UseInfrastructureModule();
        return app;
    }
}
