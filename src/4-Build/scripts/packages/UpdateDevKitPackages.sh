#!/bin/bash
cd "$(dirname "$(dirname "$(dirname "$0")")")"

echo "*************************** Update CodeBlock.DevKit NuGet Packages ***************************"

read -p "Enter the CodeBlock.DevKit packages target version (e.g., 1.5.1, leave blank for 'latest version'): " version

echo "Running: nuke UpdateDevKitPackages --version $version"
nuke UpdateDevKitPackages --version $version

