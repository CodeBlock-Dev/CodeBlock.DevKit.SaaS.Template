using CanBeYours.Application.Services.DemoThings;
using Microsoft.Extensions.DependencyInjection;

namespace CanBeYours.Application;

public static class Startup
{
    public static void AddApplicationModule(this IServiceCollection services)
    {
        //services.RegisterHandlers(typeof(Startup));

        services.AddScoped<IDemoThingService, DemoThingService>();
    }
}
