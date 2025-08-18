// Copyright (c) CodeBlock.Dev. All rights reserved.
// For more information visit https://codeblock.dev

using CanBeYours.Core.Resources;
using CodeBlock.DevKit.Core.Exceptions;
using CodeBlock.DevKit.Core.Resources;
using ApplicationException = CodeBlock.DevKit.Application.Exceptions.ApplicationException;

namespace CanBeYours.Application.Exceptions;

/// <summary>
/// Static factory class for creating application-specific exceptions related to DemoThing operations.
/// This class demonstrates how to create custom exceptions with localized error messages and
/// proper error handling patterns.
/// The DemoThing functionality shown here is just an example to help you learn how to implement
/// your own unique features into the current codebase.
/// </summary>
internal static class DemoThingApplicationExceptions
{
    /// <summary>
    /// Creates an application exception when a demo thing is not found by the specified search key.
    /// This method demonstrates how to create localized exceptions with placeholder values.
    /// </summary>
    /// <param name="searchedKey">The key that was used to search for the demo thing (e.g., ID, name)</param>
    /// <returns>An application exception with localized error message</returns>
    /// <example>
    /// <code>
    /// if (demoThing == null)
    ///     throw DemoThingApplicationExceptions.DemoThingNotFound(id);
    /// </code>
    /// </example>
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
