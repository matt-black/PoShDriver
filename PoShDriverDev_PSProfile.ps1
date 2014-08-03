#setup stuff
$poshRootDir = $PSScriptRoot
$testpageUrl = "file:///" + [System.String]::Format("{0}\Test\Resources\testpage.html",$poshRootDir).Replace('\', '/')

#set window properties
$window = (Get-Host).UI.RawUI
$window.WindowTitle = "PoShDriver Dev Shell"

#add the PoShDriver Snapin
Add-PSSnapin PoShDriver

#add the Selenium DLLs
Add-Type -Path $poshRootDir\packages\Selenium.WebDriver.2.42.0\lib\net40\WebDriver.dll
Add-Type -Path $poshRootDir\packages\Selenium.Support.2.42.0\lib\net40\WebDriver.Support.dll

#add Psake and Pester modules, along with custom TestUtil module
Import-Module $poshRootDir\packages\psake.4.3.2\tools\psake.psm1
Import-Module $poshRootDir\packages\Pester.2.1.0\tools\Pester.psm1

#add Chromedriver to the path
$env:Path += [System.String]::Format(";{0}\packages\WebDriver.ChromeDriver.win32.2.10.0.0\content", $poshRootDir)