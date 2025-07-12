using CanBeYours.Infrastructure.DbMigrations;
using CodeBlock.DevKit.Infrastructure.Database.Migrations;
using Microsoft.Extensions.DependencyInjection;

namespace CanBeYours.Infrastructure.DbContext;

internal static class DemoThingsDbMigrator
{
    public static void MigrateDatabes(this IServiceProvider serviceProvider)
    {
        using var serviceScope = serviceProvider.CreateScope();
        var dbContext = serviceScope.ServiceProvider.GetService<DemoThingsDbContext>();
        var dbMigrationRunner = serviceScope.ServiceProvider.GetService<IDbMigrationRunner>();

        var migrations = new List<IDbMigration>
        {
            new RenameSummaryToDescription(dbContext),
            // Add more migrations here as needed
        };

        foreach (var migration in migrations)
        {
            dbMigrationRunner.ApplyMigration(migration);
        }
    }
}
