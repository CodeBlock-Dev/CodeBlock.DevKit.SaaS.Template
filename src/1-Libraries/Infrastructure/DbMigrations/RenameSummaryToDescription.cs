// Copyright (c) CodeBlock.Dev. All rights reserved.
// For more information visit https://codeblock.dev

using CanBeYours.Core.Domain.DemoThings;
using CanBeYours.Infrastructure.DbContext;
using CodeBlock.DevKit.Infrastructure.Database.Migrations;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CanBeYours.Infrastructure.DbMigrations;

internal class RenameSummaryToDescription : IDbMigration
{
    private readonly DemoThingsDbContext _dbContext;

    public RenameSummaryToDescription(DemoThingsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public string UniqueName => "1_Rename_Summary_To_Description";

    public string Description => "Renames the 'Summary' property to 'Description' in the DemoThings collection.";

    public void Up()
    {
        var settings = _dbContext.GetCollection(nameof(DemoThingsDbContext.DemoThings));
        var filter = Builders<BsonDocument>.Filter.Exists("Summary");

        var update = Builders<BsonDocument>.Update.Rename("Summary", nameof(DemoThing.Description));

        settings.UpdateMany(filter, update);
    }
}
