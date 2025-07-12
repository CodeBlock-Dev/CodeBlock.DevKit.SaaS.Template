using CanBeYours.Core.Domain.DemoThings;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace CanBeYours.Infrastructure.DbContext;

internal static class DemoThingsDbSeeder
{
    public static void SeedSampleDemoThings(this IServiceProvider serviceProvider)
    {
        using var serviceScope = serviceProvider.CreateScope();
        var dbContext = serviceScope.ServiceProvider.GetService<DemoThingsDbContext>();

        serviceScope.SeedDemoThings(dbContext);
    }

    private static void SeedDemoThings(this IServiceScope serviceScope, DemoThingsDbContext dbContext)
    {
        if (dbContext.DemoThings.Find(_ => true).Any())
            return;

        var demoThingRepository = serviceScope.ServiceProvider.GetRequiredService<IDemoThingRepository>();

        var demoThings = new List<DemoThing>
        {
            DemoThing.Create("Alpha Widget", "A simple starter widget."),
            DemoThing.Create("Beta Module", "Second-generation example module."),
            DemoThing.Create("Gamma Tool", "Helper tool for demos."),
            DemoThing.Create("Delta Feature", "Experimental feature for testing."),
            DemoThing.Create("Epsilon Block", "A UI block used in layouts."),
            DemoThing.Create("Zeta Component", "Blazor component example."),
            DemoThing.Create("Eta Job", "Background job simulator."),
            DemoThing.Create("Theta Report", "Analytics report generator."),
            DemoThing.Create("Iota API", "Sample REST endpoint scaffold."),
            DemoThing.Create("Kappa Function", "Business rule executor."),
        };

        foreach (var thing in demoThings)
            demoThingRepository.Add(thing);
    }
}
