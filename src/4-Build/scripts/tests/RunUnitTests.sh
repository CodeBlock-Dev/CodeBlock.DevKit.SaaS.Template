#!/bin/bash
cd "$(dirname "$(dirname "$(dirname "$0")")")"

echo "Running: nuke RunUnitTests"
nuke RunUnitTests 