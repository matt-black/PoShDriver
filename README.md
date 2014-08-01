PoShDriver
===================
PoSh driver is a port of the Selenium WebDriver framework to Windows Powershell. It can be installed as a PSSnapin for PowerShell v3. 

**NOTE**: this is currently in development and is nowhere near ready to be used. Pull requests more than welcome. 

## Requirements##
- A Windows box (obviously)
- Powershell v3
- .NET 4.0

## Goals
1. Fully-featured port of the Selenium WebDriver framework
2. Uses native Powershell workflows - scripts should look like regular PowerShell scripts
3. Relatively version-agnostic port of Webdriver. Should rely on WebDriver .NET bindings heavily. 

## Developer Setup

Some of the setup will pollute your native Powershell install (it'll register a SnapIn). It's currently setup for my convenience, so that you don't have to re-configure PowerShell every time you want to run/test/debug PoshDriver.

### What You'll Need
- Visual Studio
- .NET 4
- Powershell v3 (ISE is helpful, as well)

### Get Started
- Clone/fork whatever this repo
- Grab all the dependencies
	- All project dependencies are managed by NuGet.
	- Look here (http://docs.nuget.org/docs/reference/package-restore) for instructions on restoring packages
- (Optional) Powershell configuration
	- the psake build script will more-or-less handle this for you, registering the latest Debug version of the PoShDriver DLL to your PowerShell (x86 only).
	- Included in this repo is a .psc1 file that configures a Powershell environment for PoShDriver.

You can create your own PowerShell equipped with PoShDriver by building the .SLN file and running: 

    Powershell.exe -PSConsoleFile {project root}\dbgPsConfig.psc1


### Building PoShDriver

There's two ways to build PoShDriver.

#### In Visual Studio

Uses MSBuild. No special scripts, just builds the DLL. Really just a convenience for quick checks that code didn't break anything/compiles properly.

#### Using PSake

PSake is included as a NuGet dependency. This is *the* build script for PoShDriver. It must be run as Administrator.

It will clean, build, and run all of the Pester tests. To run the PSake build script: 

1. Run PowerShell *As Administrator*
2. Import the PSake module into PowerShell: `Import-Module <project root>\packages\psake.4.3.2\tools\psake.psm1`
3. Run the command `Invoke-psake -buildFile <project root>\psake_build.ps1`

**Note**: The script only registers the PoShDriver DLL to the x86 version of PowerShell. If you're on a 64-bit system, I'd recommend only using PowerShell (x86).

