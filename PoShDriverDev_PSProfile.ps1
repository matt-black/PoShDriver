#setup stuff
$rootDir = $PSScriptRoot

#set window properties
$window = (Get-Host).UI.RawUI
$window.WindowTitle = "PoShDriver Dev Shell"

#add the PoShDriver Snapin
Add-PSSnapin PoShDriver

#add the Selenium DLLs
Add-Type -Path $rootDir\packages\Selenium.WebDriver.2.42.0\lib\net40\WebDriver.dll
Add-Type -Path $rootDir\packages\Selenium.Support.2.42.0\lib\net40\WebDriver.Support.dll

#add Psake and Pester modules
Import-Module $rootDir\packages\psake.4.3.2\tools\psake.psm1
Import-Module $rootDir\packages\Pester.2.1.0\tools\Pester.psm1

#add Chromedriver to the path
$env:Path += [System.String]::Format(";{0}\packages\WebDriver.ChromeDriver.win32.2.10.0.0\content", $rootDir)

Clear-Host