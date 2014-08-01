<#
Tests for starting a local WebDriver
#>
Describe "Starting a new local WebDriver" {
    Context "FirefoxDriver" {
        #create the firefox profile object
        $ffProfile = New-Object OpenQA.Selenium.Firefox.FirefoxProfile
        $ffProfile.EnableNativeEvents = $true
        #create the firefox binary object
        $ffDir = Get-ItemProperty HKLM:\SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\* | Where-Object -Property DisplayName -Like "Mozilla Firefox*" | Select-Object -Property InstallLocation
        $ffBinary = New-Object OpenQA.Selenium.Firefox.FirefoxBinary -ArgumentList @([System.String]::Format("{0}\firefox.exe", $ffDir.InstallLocation))
        
        It "doesn't have to take any params" {
            $dotNetDriver = New-Object OpenQA.Selenium.Firefox.FirefoxDriver
            Try {
                $poshDriver = Start-Driver -Firefox 
                $poshDriver | Should Be $dotNetDriver
            } Finally {
                $dotNetDriver.Quit()
                $poshDriver.Quit()
            }

        }
        It "can accept a firefox profile" {
            $dotNetDriver = New-Object OpenQA.Selenium.Firefox.FirefoxDriver -ArgumentList @($ffProfile)
            Try {
                $poshDriver = Start-Driver -Firefox $ffProfile
                $poshDriver | Should Be $dotNetDriver
            } Finally {
                $dotNetDriver.Quit()
                $poshDriver.Quit()
            }
        }
        It "can accept a profile and a binary" {
            $dotNetDriver = New-Object OpenQA.Selenium.Firefox.FirefoxDriver -ArgumentList @($ffProfile, $ffBinary)
            Try {
                $poshDriver = Start-Driver -Firefox $ffProfile $ffBinary
                $poshDriver | Should Be $dotNetDriver
            } Finally {
                $dotNetDriver.Quit()
                $poshDriver.Quit()
            }
        }
    }
    Context "ChromeDriver" {
        $chromedriverDir = [System.String]::Format("{0}\packages\WebDriver.ChromeDriver.win32.2.10.0.0\content", $rootDir)
        $env:Path += [System.String]::Format(";{0}", $chromedriverDir) #add chromedriver.exe to PATH
        #create a ChromeOptions object
        $chromeOpts = New-Object OpenQA.Selenium.Chrome.ChromeOptions
        $chromeOpts.AddArgument("--test-type")

        It "doesn't have to take any parameters" {
            $dotNetDriver = New-Object OpenQA.Selenium.Chrome.ChromeDriver
            Try {
                $poshDriver = Start-Driver -Chrome
                $poshDriver | Should Be $dotNetDriver
            } Finally {
                $dotNetDriver.Quit()
                $poshDriver.Quit()
            }
        }
        It "can take the full path to the chromedriver" {
            $dotNetDriver = New-Object OpenQA.Selenium.Chrome.ChromeDriver -ArgumentList @($chromedriverDir)
            Try {
                $poshDriver = Start-Driver -Chrome $chromedriverDir
                $poshDriver | Should Be $dotNetDriver
            }
            Finally {
                $dotNetDriver.Quit()
                $poshDriver.Quit()
            }
        }
        It "can take a ChromeOptions object" {
            $dotNetDriver = New-Object OpenQA.Selenium.Chrome.ChromeDriver -ArgumentList @($chromeOpts)
            Try {
                $poshDriver = Start-Driver -Chrome -ChromeOptions $chromeOpts
                $poshDriver | Should Be $dotNetDriver
            } Finally {
                $dotNetDriver.Quit()
                $poshDriver.Quit()
            }
        }
    }
}

<#
Tests for the Start-Driver -Remote flag
#>
Describe "Starting a new remote WebDriver" {
    Context "Starting a basic Firefox driver" {
        $desCaps = New-Object OpenQA.Selenium.Remote.DesiredCapabilities
        $desCaps.SetCapability("browserName", "firefox")
        It "need only take a browser" {
            $dotNetDriver = New-Object OpenQA.Selenium.Remote.RemoteWebDriver -ArgumentList @($desCaps)
            Try {
                $poshDriver = Start-WebDriver -Firefox -Remote
                $poshDriver | Should Be $dotNetDriver
            } Finally {
                $dotNetDriver.Quit()
                $poshDriver.Quit()
            }
        }
    }
}