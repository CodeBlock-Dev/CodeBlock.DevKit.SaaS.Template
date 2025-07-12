#!/usr/bin/env pwsh
# Cross-platform setup script for SaaS template. Run with: pwsh setup.ps1
# This script updates all relevant files for your new solution name and settings.

# Function to generate random string
function Get-RandomKey {
    param (
        [int]$length = 32
    )
    $chars = 'abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%^&*()_+-='
    return -join ((1..$length) | ForEach-Object { $chars[(Get-Random -Maximum $chars.Length)] })
}

# Function to prompt for input with default value
function Get-UserInput {
    param (
        [string]$prompt,
        [string]$default,
        [bool]$required = $false
    )
    
    Write-Host "`n$prompt"
    Write-Host "Current value: $default"
    
    do {
        $input = Read-Host "Enter new value (or press Enter to keep current)"
        if ([string]::IsNullOrWhiteSpace($input)) {
            if ($required) {
                Write-Host "This value is required. Please enter a value."
                continue
            }
            return $default
        }
        return $input
    } while ($required)
}

# Welcome message
Write-Host "Welcome to the SaaS Template Setup Script!"
Write-Host "This script will help you configure your new SaaS application."
Write-Host "Press Ctrl+C at any time to cancel the setup.`n"

# Get solution name (required)
$solutionName = Get-UserInput -prompt "Enter your solution name (this will be used for namespaces, assembly names, etc.)" -default "CanBeYours" -required $true

# Get application name
$appName = Get-UserInput -prompt "Enter your application name (this will be displayed in UI and Swagger)" -default "Can Be Yours"

# Get application URL
$appUrl = Get-UserInput -prompt "Enter your application URL (this will be used for JWT issuer and other settings)" -default "https://localhost"

# Get admin user details
$adminMobile = Get-UserInput -prompt "Enter admin user mobile number" -default "+1234567890"
$adminEmail = Get-UserInput -prompt "Enter admin user email" -default "admin@example.com"
$adminPassword = Get-UserInput -prompt "Enter admin user password" -default "Admin123!"

# Generate random keys
$encryptionKey = Get-RandomKey -length 32
$apiKey = Get-RandomKey -length 32
$jwtKey = Get-RandomKey -length 32

# Update solution file
Write-Host "`nUpdating solution file..."
(Get-Content "CanBeYours.sln") -replace "CanBeYours", $solutionName | Set-Content "$solutionName.sln"
Remove-Item "CanBeYours.sln"

# Update all .csproj files
Write-Host "Updating project files..."
Get-ChildItem -Recurse -Filter "*.csproj" | ForEach-Object {
    $content = Get-Content $_.FullName
    $content = $content -replace "CanBeYours\.", "$solutionName."
    $content | Set-Content $_.FullName
}

# Update all .cs files
Write-Host "Updating C# files..."
Get-ChildItem -Recurse -Filter "*.cs" | ForEach-Object {
    $content = Get-Content $_.FullName
    $content = $content -replace "namespace CanBeYours\." , "namespace $solutionName."
    $content = $content -replace "using CanBeYours\." , "using $solutionName."
    $content | Set-Content $_.FullName
}

# Update all .razor and .cshtml files
Write-Host "Updating Razor and Blazor files..."
Get-ChildItem -Recurse -Include *.razor,*.cshtml | ForEach-Object {
    $content = Get-Content $_.FullName
    $content = $content -replace "@using CanBeYours\.", "@using $solutionName."
    $content = $content -replace "@namespace CanBeYours\.", "@namespace $solutionName."
    $content | Set-Content $_.FullName
}

# Update appsettings.json files
Write-Host "Updating appsettings.json files..."
Get-ChildItem -Recurse -Filter "appsettings.json" | ForEach-Object {
    $json = Get-Content $_.FullName | ConvertFrom-Json -Depth 100
    
    # Update application settings
    $json.Application.Default.Name = $appName
    $json.Application.Default.Url = $appUrl
    $json.Swagger.Title = $appName
    
    # Update admin user settings
    $json.Identity.AdminUser.Mobile = $adminMobile
    $json.Identity.AdminUser.Email = $adminEmail
    $json.Identity.AdminUser.Password = $adminPassword
    
    # Update security keys
    $json.Security.EncryptionSymmetricKey = $encryptionKey
    $json.ApiKey = $apiKey
    $json.JwtAuthentication.Key = $jwtKey
    $json.JwtAuthentication.Issuer = $appUrl
    
    # Update solution name based settings
    $json.Hangfire.MongoStorage = $json.Hangfire.MongoStorage -replace "CanBeYours", $solutionName
    $json.Monitoring.Service.Name = $json.Monitoring.Service.Name -replace "CanBeYours", $solutionName
    $json.CookieAuthentication.CookieName = $json.CookieAuthentication.CookieName -replace "CanBeYours", $solutionName
    $json.Localization.CookieName = $json.Localization.CookieName -replace "CanBeYours", $solutionName
    $json.MongoDB.DatabaseName = $json.MongoDB.DatabaseName -replace "CanBeYours", $solutionName
    
    $json | ConvertTo-Json -Depth 100 | Set-Content $_.FullName
}

Write-Host "`nSetup completed successfully!"
Write-Host "Your SaaS application has been configured with the following settings:"
Write-Host "Solution Name: $solutionName"
Write-Host "Application Name: $appName"
Write-Host "Application URL: $appUrl"
Write-Host "Admin Email: $adminEmail"
Write-Host "`nYou can now open the solution in your IDE and start developing!" 