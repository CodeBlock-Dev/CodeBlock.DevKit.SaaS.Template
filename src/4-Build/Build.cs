using CodeBlock.DevKit.Build;

namespace Build;

/// <summary>
/// Main build class that demonstrates how to implement a custom build configuration using CodeBlock.DevKit.Build.
/// 
/// This class serves as a learning example showing developers how to:
/// - Extend the BaseBuild class to create custom build workflows
/// - Configure build settings like skipping linting
/// - Set up the main entry point for build execution
/// - Override default build behavior for specific project needs
/// 
/// Note: The current functionality is just for demonstration purposes to help you learn
/// how to implement your own unique build features into the CodeBlock.DevKit codebase.
/// </summary>
public class Build : BaseBuild
{
    /// <summary>
    /// Overrides the default linting behavior to skip code linting during build.
    /// 
    /// This property demonstrates how to customize build behavior by overriding
    /// BaseBuild properties. You can set this to false if you want to include
    /// linting in your build process.
    /// 
    /// Example usage: Set to false when you want to enforce code quality checks
    /// during the build process.
    /// </summary>
    protected override bool SkipLinting => true;

    /// <summary>
    /// Main entry point for the build application.
    /// 
    /// This method demonstrates how to:
    /// - Set up the main entry point for build execution
    /// - Execute build operations using the Execute<T> method
    /// - Configure which build target to run by default (RunUnitTests in this case)
    /// 
    /// The Execute<T> method handles the build lifecycle and provides a clean
    /// way to run build operations with proper error handling and logging.
    /// 
    /// Example: This will run unit tests when the build application is executed.
    /// You can modify the lambda to run different build targets like Compile, 
    /// RunIntegrationTests, or custom targets you define.
    /// </summary>
    /// <returns>Exit code indicating build success (0) or failure (non-zero)</returns>
    public static int Main() => Execute<Build>(x => x.RunUnitTests);
}
