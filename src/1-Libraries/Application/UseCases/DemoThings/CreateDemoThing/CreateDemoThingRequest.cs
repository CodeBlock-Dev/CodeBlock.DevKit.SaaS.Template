using System.ComponentModel.DataAnnotations;
using CanBeYours.Core.Resources;
using CodeBlock.DevKit.Application.Commands;
using CodeBlock.DevKit.Core.Resources;

namespace CanBeYours.Application.UseCases.DemoThings.CreateDemoThing;

internal class CreateDemoThingRequest : BaseCommand
{
    public CreateDemoThingRequest(string name, string description)
    {
        Name = name;
        Description = description;
    }

    [Display(Name = nameof(SharedResource.DemoThing_Name), ResourceType = typeof(SharedResource))]
    [Required(ErrorMessageResourceName = nameof(CoreResource.Required), ErrorMessageResourceType = typeof(CoreResource))]
    public string Name { get; }

    [Display(Name = nameof(SharedResource.DemoThing_Description), ResourceType = typeof(SharedResource))]
    [Required(ErrorMessageResourceName = nameof(CoreResource.Required), ErrorMessageResourceType = typeof(CoreResource))]
    public string Description { get; }
}
