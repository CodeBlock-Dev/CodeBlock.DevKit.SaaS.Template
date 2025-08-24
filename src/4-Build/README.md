# Build Scripts

This directory contains build scripts organized by functionality in the `scripts` folder:

```
scripts/
├── compile/          # Compilation scripts
├── lint/            # Code linting scripts
├── packages/        # Package management scripts
└── tests/           # Test runner scripts
```

Each directory contains scripts in multiple formats for cross-platform compatibility:
- `.bat` - Windows batch files
- `.ps1` - PowerShell scripts (cross-platform)
- `.sh` - Shell scripts (Unix-based systems)

## Running Scripts on Windows

### Batch Files (.bat)
- Simply double-click the `.bat` file in the appropriate directory
- Or run from command prompt: `scripts\category\ScriptName.bat`
  Example: `scripts\compile\Compile.bat`

### PowerShell Scripts (.ps1)
1. Right-click method:
   - Right-click on the `.ps1` file in the appropriate directory
   - Select "Run with PowerShell"

2. Command line method:
   - Open PowerShell
   - Navigate to this directory
   - Run: `.\scripts\category\ScriptName.ps1`
   Example: `.\scripts\compile\Compile.ps1`

   If you get a security error, you may need to allow script execution:
   ```powershell
   Set-ExecutionPolicy -ExecutionPolicy RemoteSigned -Scope CurrentUser
   ```

### Shell Scripts (.sh)
To run `.sh` files on Windows, you need one of these installed:
1. Git Bash (recommended, comes with Git for Windows):
   ```bash
   ./scripts/category/ScriptName.sh
   ```
   Example: `./scripts/compile/Compile.sh`

2. Windows Subsystem for Linux (WSL):
   ```bash
   bash scripts/category/ScriptName.sh
   ```

## Running Scripts on macOS/Linux

### Shell Scripts (.sh)
1. Open terminal
2. Navigate to this directory
3. Make scripts executable (one-time setup):
   ```bash
   chmod +x scripts/**/*.sh
   ```
4. Run the script:
   ```bash
   ./scripts/category/ScriptName.sh
   ```
   Example: `./scripts/compile/Compile.sh`

### PowerShell Scripts (.ps1)
1. Install PowerShell if not already installed
2. Open terminal
3. Navigate to this directory
4. Run:
   ```bash
   pwsh scripts/category/ScriptName.ps1
   ```
   Example: `pwsh scripts/compile/Compile.ps1`

## Available Scripts

### Compile
- Location: `scripts/compile/`
- Purpose: Compiles the project
- Files: `Compile.bat`, `Compile.ps1`, `Compile.sh`

### Lint
- Location: `scripts/lint/`
- Purpose: Runs code linting
- Files: `Lint.bat`, `Lint.ps1`, `Lint.sh`

### Tests
- Location: `scripts/tests/`
- Purpose: Runs unit tests
- Files: `RunUnitTests.bat`, `RunUnitTests.ps1`, `RunUnitTests.sh`

### Packages
- Location: `scripts/packages/`
- Purpose: Updates CodeBlock DevKit NuGet packages, giving you access to the latest features and bug fixes.
- Files: `UpdateDevKitPackages.bat`, `UpdateDevKitPackages.ps1`, `UpdateDevKitPackages.sh` 
