#!/usr/bin/env pwsh
# Cross-platform setup script for SaaS template. Run with: pwsh setup.ps1
# On Windows, double-click setup.bat. On Linux/macOS, double-click setup.sh (or run chmod +x setup.ps1 and ./setup.ps1)

# Validation functions
function Test-SolutionName {
    param ([string]$name)
    if ($name -match '^[a-zA-Z][a-zA-Z0-9_]*$') {
        return $true
    }
    Write-Host "Invalid solution name. It must:" -ForegroundColor Red
    Write-Host "- Start with a letter" -ForegroundColor Red
    Write-Host "- Contain only letters, numbers, and underscores" -ForegroundColor Red
    return $false
}

function Test-Password {
    param ([string]$password)
    if ($password.Length -ge 8) {
        return $true
    }
    Write-Host "Password must be at least 8 characters long (you can use more)." -ForegroundColor Red
    return $false
}

function Test-Email {
    param ([string]$email)
    if ($email -match '^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$') {
        return $true
    }
    Write-Host "Please enter a valid email address." -ForegroundColor Red
    return $false
}

function Test-Url {
    param ([string]$url)
    if ($url -match '^https?://[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}[a-zA-Z0-9./_-]*$') {
        return $true
    }
    Write-Host "Please enter a valid URL (e.g., https://example.com)." -ForegroundColor Red
    return $false
}

function Test-Mobile {
    param ([string]$mobile)
    # Allows international format with optional + prefix, country code, and 6-15 digits
    if ($mobile -match '^\+?[1-9]\d{1,3}[0-9]{6,14}$') {
        return $true
    }
    Write-Host "Please enter a valid mobile number in international format (e.g., +1234567890)." -ForegroundColor Red
    Write-Host "- Country code (optional + and 1-4 digits)" -ForegroundColor Red
    Write-Host "- Phone number (6-15 digits)" -ForegroundColor Red
    return $false
}

# Function to generate random string
function Get-RandomKey {
    param (
        [int]$length = 32
    )
    $chars = 'abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%^&*()_+-='
    return -join ((1..$length) | ForEach-Object { $chars[(Get-Random -Maximum $chars.Length)] })
}

# Function to prompt for input with default value and validation
function Get-UserInput {
    param (
        [string]$prompt,
        [string]$default,
        [bool]$required = $false,
        [ValidateSet('text', 'solution', 'password', 'email', 'url', 'mobile')]
        [string]$validationType = 'text'
    )
    
    Write-Host "`n$prompt" -ForegroundColor Cyan
    Write-Host "Current value: " -NoNewline
    Write-Host $default -ForegroundColor Yellow
    
    do {
        Write-Host "Enter new value (or press Enter to keep current): " -NoNewline -ForegroundColor Gray
        if ($validationType -eq 'password') {
            $input = Read-Host -AsSecureString
            $input = [System.Runtime.InteropServices.Marshal]::PtrToStringAuto([System.Runtime.InteropServices.Marshal]::SecureStringToBSTR($input))
        } else {
            $input = Read-Host
        }

        if ([string]::IsNullOrWhiteSpace($input)) {
            $input = $default
        }

        # Skip validation if using default value and not required
        if ($input -eq $default -and -not $required) {
            return $input
        }

        # Validate input based on type
        $isValid = switch ($validationType) {
            'solution' { Test-SolutionName $input }
            'password' { Test-Password $input }
            'email' { Test-Email $input }
            'url' { Test-Url $input }
            'mobile' { Test-Mobile $input }
            default { $true }
        }

        if ($isValid) {
            if (-not $required -or -not [string]::IsNullOrWhiteSpace($input)) {
                return $input
            }
        }

        if ($required -and [string]::IsNullOrWhiteSpace($input)) {
            Write-Host "This value is required. Please enter a value." -ForegroundColor Red
        }
    } while ($true)
}

