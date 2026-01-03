@echo off
cd /d "%~dp0..\.."

echo *************************** Update CodeBlock.DevKit NuGet Packages ***************************

set /p version=Enter the CodeBlock.DevKit packages target version (e.g., 1.5.1, leave blank for 'latest version'): 

echo Running: nuke UpdateDevKitPackages --version %version%
nuke UpdateDevKitPackages --version %version%

pause
