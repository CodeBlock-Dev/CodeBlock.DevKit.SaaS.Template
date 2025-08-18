using CanBeYours.Infrastructure.DbMigrations;
using CodeBlock.DevKit.Infrastructure.Database.Migrations;
using Microsoft.Extensions.DependencyInjection;

namespace CanBeYours.Infrastructure.DbContext;

/// <summary>
/// Database migration runner that applies database schema changes in order.
/// This class demonstrates how to manage database schema evolution through
/// versioned migrations that can be applied safely to production databases.
/// 
/// IMPORTANT: This is an example implementation for learning purposes. Replace the
/// DemoThing migrations with your actual business domain schema migrations.
/// 
/// Key features demonstrated:
/// - Ordered migration execution
/// - Migration dependency management
/// - Safe database schema updates
/// - Service scope management for migrations
/// </summary>
internal static class DbMigrator
{
    /// <summary>
    /// Applies all pending database migrations in the correct order.
    /// This method ensures that database schema changes are applied safely
    /// and in the proper sequence during application startup.
    /// 
    /// Example usage in startup:
    /// serviceProvider.MigrateDatabes();
    /// </summary>
    /// <param name="serviceProvider">The service provider to access required services</param>
    public static void MigrateDatabes(this IServiceProvider serviceProvider)
    {
        using var serviceScope = serviceProvider.CreateScope();
        var dbContext = serviceScope.ServiceProvider.GetService<MainDbContext>();
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
