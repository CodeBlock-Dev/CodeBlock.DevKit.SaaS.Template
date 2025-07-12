using CanBeYours.Infrastructure;
using CodeBlock.DevKit.Clients.Api;
using CodeBlock.DevKit.Infrastructure.Extensions;

namespace CanBeYours.Api;

internal static class Startup
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.AddApiClientModule(typeof(Startup));
        builder.Services.AddInfrastructureModule();
        builder.Services.RegisterHandlers(typeof(Startup));
        builder.Services.RegisterApiModule(typeof(Startup).Assembly);

        return builder.Build();
    }

    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        app.UseApiClientModule();
        app.Services.UseInfrastructureModule();

        return app;
    }
}
