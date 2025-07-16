using CanBeYours.Infrastructure;
using CodeBlock.DevKit.Clients.Api;

namespace CanBeYours.Api;

internal static class Startup
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.AddApiClientModule(typeof(Startup));
        builder.Services.AddInfrastructureModule();

        return builder.Build();
    }

    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        app.UseApiClientModule();
        app.Services.UseInfrastructureModule(app.Environment.IsDevelopment());

        return app;
    }
}
