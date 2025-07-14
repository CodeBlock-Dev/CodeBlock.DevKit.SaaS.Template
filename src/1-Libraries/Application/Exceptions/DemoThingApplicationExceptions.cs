// Copyright (c) CodeBlock.Dev. All rights reserved.
// For more information visit https://codeblock.dev

using CanBeYours.Core.Resources;
using CodeBlock.DevKit.Core.Exceptions;
using CodeBlock.DevKit.Core.Resources;
using ApplicationException = CodeBlock.DevKit.Application.Exceptions.ApplicationException;

namespace CanBeYours.Application.Exceptions;

internal static class DemoThingApplicationExceptions
{
    public static ApplicationException DemoThingNotFound(string searchedKey)
    {
        return new ApplicationException(
            nameof(CoreResource.Record_Not_Found),
            typeof(CoreResource),
            new List<MessagePlaceholder>
            {
                MessagePlaceholder.CreateResource(SharedResource.DemoThing, typeof(SharedResource)),
                MessagePlaceholder.CreatePlainText(searchedKey),
            }
        );
    }
}
