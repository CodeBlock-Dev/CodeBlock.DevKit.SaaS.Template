# CodeBlock DevKit Template

A comprehensive SaaS application template built with the [CodeBlock DevKit](https://codeblock.dev/) framework. This template provides a complete foundation for building modern, scalable SaaS applications with enterprise-grade architecture.

## 🎬 Demo & Resources

- **Website**: [https://codeblock.dev/](https://codeblock.dev/)
- **Documentation**: [https://docs.codeblock.dev/](https://docs.codeblock.dev/)

### Demo Video

Watch how to use this template and build your SaaS application:

<div align="center">
  <a href="https://www.youtube.com/embed/dQw4w9WgXcQ" target="_blank">
    <img src="https://img.youtube.com/vi/dQw4w9WgXcQ/maxresdefault.jpg" alt="CodeBlock DevKit Template Demo" width="560" height="315" style="border-radius: 8px; box-shadow: 0 4px 8px rgba(0,0,0,0.1);">
  </a>
  <br>
  <em>Click the image above to watch the demo video</em>
</div>

## 📋 Prerequisites: Purchasing a License

To use this template, you need to purchase a license from the CodeBlock DevKit website. Here's how to get started:

1. **Purchase a License**: Visit [https://codeblock.dev/pricing](https://codeblock.dev/pricing) and select the appropriate license for your needs
2. **Download Your License**: After purchase, download your license file from your user dashboard
3. **Place License File**: Copy the license file to the root directory of this template project
4. **Run the Template**: Execute the setup script to configure your SaaS application

## 📥 Download the Template

You can download this template in two ways:

### Option 1: Download ZIP File
1. Go to [https://github.com/CodeBlock-Dev/CodeBlock.DevKit.Template](https://github.com/CodeBlock-Dev/CodeBlock.DevKit.Template)
2. Click the green "Code" button
3. Select "Download ZIP"
4. Extract the ZIP file to your desired location

### Option 2: Clone with Git
```bash
git clone https://github.com/CodeBlock-Dev/CodeBlock.DevKit.Template.git
cd CodeBlock.DevKit.Template
```

### Important Notes
- All DevKit modules are delivered via NuGet packages, so you only need to maintain your business logic
- You can update DevKit modules to the latest versions using the update script in the [Build section](#build-and-maintenance)

## ⚙️ Setup the Template

The template includes automated setup scripts for different operating systems:

1. **Navigate to Setup Folder**: Go to the `/setup` directory
2. **Run Setup Script**: Execute the appropriate script for your OS:
   - **Windows**: Run `setup.bat` or `setup.ps1`
   - **macOS/Linux**: Run `setup.sh`
3. **Configure Your Application**: The setup wizard will guide you through:
   - Setting your solution name
   - Configuring database settings
   - Setting up admin user credentials
   - Customizing application settings

## 🔧 Build and Maintenance

This template includes comprehensive build automation using the [NUKE build system](https://nuke.build/). The build system is organized in the `/4-Build` directory with scripts for different tasks.

### Available Build Scripts

| Script Category | Location | Purpose |
|----------------|----------|---------|
| **Compile** | `/scripts/compile/` | Builds the entire solution |
| **Lint** | `/scripts/lint/` | Runs code quality checks |
| **Tests** | `/scripts/tests/` | Executes unit and integration tests |
| **Packages** | `/scripts/packages/` | Updates CodeBlock.DevKit NuGet packages |

### Running Build Scripts

**Windows:**
```powershell
# PowerShell (recommended)
.\scripts\compile\Compile.ps1
.\scripts\lint\Lint.ps1
.\scripts\tests\RunUnitTests.ps1
.\scripts\packages\UpdateDevKitPackages.ps1

# Or batch files
scripts\compile\Compile.bat
```

**macOS/Linux:**
```bash
# Make scripts executable (one-time setup)
chmod +x scripts/**/*.sh

# Run scripts
./scripts/compile/Compile.sh
./scripts/lint/Lint.sh
./scripts/tests/RunUnitTests.sh
./scripts/packages/UpdateDevKitPackages.sh
```

For detailed information about the build system, see the [Build README](src/4-Build/README.md).

### Continuous Integration & Deployment

The template includes pre-configured CI/CD workflows:

- **CI Workflow** (`.github/workflows/ci.yml`): Builds, tests, and packages your application
- **CD Workflow** (`.github/workflows/cd.yml`): Deploys to Windows IIS server

**Customizing for Your Environment:**

1. **Modify CI Workflow**: Update `.github/workflows/ci.yml` to match your build requirements
2. **Configure CD Workflow**: Customize `.github/workflows/cd.yml` for your deployment target:
   - For Linux deployment: Change runner to `ubuntu-latest`
   - For cloud deployment: Update deployment steps for your cloud provider
   - For different platforms: Modify build commands and artifact packaging

## 🚀 Implement Your Logic and Build Your SaaS

The template includes a complete example implementation to help you understand how to build your own features. Study the demo implementation to learn the patterns:

### Frontend Implementation
- **Pages**: `/src/2-Clients/AdminPanel/Pages/DemoThings/`
- **Navigation**: `/src/2-Clients/AdminPanel/Pages/Shared/NavMenu.razor`

### Backend Implementation
- **API Controllers**: `/src/2-Clients/Api/Controllers/DemoThingsController.cs`
- **Application Services**: `/src/1-Libraries/Application/Services/DemoThings/`
- **Use Cases**: `/src/1-Libraries/Application/UseCases/DemoThings/`
- **Domain Logic**: `/src/1-Libraries/Core/Domain/DemoThings/`

### Testing
- **Unit Tests**: `/src/3-Tests/Application.Tests.Unit/UseCases/DemoThings/`
- **Integration Tests**: `/src/3-Tests/Application.Tests.Integration/UseCases/DemoThings/`

### Learning Resources
- **DevKit Documentation**: [https://docs.codeblock.dev/](https://docs.codeblock.dev/)
- **Architecture Patterns**: Study the demo implementation to understand clean architecture principles
- **Best Practices**: Follow the established patterns for consistency and maintainability

## 📚 Next Steps

1. **Study the Demo**: Examine the demo implementation to understand the patterns
2. **Customize Settings**: Update configuration files with your application details
3. **Add Your Features**: Implement your business logic following the established patterns
4. **Deploy**: Use the provided CI/CD workflows or customize them for your deployment environment

For detailed guidance on implementing features and customizing the template, refer to the [CodeBlock DevKit Documentation](https://docs.codeblock.dev/). 
