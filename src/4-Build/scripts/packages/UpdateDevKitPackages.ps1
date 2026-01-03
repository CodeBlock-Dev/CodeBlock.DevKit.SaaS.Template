Set-Location -Path (Split-Path -Parent (Split-Path -Parent $PSScriptRoot))

Write-Host "*************************** Update CodeBlock.DevKit NuGet Packages ***************************"

$version = Read-Host "Enter the CodeBlock.DevKit packages target version (e.g., 1.5.1, leave blank for 'latest version')"

Write-Host "Running: nuke UpdateDevKitPackages --version $version"
nuke UpdateDevKitPackages --version $version

if ($Host.Name -eq "ConsoleHost") {
    Write-Host "Press any key to continue..."
    $Host.UI.RawUI.ReadKey("NoEcho,IncludeKeyUp") > $null
}

