// Copyright (c) CodeBlock.Dev. All rights reserved.
// For more information visit https://codeblock.dev

using CanBeYours.Core.Resources;
using CodeBlock.DevKit.Core.Exceptions;
using CodeBlock.DevKit.Core.Resources;
using CodeBlock.DevKit.Domain.Exceptions;

namespace CanBeYours.Core.Domain.DemoThings;

/// <summary>
/// Static factory class for creating domain-specific exceptions related to DemoThing entities.
/// This class demonstrates how to implement domain exception factories that provide
/// localized error messages with proper resource management.
/// 
/// This is an example implementation showing domain exception handling patterns. Replace with
/// your actual domain exception factories that handle your business rule violations.
/// 
/// Key features demonstrated:
/// - Centralized exception creation with consistent error messages
/// - Localized error messages using resource files
/// - Domain-specific exception types for different validation failures
/// - Proper use of message placeholders for dynamic content
/// </summary>
internal static class DemoThingDomainExceptions
{
    /// <summary>
    /// Creates a domain exception when the DemoThing name is missing or invalid.
    /// This exception is thrown when business rules require a valid name but none is provided.
    /// 
    /// Example usage:
    /// if (string.IsNullOrWhiteSpace(name))
    ///     throw DemoThingDomainExceptions.NameIsRequired();
    /// </summary>
    /// <returns>A domain exception with localized error message for missing name</returns>
    public static DomainException NameIsRequired()
    {
        return new DomainException(
            nameof(CoreResource.Required),
            typeof(CoreResource),
            new List<MessagePlaceholder> { MessagePlaceholder.CreateResource(SharedResource.DemoThing_Name, typeof(SharedResource)) }
        );
    }

    /// <summary>
    /// Creates a domain exception when the DemoThing description is missing or invalid.
    /// This exception is thrown when business rules require a valid description but none is provided.
    /// 
    /// Example usage:
    /// if (string.IsNullOrWhiteSpace(description))
    ///     throw DemoThingDomainExceptions.DescriptionIsRequired();
    /// </summary>
    /// <returns>A domain exception with localized error message for missing description</returns>
    public static DomainException DescriptionIsRequired()
    {
        return new DomainException(
            nameof(CoreResource.Required),
            typeof(CoreResource),
            new List<MessagePlaceholder> { MessagePlaceholder.CreateResource(SharedResource.DemoThing_Description, typeof(SharedResource)) }
        );
    }

    /// <summary>
    /// Creates a domain exception when the DemoThing user ID is missing or invalid.
    /// This exception is thrown when business rules require a valid user ID for ownership tracking.
    /// 
    /// Example usage:
    /// if (string.IsNullOrWhiteSpace(userId))
    ///     throw DemoThingDomainExceptions.UserIdIsRequired();
    /// </summary>
    /// <returns>A domain exception with localized error message for missing user ID</returns>
    public static DomainException UserIdIsRequired()
    {
        return new DomainException(
            nameof(CoreResource.Required),
            typeof(CoreResource),
            new List<MessagePlaceholder> { MessagePlaceholder.CreateResource(SharedResource.DemoThing_UserId, typeof(SharedResource)) }
        );
    }
}
