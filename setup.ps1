#!/usr/bin/env pwsh
# Cross-platform setup script for SaaS template. Run with: pwsh setup.ps1
# On Windows, double-click setup.bat. On Linux/macOS, double-click setup.sh (or run chmod +x setup.ps1 and ./setup.ps1)

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
            $input = $default
        }
        if ($required -and [string]::IsNullOrWhiteSpace($input)) {
            Write-Host "This value is required. Please enter a value."
            continue
        }
        break
    } while ($true)
    
    return $input
}

# Welcome message
Write-Host "Welcome to CodeBlock Dev Kit - SaaS Template Setup!"
Write-Host "This script will help you configure your new SaaS application powered by CodeBlock Dev Kit."
Write-Host "Press Ctrl+C at any time to cancel the setup.`n"

# Read default values from appsettings.json
$defaultSettingsPath = Join-Path $PSScriptRoot "src/2-Clients/AdminPanel/appsettings.json"
$defaultSettings = Get-Content $defaultSettingsPath -Raw | ConvertFrom-Json

# Get solution name (required)
$solutionName = Get-UserInput -prompt "Enter your solution name (this will be used for namespaces, assembly names, database name, etc.)" -default $defaultSettings.MongoDB.DatabaseName -required $true

# Get application name
$appName = Get-UserInput -prompt "Enter your application name (this will be displayed in UI)" -default $defaultSettings.Application.Default.Name

# Get application URL
$appUrl = Get-UserInput -prompt "Enter your application URL" -default $defaultSettings.Application.Default.Url

# Get admin user details
$adminMobile = Get-UserInput -prompt "Enter admin user mobile number" -default $defaultSettings.Identity.AdminUser.Mobile
$adminEmail = Get-UserInput -prompt "Enter admin user email" -default $defaultSettings.Identity.AdminUser.Email
$adminPassword = Get-UserInput -prompt "Enter admin user password" -default $defaultSettings.Identity.AdminUser.Password

# Generate random keys
$encryptionKey = Get-RandomKey -length 32
$apiKey = Get-RandomKey -length 32
$jwtKey = Get-RandomKey -length 32

# Update all appsettings.json files
Write-Host "Updating appsettings.json files..."
Get-ChildItem -Recurse -Filter "appsettings.json" | ForEach-Object {
    $content = Get-Content $_.FullName -Raw
    
    # Update values using string replacement to preserve JSON structure
    $content = $content -replace '"Name":\s*"[^"]*"', "`"Name`": `"$appName`""
    $content = $content -replace '"Url":\s*"[^"]*"', "`"Url`": `"$appUrl`""
    $content = $content -replace '"Title":\s*"[^"]*"', "`"Title`": `"$appName`""
    $content = $content -replace '"Issuer":\s*"[^"]*"', "`"Issuer`": `"$appUrl`""
    $content = $content -replace '"Mobile":\s*"[^"]*"', "`"Mobile`": `"$adminMobile`""
    $content = $content -replace '"Email":\s*"[^"]*"', "`"Email`": `"$adminEmail`""
    $content = $content -replace '"Password":\s*"[^"]*"', "`"Password`": `"$adminPassword`""
    $content = $content -replace '"EncryptionSymmetricKey":\s*"[^"]*"', "`"EncryptionSymmetricKey`": `"$encryptionKey`""
    $content = $content -replace '"ApiKey":\s*"[^"]*"', "`"ApiKey`": `"$apiKey`""
    $content = $content -replace '"Key":\s*"[^"]*"', "`"Key`": `"$jwtKey`""
    $content = $content -replace '"DatabaseName":\s*"[^"]*"', "`"DatabaseName`": `"$solutionName`""
    $content = $content -replace '"Name":\s*"CanBeYours\.[^"]*"', "`"Name`": `"$solutionName.$1`""
    $content = $content -replace '"CookieName":\s*"CanBeYours\.[^"]*"', "`"CookieName`": `"$solutionName.$1`""
    
    Set-Content $_.FullName $content
}

# Update solution file
Write-Host "Updating solution file..."
Get-ChildItem -Filter "*.sln" | ForEach-Object {
    $oldName = $_.Name
    $newName = $_.Name -replace "CanBeYours", $solutionName
    $content = Get-Content $_.FullName
    $content = $content -replace "CanBeYours", $solutionName
    Set-Content $_.FullName $content
    
    # Rename the solution file if name changed
    if ($oldName -ne $newName) {
        Write-Host "Renaming solution file from $oldName to $newName"
        Rename-Item -Path $_.FullName -NewName $newName -Force
    }
}

# Update all project files
Write-Host "Updating project files..."
Get-ChildItem -Recurse -Filter "*.csproj" | ForEach-Object {
    $content = Get-Content $_.FullName
    $content = $content -replace "<AssemblyName>CanBeYours\.", "<AssemblyName>$solutionName."
    $content = $content -replace "<RootNamespace>CanBeYours\.", "<RootNamespace>$solutionName."
    Set-Content $_.FullName $content
}

# Update all source files
Write-Host "Updating source files..."
Get-ChildItem -Recurse -Include "*.cs","*.razor","*.cshtml" | ForEach-Object {
    $content = Get-Content $_.FullName
    $content = $content -replace "namespace CanBeYours\.", "namespace $solutionName."
    $content = $content -replace "using CanBeYours\.", "using $solutionName."
    $content = $content -replace "@namespace CanBeYours\.", "@namespace $solutionName."
    $content = $content -replace "@using CanBeYours\.", "@using $solutionName."
    Set-Content $_.FullName $content
}

Write-Host "`nSetup completed successfully!"
Write-Host "Your CodeBlock Dev Kit SaaS application has been configured with the following settings:"
Write-Host "Solution Name: $solutionName"
Write-Host "Application Name: $appName"
Write-Host "Application URL: $appUrl"
Write-Host "Admin Email: $adminEmail"
Write-Host "`nYou can now build and run your application." 