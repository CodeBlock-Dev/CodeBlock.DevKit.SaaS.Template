using CanBeYours.Application.Helpers;
using CodeBlock.DevKit.Administration.Domain.Permissions;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace CanBeYours.Infrastructure.DbContext;

/// <summary>
/// Database seeder that initializes the database with required seed data.
/// This class demonstrates how to populate the database with initial permissions
/// and other essential data during application startup.
/// 
/// IMPORTANT: This is an example implementation for learning purposes. Replace the
/// permission seeding logic with your actual business domain seed data requirements.
/// 
/// Key features demonstrated:
/// - Permission seeding from application configuration
/// - Safe seeding that prevents duplicate data
/// - Service scope management for database operations
/// </summary>
internal static class DbSeeder
{
    /// <summary>
    /// Seeds the database with initial permissions defined in the application.
    /// This method ensures that all required permissions exist in the database
    /// and prevents duplicate permission creation.
    /// 
    /// Example usage in startup:
    /// serviceProvider.SeedPermissions();
    /// </summary>
    /// <param name="serviceProvider">The service provider to access required services</param>
    public static void SeedPermissions(this IServiceProvider serviceProvider)
    {
        using var serviceScope = serviceProvider.CreateScope();
        var permissionRepository = serviceScope.ServiceProvider.GetService<IPermissionRepository>();

        var permissions = permissionRepository.GetListAsync().GetAwaiter().GetResult();

        foreach (var permission in Permissions.GetPermissions())
        {
            // Check if the permission already exists
            if (permissions.Any(p => p.SystemName == permission.SystemName))
                continue;

            var newPermission = Permission.Create(permissionRepository, permission.DisplayName, permission.SystemName, permission.GroupName);
            permissionRepository.Add(newPermission);
        }
    }
}
