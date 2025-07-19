# CodeBlock Dev Kit Template

A comprehensive SaaS application template built with the [CodeBlock Dev Kit](https://codeblock.dev). This template provides a complete foundation for building modern, scalable SaaS applications with enterprise-grade architecture.

<div align="center">
  <a href="https://www.youtube.com/embed/dQw4w9WgXcQ" target="_blank">
    <img src="https://img.youtube.com/vi/dQw4w9WgXcQ/maxresdefault.jpg" alt="CodeBlock Dev Kit Template Demo" width="560" height="315" style="border-radius: 8px; box-shadow: 0 4px 8px rgba(0,0,0,0.1);">
  </a>
</div>

# 📥 Download the Template

You can download this template in two ways:

#### Option 1: Download ZIP File
1. Go to the [GitHub](https://github.com/CodeBlock-Dev/CodeBlock.DevKit.Template) repository
2. Click the green `< > Code` button
3. Select `Download ZIP`
4. Extract the ZIP file to your desired location on your PC

#### Option 2: Clone with Git
```bash
git clone https://github.com/CodeBlock-Dev/CodeBlock.Dev Kit.Template.git
cd CodeBlock.Dev Kit.Template
```

**Note**: All Dev Kit modules are delivered via NuGet packages, so you only need to maintain your own business logic.


- 
# 📋 Purchase a License

To use this template, you need to purchase a license from the CodeBlock Dev Kit website. Here's how to get started:

1. Visit [codeblock.dev](https://codeblock.dev/#pricing) and purchase a plan.
2. Go to your [dashboard](https://codeblock.dev/dashboard) and download your license file (`codeblock.dev.license.lic`).
3. Copy the license file to the root directory of this template project
4. Execute the setup script to configure your SaaS application


# ⚙️ Setup the Template

The template includes automated setup scripts for different operating systems:

1. Go to the `/setup` directory in the template root
2. Run the appropriate setup script for your OS:
3. Configure your application via the setup wizard.


## 🔧 Build, Deploy and Maintenance

This template includes comprehensive build automation using the [NUKE](https://nuke.build/) build system. You can easily user them in your [CI/CD](.github/workflows) pipelines or run them locally.



| Location | Purpose |
|----------------|----------|---------|
| `src/4-Build/scripts/compile/` | Builds the entire solution |
| `src/4-Build/scripts/lint/` | Runs code quality checks |
| `src/4-Build/scripts/tests/` | Executes unit and integration tests |
| `src/4-Build/scripts/packages/` | Updates CodeBlock Dev Kit NuGet packages |

### Running Build Scripts

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

# 🚀 Implement Your Logic and Build Your SaaS

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
- **Dev Kit Documentation**: [https://docs.codeblock.dev/](https://docs.codeblock.dev/)
- **Architecture Patterns**: Study the demo implementation to understand clean architecture principles
- **Best Practices**: Follow the established patterns for consistency and maintainability

## 📚 Next Steps

1. **Study the Demo**: Examine the demo implementation to understand the patterns
2. **Customize Settings**: Update configuration files with your application details
3. **Add Your Features**: Implement your business logic following the established patterns
4. **Deploy**: Use the provided CI/CD workflows or customize them for your deployment environment

For detailed guidance on implementing features and customizing the template, refer to the [CodeBlock Dev Kit Documentation](https://docs.codeblock.dev/). 