using System.ComponentModel.DataAnnotations;
using CanBeYours.Core.Domain.DemoThings;
using CanBeYours.Core.Resources;
using CodeBlock.DevKit.Core.Resources;

namespace CanBeYours.Application.Dtos.DemoThings;

/// <summary>
/// Data Transfer Object for creating a new DemoThing entity.
/// This class demonstrates how to create DTOs with validation attributes and resource-based localization.
/// The DemoThing functionality shown here is just an example to help you learn how to implement
/// your own unique features into the current codebase.
/// </summary>
public class CreateDemoThingDto
{
    /// <summary>
    /// The name of the demo thing. This field is required and will be displayed using localized resources.
    /// </summary>
    [Display(Name = nameof(SharedResource.DemoThing_Name), ResourceType = typeof(SharedResource))]
    [Required(ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.DemoThing_Name))]
    public string Name { get; set; }

    /// <summary>
    /// The description of the demo thing. This field is required and will be displayed using localized resources.
    /// </summary>
    [Display(Name = nameof(SharedResource.DemoThing_Description), ResourceType = typeof(SharedResource))]
    [Required(ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.DemoThing_Description))]
    public string Description { get; set; }

    /// <summary>
    /// The type of the demo thing. This field is required and determines the category of the demo thing.
    /// </summary>
    [Display(Name = nameof(SharedResource.DemoThing_Type), ResourceType = typeof(SharedResource))]
    [Required(ErrorMessageResourceName = nameof(CoreResource.Required), ErrorMessageResourceType = typeof(CoreResource))]
    public DemoThingType Type { get; set; }
}