# Welcome message
Write-Host "`n=== CodeBlock Dev Kit - SaaS Template Setup ===" -ForegroundColor Green
Write-Host "This script will help you configure your new SaaS application powered by CodeBlock Dev Kit."
Write-Host "Press Ctrl+C at any time to cancel the setup.`n"

# Get the root directory (one level up from setup folder)
$rootDir = Split-Path -Parent $PSScriptRoot

# Read default values from appsettings.json
$defaultSettingsPath = Join-Path $rootDir "src/2-Clients/AdminPanel/appsettings.json"
$defaultSettings = Get-Content $defaultSettingsPath -Raw | ConvertFrom-Json

# Get solution name (required)
$solutionName = Get-UserInput -prompt "Enter your solution name (this will be used for namespaces, assembly names, database name, etc.)" -default $defaultSettings.MongoDB.DatabaseName -required $true -validationType 'solution'

# Get application name
$appName = Get-UserInput -prompt "Enter your application name (this will be displayed in UI)" -default $defaultSettings.Application.Default.Name -validationType 'text'

# Get application URL
$appUrl = Get-UserInput -prompt "Enter your application URL" -default $defaultSettings.Application.Default.Url -validationType 'url'

# Get admin user details
$adminMobile = Get-UserInput -prompt "Enter admin user mobile number" -default $defaultSettings.Identity.AdminUser.Mobile -validationType 'mobile'
$adminEmail = Get-UserInput -prompt "Enter admin user email" -default $defaultSettings.Identity.AdminUser.Email -validationType 'email'
$adminPassword = Get-UserInput -prompt "Enter admin user password" -default $defaultSettings.Identity.AdminUser.Password -validationType 'password'

# Generate random keys
$encryptionKey = Get-RandomKey -length 24  # Fixed length for EncryptionSymmetricKey
$apiKey = Get-RandomKey -length 32
$jwtKey = Get-RandomKey -length 32

# Change to root directory for all file operations
Push-Location $rootDir

