using System.ComponentModel.DataAnnotations;
using CanBeYours.Core.Domain.DemoThings;
using CanBeYours.Core.Resources;
using CodeBlock.DevKit.Application.Commands;
using CodeBlock.DevKit.Core.Resources;

namespace CanBeYours.Application.UseCases.DemoThings.CreateDemoThing;

/// <summary>
/// Command request for creating a new DemoThing entity.
/// This class demonstrates how to implement command requests with validation attributes,
/// immutable properties, and proper resource-based localization.
/// The DemoThing functionality shown here is just an example to help you learn how to implement
/// your own unique features into the current codebase.
/// </summary>
internal class CreateDemoThingRequest : BaseCommand
{
    /// <summary>
    /// Initializes a new instance of the CreateDemoThingRequest with the required data.
    /// </summary>
    /// <param name="name">The name of the demo thing</param>
    /// <param name="description">The description of the demo thing</param>
    /// <param name="type">The type/category of the demo thing</param>
    public CreateDemoThingRequest(string name, string description, DemoThingType type)
    {
        Name = name;
        Description = description;
        Type = type;
    }

    /// <summary>
    /// The name of the demo thing. This field is required and will be displayed using localized resources.
    /// </summary>
    [Display(Name = nameof(SharedResource.DemoThing_Name), ResourceType = typeof(SharedResource))]
    [Required(ErrorMessageResourceName = nameof(CoreResource.Required), ErrorMessageResourceType = typeof(CoreResource))]
    public string Name { get; }

    /// <summary>
    /// The description of the demo thing. This field is required and will be displayed using localized resources.
    /// </summary>
    [Display(Name = nameof(SharedResource.DemoThing_Description), ResourceType = typeof(SharedResource))]
    [Required(ErrorMessageResourceName = nameof(CoreResource.Required), ErrorMessageResourceType = typeof(CoreResource))]
    public string Description { get; }

    /// <summary>
    /// The type of the demo thing. This field is required and determines the category of the demo thing.
    /// </summary>
    [Display(Name = nameof(SharedResource.DemoThing_Type), ResourceType = typeof(SharedResource))]
    [Required(ErrorMessageResourceName = nameof(CoreResource.Required), ErrorMessageResourceType = typeof(CoreResource))]
    public DemoThingType Type { get; }
}
