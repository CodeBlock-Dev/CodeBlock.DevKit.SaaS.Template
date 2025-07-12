using CanBeYours.Infrastructure;
using CodeBlock.DevKit.Clients.AdminPanel;
using CodeBlock.DevKit.Infrastructure.Extensions;

namespace CanBeYours.AdminPanel;

internal static class Startup
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.AddAdminPanelClientModule(typeof(Startup));
        builder.Services.AddInfrastructureModule();
        builder.Services.RegisterHandlers(typeof(Startup));
        builder.Services.RegisterUIModule(typeof(Startup).Assembly);

        return builder.Build();
    }

    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        app.UseAdminPanelClientModule();
        app.Services.UseInfrastructureModule();
        return app;
    }
}
