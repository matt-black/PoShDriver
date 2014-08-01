PoShDriver Tests
===================

This folder holds all of the tests for PoShDriver. 

Right now, these tests are all written in PowerShell, using  Pester (https://github.com/pester/Pester).

## Running the Tests
Tests are run in the psake build script.

**To manually run tests**, run the pester.bat file in this directory. 

### Requirements

- Firefox
- PowerShell v3

## Test Directory Structure
Matches the C# project structure. Pester.bat will run all the tests in all the subdirectories of /Test so keeping tests in subdirectories isn't a problem.

## Setup
Commands are tested against FirefoxDriver.