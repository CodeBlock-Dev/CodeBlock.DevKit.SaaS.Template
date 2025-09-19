using CanBeYours.Core.Domain.DemoThings;
using CodeBlock.DevKit.Contracts.Dtos;

namespace CanBeYours.Application.Dtos.DemoThings;

/// <summary>
/// Data Transfer Object for retrieving DemoThing entity data.
/// This class demonstrates how to create response DTOs that extend base DTOs and include
/// additional properties for display purposes.
/// The DemoThing functionality shown here is just an example to help you learn how to implement
/// your own unique features into the current codebase.
/// </summary>
public class GetDemoThingDto : GetEntityDto
{
    /// <summary>
    /// The name of the demo thing for display purposes.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// The description of the demo thing for display purposes.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// The type/category of the demo thing.
    /// </summary>
    public DemoThingType Type { get; set; }

    /// <summary>
    /// The email address of the user who owns this demo thing.
    /// This is populated by the service layer for display purposes.
    /// </summary>
    public string UserEmail { get; set; }

    /// <summary>
    /// The unique identifier of the user who owns this demo thing.
    /// </summary>
    public string UserId { get; set; }
}
