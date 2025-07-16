using CanBeYours.Core.Domain.DemoThings;
using CodeBlock.DevKit.Identity.Domain.Users;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace CanBeYours.Infrastructure.DbContext;

internal static class DemoThingsDbSeeder
{
    public static void SeedSampleDemoThings(this IServiceProvider serviceProvider)
    {
        using var serviceScope = serviceProvider.CreateScope();

        serviceScope.SeedDemoThings();
    }

    private static void SeedDemoThings(this IServiceScope serviceScope)
    {
        var dbContext = serviceScope.ServiceProvider.GetService<MainDbContext>();

        if (dbContext.DemoThings.Find(_ => true).Any())
            return;

        var userRepository = serviceScope.ServiceProvider.GetRequiredService<IUserRepository>();
        var demoThingRepository = serviceScope.ServiceProvider.GetRequiredService<IDemoThingRepository>();

        var user = userRepository.GetListAsync().GetAwaiter().GetResult().First();

        var demoThings = new List<DemoThing>
        {
            DemoThing.Create("Alpha Widget", "A simple starter widget.", DemoThingType.DemoType1, user.Id),
            DemoThing.Create("Beta Module", "Second-generation example module.", DemoThingType.DemoType1, user.Id),
            DemoThing.Create("Gamma Tool", "Helper tool for demos.", DemoThingType.DemoType3, user.Id),
            DemoThing.Create("Delta Feature", "Experimental feature for testing.", DemoThingType.DemoType1, user.Id),
            DemoThing.Create("Epsilon Block", "A UI block used in layouts.", DemoThingType.DemoType2, user.Id),
            DemoThing.Create("Zeta Component", "Blazor component example.", DemoThingType.DemoType2, user.Id),
            DemoThing.Create("Eta Job", "Background job simulator.", DemoThingType.DemoType1, user.Id),
            DemoThing.Create("Theta Report", "Analytics report generator.", DemoThingType.DemoType1, user.Id),
            DemoThing.Create("Iota API", "Sample REST endpoint scaffold.", DemoThingType.DemoType2, user.Id),
            DemoThing.Create("Kappa Function", "Business rule executor.", DemoThingType.DemoType3, user.Id),
        };

        foreach (var thing in demoThings)
        {
            // We recreate the demo things only to add a delay and have different creation time for each.
            var demoThing = DemoThing.Create(thing.Name, thing.Description, thing.Type, thing.UserId);
            demoThingRepository.Add(thing);
            Thread.Sleep(10);
        }
    }
}
