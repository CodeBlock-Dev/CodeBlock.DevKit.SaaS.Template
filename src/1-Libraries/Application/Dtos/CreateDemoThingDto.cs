using System.ComponentModel.DataAnnotations;
using CanBeYours.Core.Resources;
using CodeBlock.DevKit.Contracts.Dtos;

namespace CanBeYours.Application.Dtos;

public class CreateDemoThingDto : BaseDto
{
    [Required(ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.DemoThing_Name))]
    public string Name { get; set; }

    [Required(ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.DemoThing_Description))]
    public string Description { get; set; }
} 