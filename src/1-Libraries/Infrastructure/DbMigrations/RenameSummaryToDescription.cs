// Copyright (c) CodeBlock.Dev. All rights reserved.
// For more information visit https://codeblock.dev

using CanBeYours.Core.Domain.DemoThings;
using CanBeYours.Infrastructure.DbContext;
using CodeBlock.DevKit.Infrastructure.Database.Migrations;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CanBeYours.Infrastructure.DbMigrations;

/// <summary>
/// Database migration that renames the 'Summary' field to 'Description' in the DemoThings collection.
/// This class demonstrates how to implement database migrations for schema evolution,
/// showing how to safely update existing data structures without data loss.
/// 
/// IMPORTANT: This is an example implementation for learning purposes. Replace this migration
/// with your actual business domain schema changes and migrations.
/// 
/// Key features demonstrated:
/// - Database migration pattern implementation
/// - Safe field renaming using MongoDB operations
/// - Migration versioning and identification
/// - Database context integration
/// </summary>
internal class RenameSummaryToDescription : IDbMigration
{
    private readonly MainDbContext _dbContext;

    /// <summary>
    /// Initializes a new instance of the migration with the database context.
    /// </summary>
    /// <param name="dbContext">The database context for accessing collections</param>
    public RenameSummaryToDescription(MainDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    /// <summary>
    /// Unique identifier for this migration to prevent duplicate execution.
    /// This property ensures that the migration runs only once and can be
    /// tracked in migration history.
    /// </summary>
    public string UniqueName => "1_Rename_Summary_To_Description";

    /// <summary>
    /// Human-readable description of what this migration accomplishes.
    /// This description helps developers understand the purpose and scope
    /// of the migration during deployment and troubleshooting.
    /// </summary>
    public string Description => "Renames the 'Summary' property to 'Description' in the DemoThings collection.";

    /// <summary>
    /// Executes the migration to rename the 'Summary' field to 'Description'.
    /// This method demonstrates how to perform safe schema changes on existing
    /// MongoDB collections using the UpdateMany operation.
    /// 
    /// The migration:
    /// 1. Finds all documents that have a 'Summary' field
    /// 2. Renames the field to 'Description' without losing data
    /// 3. Uses BsonDocument operations for field-level changes
    /// </summary>
    public void Up()
    {
        var settings = _dbContext.GetCollection(nameof(MainDbContext.DemoThings));
        var filter = Builders<BsonDocument>.Filter.Exists("Summary");

        var update = Builders<BsonDocument>.Update.Rename("Summary", nameof(DemoThing.Description));

        settings.UpdateMany(filter, update);
    }
}
