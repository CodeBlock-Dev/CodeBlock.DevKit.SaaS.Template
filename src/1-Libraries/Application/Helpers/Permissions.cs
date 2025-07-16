// Copyright (c) CodeBlock.Dev. All rights reserved.
// For more information visit https://codeblock.dev

using System.Reflection;
using CodeBlock.DevKit.Contracts.Models;

namespace CanBeYours.Application.Helpers;

/// <summary>
/// This class contains all permissions used in the application.
/// They will be automatically registered in the database.
/// </summary>
public static class Permissions
{
    /// <summary>
    /// This class name is used as a group name for permissions related to demo things.
    /// </summary>
    public static class Demo
    {
        /// <summary>
        /// This property represents the permission to manage demo things.
        /// </summary>
        public const string DEMO_THINGS = "DemoThings";
    }

    /// <summary>
    /// Retrieves all permissions dynamically using reflection.
    /// </summary>
    public static IEnumerable<PermissionsSettings> GetPermissions()
    {
        var permissionsList = new List<PermissionsSettings>();

        // Get all nested classes (categories)
        var categories = typeof(Permissions).GetNestedTypes(BindingFlags.Public | BindingFlags.Static);

        foreach (var category in categories)
        {
            // Get all public constants (permissions) inside the category
            var permissions = category
                .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
                .Where(f => f.IsLiteral && !f.IsInitOnly) // Ensure they are constants
                .Select(f => new
                {
                    SystemName = f.GetRawConstantValue()?.ToString(), // Property value
                    DisplayName = f.GetRawConstantValue()?.ToString(),
                })
                .Where(p => p.DisplayName != null)
                .ToList();

            // Add extracted permissions to the list
            permissionsList.AddRange(
                permissions.Select(permission => new PermissionsSettings
                {
                    SystemName = permission.SystemName,
                    DisplayName = permission.DisplayName,
                    GroupName = category.Name,
                })
            );
        }

        return permissionsList;
    }
}
