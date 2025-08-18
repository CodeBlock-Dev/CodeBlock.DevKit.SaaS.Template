namespace CanBeYours.AdminPanel;

/// <summary>
/// Entry point for the AdminPanel application. This demonstrates how to configure and run
/// a Blazor Server application using the CodeBlock.DevKit template structure.
/// The current functionality is just for learning purposes to show you how to implement
/// your own unique features into the existing codebase.
/// </summary>
public class Program
{
    /// <summary>
    /// Main entry point that configures and runs the web application.
    /// Uses extension methods to configure services and pipeline for clean separation of concerns.
    /// </summary>
    /// <param name="args">Command line arguments</param>
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var app = builder.ConfigureServices().ConfigurePipeline();

        app.Run();
    }
}
