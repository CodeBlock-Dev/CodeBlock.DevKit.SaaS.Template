@echo off
cd /d "%~dp0..\.."

echo Running: nuke RunIntegrationTests
nuke RunIntegrationTests

pause 