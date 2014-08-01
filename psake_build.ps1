<##################
Build script for PoShDriver
It builds the new DLL using MSBuild, runs unit/integration tests in Pester, and register the newly built DLL to the system PowerShell

NOTE: This script should be run as an administrator, so that you can use installutil

Author: Matt Black
###################>



##CONVENIENCE FUNCTIONS FOR THE SCRIPT
#get the directory the script is executing from (root directory of project)
function Get-ScriptDirectory 
{
    Split-Path -Parent $PSCommandPath
}

#test whether the user running the script is an admin
function Test-Admin
{
    If (-NOT ([Security.Principal.WindowsPrincipal] [Security.Principal.WindowsIdentity]::GetCurrent()).IsInRole([Security.Principal.WindowsBuiltInRole] "Administrator")) 
    {
        #not running as admin
        return $false
    } 
    Else 
    {
        #running as admin
        return $true
    }
}

#test if the PoShDriver is already registered with PowerShell on this system
function Test-AlreadyRegistered
{
    $driverExist = Get-PSSnapin -Registered | Where-Object { $_.Name -eq "PoShDriver" }
    If ($driverExist -eq $null)
    {
        return $false
    }
    Else
    {
        return $true
    }
}

$framework = '4.0'
$base_dir = Get-ScriptDirectory
$sln_file = $base_dir + '\PowerShellDriver.sln'

task default -depends CheckAdmin,Rebuild,Test
task Rebuild -depends Unregister,Clean,Build
task Test -depends Register,RunTests

task CheckAdmin {
    #checks if the user is running the script as admin and fails if they are not
    if (Test-Admin) 
    {
        Write-Host Yay! Running as admin. Continuing build.
    }
    else
    {
        Write-Host Not running as admin. Failing the PSake build. Please run as admin.
        throw "ERROR: Need to run build script as admin"
    }
}

task Clean {
    #cleans the build
    exec { msbuild $slnPath '/t:Clean' }
}

task Build {
    #builds PoShDriver from latest sources
    exec { msbuild $slnPath '/t:Build' }
}

task RunTests {
    #run Pester tests
    cmd /c ($base_dir + "\Test\pester.bat")
    Write-Host $testResults
}

task Unregister {
    #unregister the new DLL as a snap-in on system powershell
    If (Test-AlreadyRegistered)
    {
        #it's already registered, need to unregister
        Write-Host Unregistering the previous version of PoShDriver from PowerShell
        exec { C:\Windows\Microsoft.net\Framework\V4.0.30319\installutil /uninstall ($base_dir + "\PowerShellDriver\bin\Debug\PoShDriver.dll") }
    }
    Else
    {
        #it's not registered, don't need to do anything
        Write-Host PoShDriver is not registered as a PSSnapin on this system. Skipping unregistration of previous version...
    }
}

task Register {
    #register the new DLL as a snap-in on system powershell
    Write-Host Registering the newly-built PoShDriver DLL to PowerShell...
    exec {C:\Windows\Microsoft.net\Framework\V4.0.30319\installutil ($base_dir + "\PowerShellDriver\bin\Debug\PoShDriver.dll") }
}