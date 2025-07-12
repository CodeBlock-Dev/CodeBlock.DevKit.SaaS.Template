@echo off
cd /d "%~dp0..\.."

echo Running: nuke RunUnitTests
nuke RunUnitTests

pause 