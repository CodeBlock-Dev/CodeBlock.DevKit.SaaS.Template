using System.ComponentModel.DataAnnotations;
using CanBeYours.Core.Domain.DemoThings;
using CanBeYours.Core.Resources;
using CodeBlock.DevKit.Application.Commands;
using CodeBlock.DevKit.Core.Resources;

namespace CanBeYours.Application.UseCases.DemoThings.UpdateDemoThing;

internal class UpdateDemoThingRequest : BaseCommand
{
    public UpdateDemoThingRequest(string id, string name, string description, DemoThingType type)
    {
        Id = id;
        Name = name;
        Description = description;
        Type = type;
    }

    public string Id { get; }

    [Display(Name = nameof(SharedResource.DemoThing_Name), ResourceType = typeof(SharedResource))]
    [Required(ErrorMessageResourceName = nameof(CoreResource.Required), ErrorMessageResourceType = typeof(CoreResource))]
    public string Name { get; }

    [Display(Name = nameof(SharedResource.DemoThing_Description), ResourceType = typeof(SharedResource))]
    [Required(ErrorMessageResourceName = nameof(CoreResource.Required), ErrorMessageResourceType = typeof(CoreResource))]
    public string Description { get; }

    [Display(Name = nameof(SharedResource.DemoThing_Type), ResourceType = typeof(SharedResource))]
    [Required(ErrorMessageResourceName = nameof(CoreResource.Required), ErrorMessageResourceType = typeof(CoreResource))]
    public DemoThingType Type { get; }
}
