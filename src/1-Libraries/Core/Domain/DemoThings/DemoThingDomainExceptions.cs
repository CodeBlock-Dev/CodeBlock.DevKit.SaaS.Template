// Copyright (c) CodeBlock.Dev. All rights reserved.
// For more information visit https://codeblock.dev

using CanBeYours.Core.Resources;
using CodeBlock.DevKit.Core.Exceptions;
using CodeBlock.DevKit.Core.Resources;
using CodeBlock.DevKit.Domain.Exceptions;

namespace CanBeYours.Core.Domain.DemoThings;

internal static class DemoThingDomainExceptions
{
    public static DomainException NameIsRequired()
    {
        return new DomainException(
            nameof(CoreResource.Required),
            typeof(CoreResource),
            new List<MessagePlaceholder> { MessagePlaceholder.CreateResource(SharedResource.DemoThing_Name, typeof(SharedResource)) }
        );
    }

    public static DomainException DescriptionIsRequired()
    {
        return new DomainException(
            nameof(CoreResource.Required),
            typeof(CoreResource),
            new List<MessagePlaceholder> { MessagePlaceholder.CreateResource(SharedResource.DemoThing_Description, typeof(SharedResource)) }
        );
    }

    public static DomainException UserIdIsRequired()
    {
        return new DomainException(
            nameof(CoreResource.Required),
            typeof(CoreResource),
            new List<MessagePlaceholder> { MessagePlaceholder.CreateResource(SharedResource.DemoThing_UserId, typeof(SharedResource)) }
        );
    }
}
