using System.ComponentModel.DataAnnotations;
using CanBeYours.Core.Domain.DemoThings;
using CanBeYours.Core.Resources;
using CodeBlock.DevKit.Core.Resources;

namespace CanBeYours.Application.Dtos;

/// <summary>
/// Data Transfer Object for updating an existing DemoThing entity.
/// This class demonstrates how to create update DTOs with validation attributes and resource-based localization.
/// The DemoThing functionality shown here is just an example to help you learn how to implement
/// your own unique features into the current codebase.
/// </summary>
public class UpdateDemoThingDto
{
    /// <summary>
    /// The new name for the demo thing. This field is required and will be displayed using localized resources.
    /// </summary>
    [Display(Name = nameof(SharedResource.DemoThing_Name), ResourceType = typeof(SharedResource))]
    [Required(ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.DemoThing_Name))]
    public string Name { get; set; }

    /// <summary>
    /// The new description for the demo thing. This field is required and will be displayed using localized resources.
    /// </summary>
    [Display(Name = nameof(SharedResource.DemoThing_Description), ResourceType = typeof(SharedResource))]
    [Required(ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.DemoThing_Description))]
    public string Description { get; set; }

    /// <summary>
    /// The new type for the demo thing. This field is required and determines the category of the demo thing.
    /// </summary>
    [Display(Name = nameof(SharedResource.DemoThing_Type), ResourceType = typeof(SharedResource))]
    [Required(ErrorMessageResourceName = nameof(CoreResource.Required), ErrorMessageResourceType = typeof(CoreResource))]
    public DemoThingType Type { get; set; }
}
