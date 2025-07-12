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

# Update all .cs, .razor, and .cshtml files
Write-Host "Updating C#, Razor, and Blazor files..."
Get-ChildItem -Recurse -Include "*.cs","*.razor","*.cshtml" | ForEach-Object {
    $content = Get-Content $_.FullName
    $content = $content -replace "namespace CanBeYours\.", "namespace $solutionName."
    $content = $content -replace "using CanBeYours\.", "using $solutionName."
    $content = $content -replace "@using CanBeYours\.", "@using $solutionName."
    $content = $content -replace "@namespace CanBeYours\.", "@namespace $solutionName."
    $content | Set-Content $_.FullName
}

# Update appsettings.json files
Write-Host "Updating appsettings.json files..."
Get-ChildItem -Recurse -Filter "appsettings.json" | ForEach-Object {
    $jsonContent = Get-Content $_.FullName -Raw
    
    # Convert string to JSON object (compatible with older PowerShell versions)
    $json = $jsonContent | ConvertFrom-Json
    
    # Create a template if the JSON is empty
    if ($null -eq $json) {
        $json = @{
            Application = @{
                Default = @{
                    Name = ""
                    Url = ""
                }
            }
            Swagger = @{
                Title = ""
            }
            Identity = @{
                AdminUser = @{
                    Mobile = ""
                    Email = ""
                    Password = ""
                }
            }
            Security = @{
                EncryptionSymmetricKey = ""
            }
            ApiKey = ""
            JwtAuthentication = @{
                Key = ""
                Issuer = ""
            }
            Hangfire = @{
                MongoStorage = "mongodb://localhost:27017/CanBeYours-Hangfire"
            }
            Monitoring = @{
                Service = @{
                    Name = "CanBeYours-Monitoring"
                }
            }
            CookieAuthentication = @{
                CookieName = "CanBeYours-Auth"
            }
            Localization = @{
                CookieName = "CanBeYours-Language"
            }
            MongoDB = @{
                DatabaseName = "CanBeYours-DB"
            }
        }
    }

    # Update values using string replacement to preserve the JSON structure
    $jsonContent = $jsonContent -replace '"Name":\s*"[^"]*"', "`"Name`": `"$appName`""
    $jsonContent = $jsonContent -replace '"Url":\s*"[^"]*"', "`"Url`": `"$appUrl`""
    $jsonContent = $jsonContent -replace '"Title":\s*"[^"]*"', "`"Title`": `"$appName`""
    $jsonContent = $jsonContent -replace '"Mobile":\s*"[^"]*"', "`"Mobile`": `"$adminMobile`""
    $jsonContent = $jsonContent -replace '"Email":\s*"[^"]*"', "`"Email`": `"$adminEmail`""
    $jsonContent = $jsonContent -replace '"Password":\s*"[^"]*"', "`"Password`": `"$adminPassword`""
    $jsonContent = $jsonContent -replace '"EncryptionSymmetricKey":\s*"[^"]*"', "`"EncryptionSymmetricKey`": `"$encryptionKey`""
    $jsonContent = $jsonContent -replace '"ApiKey":\s*"[^"]*"', "`"ApiKey`": `"$apiKey`""
    $jsonContent = $jsonContent -replace '"Key":\s*"[^"]*"', "`"Key`": `"$jwtKey`""
    $jsonContent = $jsonContent -replace '"Issuer":\s*"[^"]*"', "`"Issuer`": `"$appUrl`""
    
    # Replace solution name in various settings
    $jsonContent = $jsonContent -replace "CanBeYours-", "$solutionName-"
    
    # Save the updated JSON
    $jsonContent | Set-Content $_.FullName -NoNewline
}

Write-Host "`nSetup completed successfully!"
Write-Host "Your SaaS application has been configured with the following settings:"
Write-Host "Solution Name: $solutionName"
Write-Host "Application Name: $appName"
Write-Host "Application URL: $appUrl"
Write-Host "Admin Email: $adminEmail"
Write-Host "`nYou can now open the solution in your IDE and start developing!" 