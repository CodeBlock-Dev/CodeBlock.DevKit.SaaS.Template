using System.ComponentModel.DataAnnotations;
using CanBeYours.Core.Resources;

namespace CanBeYours.Application.Dtos;

public class CreateDemoThingDto
{
    [Required(ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.DemoThing_Name))]
    public string Name { get; set; }

    [Required(ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.DemoThing_Description))]
    public string Description { get; set; }
}