try {
    # Update all appsettings.json files
    Write-Host "`nUpdating appsettings.json files..." -ForegroundColor Blue
    Get-ChildItem -Recurse -Filter "appsettings.json" | ForEach-Object {
        $content = Get-Content $_.FullName -Raw
        
        # Update values using string replacement to preserve JSON structure
        # Only replace the application name in specific places
        $content = $content -replace '"Application":\s*{\s*"Default":\s*{\s*"Name":\s*"[^"]*"', "`"Application`": { `"Default`": { `"Name`": `"$appName`""
        $content = $content -replace '"Url":\s*"[^"]*"', "`"Url`": `"$appUrl`""
        
        # Preserve suffixes when replacing CanBeYours
        $content = $content -replace '"CookieName":\s*"CanBeYours\.([^"]*)"', "`"CookieName`": `"$solutionName.`$1`""
        $content = $content -replace '"Name":\s*"CanBeYours\.([^"]*)"', "`"Name`": `"$solutionName.`$1`""
        $content = $content -replace '"DatabaseName":\s*"CanBeYours_([^"]*)"', "`"DatabaseName`": `"$solutionName_`$1`""
        $content = $content -replace '"DatabaseName":\s*"CanBeYours"', "`"DatabaseName`": `"$solutionName`""
        
        # Update admin user details
        $content = $content -replace '"Mobile":\s*"[^"]*"', "`"Mobile`": `"$adminMobile`""
        $content = $content -replace '"Email":\s*"[^"]*"', "`"Email`": `"$adminEmail`""
        $content = $content -replace '"Password":\s*"[^"]*"', "`"Password`": `"$adminPassword`""
        
        # Update security keys
        $content = $content -replace '"EncryptionSymmetricKey":\s*"[^"]*"', "`"EncryptionSymmetricKey`": `"$encryptionKey`""
        $content = $content -replace '"ApiKey":\s*"[^"]*"', "`"ApiKey`": `"$apiKey`""
        $content = $content -replace '"Key":\s*"[^"]*"', "`"Key`": `"$jwtKey`""
        
        # Update Swagger title preserving the suffix
        $content = $content -replace '"Title":\s*"Can Be Yours([^"]*)"', "`"Title`": `"$appName`$1`""
        
        Set-Content $_.FullName $content
    }

    # Update solution file
    Write-Host "`nUpdating solution file..." -ForegroundColor Blue
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
    Write-Host "`nUpdating project files..." -ForegroundColor Blue
    Get-ChildItem -Recurse -Filter "*.csproj" | ForEach-Object {
        $content = Get-Content $_.FullName
        $content = $content -replace "<AssemblyName>CanBeYours\.", "<AssemblyName>$solutionName."
        $content = $content -replace "<RootNamespace>CanBeYours\.", "<RootNamespace>$solutionName."
        Set-Content $_.FullName $content
    }

    # Update all source files
    Write-Host "`nUpdating source files..." -ForegroundColor Blue
    Get-ChildItem -Recurse -Include "*.cs","*.razor","*.cshtml","*.Designer.cs","AssemblyInfo.cs" | ForEach-Object {
        $content = Get-Content $_.FullName
        $content = $content -replace "namespace CanBeYours\.", "namespace $solutionName."
        $content = $content -replace "using CanBeYours\.", "using $solutionName."
        $content = $content -replace "@namespace CanBeYours\.", "@namespace $solutionName."
        $content = $content -replace "@using CanBeYours\.", "@using $solutionName."
        
        # Handle specific patterns in Designer.cs files (ResourceManager strings)
        $content = $content -replace '"CanBeYours\.([^"]*)"', "`"$solutionName.`$1`""
        
        # Handle InternalsVisibleTo attributes in AssemblyInfo.cs
        $content = $content -replace 'InternalsVisibleTo\("CanBeYours\.([^"]*)"\)', "InternalsVisibleTo(`"$solutionName.`$1`")"
        
        Set-Content $_.FullName $content
    }

    # Update .nuke configuration files and other JSON files
    Write-Host "`nUpdating configuration files..." -ForegroundColor Blue
    Get-ChildItem -Recurse -Include "*.nuke","parameters.json" | ForEach-Object {
        $content = Get-Content $_.FullName
        $content = $content -replace '"Solution":\s*"CanBeYours\.sln"', "`"Solution`": `"$solutionName.sln`""
        Set-Content $_.FullName $content
    }

    # Update generated files (test results, logs, etc.)
    Write-Host "`nUpdating generated files..." -ForegroundColor Blue
    Get-ChildItem -Recurse -Include "*.trx","*.log","*.cache","*.deps.json","*.runtimeconfig.json","*.staticwebassets.*.json","*.resources.dll","*.pdb","*.dll","*.exe" | Where-Object { $_.FullName -like "*CanBeYours*" } | ForEach-Object {
        $content = Get-Content $_.FullName -Raw -ErrorAction SilentlyContinue
        if ($content) {
            $content = $content -replace "CanBeYours\.", "$solutionName."
            Set-Content $_.FullName $content -NoNewline
        }
    }
}
finally {
    # Return to original directory
    Pop-Location
}

Write-Host "`nSetup completed successfully!" -ForegroundColor Green
Write-Host "`nYour CodeBlock Dev Kit SaaS application has been configured with the following settings:" -ForegroundColor Blue
Write-Host "Solution Name: " -NoNewline
Write-Host $solutionName -ForegroundColor Yellow
Write-Host "Application Name: " -NoNewline
Write-Host $appName -ForegroundColor Yellow
Write-Host "Application URL: " -NoNewline
Write-Host $appUrl -ForegroundColor Yellow
Write-Host "Admin Email: " -NoNewline
Write-Host $adminEmail -ForegroundColor Yellow

Write-Host "`nYou can now build and run your application." -ForegroundColor Green 