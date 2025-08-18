namespace CanBeYours.Api;

/// <summary>
/// Main entry point for the CanBeYours API application.
/// This class demonstrates the standard structure for a .NET Web API Program.cs file.
/// The current functionality is just for you to learn and understand how to implement
/// your own unique features into the current codebase.
/// </summary>
public class Program
{
    /// <summary>
    /// Main method that bootstraps the web application.
    /// Configures services and pipeline, then runs the application.
    /// </summary>
    /// <param name="args">Command line arguments</param>
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var app = builder.ConfigureServices().ConfigurePipeline();

        app.Run();
    }
}
