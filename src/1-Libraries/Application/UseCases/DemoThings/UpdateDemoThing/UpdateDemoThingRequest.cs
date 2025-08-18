using System.ComponentModel.DataAnnotations;
using CanBeYours.Core.Domain.DemoThings;
using CanBeYours.Core.Resources;
using CodeBlock.DevKit.Application.Commands;
using CodeBlock.DevKit.Core.Resources;

namespace CanBeYours.Application.UseCases.DemoThings.UpdateDemoThing;

/// <summary>
/// Command request for updating an existing DemoThing entity.
/// This class demonstrates how to implement update command requests with validation attributes,
/// immutable properties, and proper resource-based localization.
/// The DemoThing functionality shown here is just an example to help you learn how to implement
/// your own unique features into the current codebase.
/// </summary>
internal class UpdateDemoThingRequest : BaseCommand
{
    /// <summary>
    /// Initializes a new instance of the UpdateDemoThingRequest with the required data.
    /// </summary>
    /// <param name="id">The unique identifier of the demo thing to update</param>
    /// <param name="name">The new name for the demo thing</param>
    /// <param name="description">The new description for the demo thing</param>
    /// <param name="type">The new type/category for the demo thing</param>
    public UpdateDemoThingRequest(string id, string name, string description, DemoThingType type)
    {
        Id = id;
        Name = name;
        Description = description;
        Type = type;
    }

    /// <summary>
    /// The unique identifier of the demo thing to update.
    /// </summary>
    public string Id { get; }

    /// <summary>
    /// The new name for the demo thing. This field is required and will be displayed using localized resources.
    /// </summary>
    [Display(Name = nameof(SharedResource.DemoThing_Name), ResourceType = typeof(SharedResource))]
    [Required(ErrorMessageResourceName = nameof(CoreResource.Required), ErrorMessageResourceType = typeof(CoreResource))]
    public string Name { get; }

    /// <summary>
    /// The new description for the demo thing. This field is required and will be displayed using localized resources.
    /// </summary>
    [Display(Name = nameof(SharedResource.DemoThing_Description), ResourceType = typeof(SharedResource))]
    [Required(ErrorMessageResourceName = nameof(CoreResource.Required), ErrorMessageResourceType = typeof(CoreResource))]
    public string Description { get; }

    /// <summary>
    /// The new type for the demo thing. This field is required and determines the category of the demo thing.
    /// </summary>
    [Display(Name = nameof(SharedResource.DemoThing_Type), ResourceType = typeof(SharedResource))]
    [Required(ErrorMessageResourceName = nameof(CoreResource.Required), ErrorMessageResourceType = typeof(CoreResource))]
    public DemoThingType Type { get; }
}
