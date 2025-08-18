namespace CanBeYours.WebApp;

/// <summary>
/// Main entry point for the WebApp client application.
/// This class demonstrates how to set up a public-facing website using the CodeBlock.DevKit template.
/// The current functionality serves as a learning example - you can replace this with your own
/// website features, landing pages, or public content while keeping the same structure.
/// </summary>
public class Program
{
    /// <summary>
    /// Main method that initializes and runs the web application.
    /// Uses the CodeBlock.DevKit extension methods for streamlined configuration.
    /// </summary>
    /// <param name="args">Command line arguments passed to the application</param>
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var app = builder.ConfigureServices().ConfigurePipeline();

        app.Run();
    }
}
