Describe "Switch-To<Whatever> Cmdlets" {
    Context "Switch-ToActiveElement" {
        #test setup
        $htmlUnitCaps = [OpenQA.Selenium.Remote.DesiredCapabilities]::HtmlUnit()
        $driver = New-Object OpenQA.Selenium.Remote.RemoteWebDriver -ArgumentList @($htmlUnitCaps)
        $driver.Navigate().GoToUrl("file:///C:/Users/Matt/dotNet/PowerShellDriver/Test/Resources/testpage.html")   #load the test page

        It "switches to the active element just like .NET" {
            $activeElem = $driver.SwitchTo().ActiveElement()
            $driver | Switch-ToActiveElement | Should Be $activeElem
        }
        It "switches to the active element when selected" {
            $driver.FindElementByName("firstname").Click()
            $activeElem = $driver.SwitchTo().ActiveElement()
            $driver | Switch-ToActiveElement | Should Be $activeElem
        }
        It "passes through the driver" {
            $driver2 = $driver | Switch-ToActiveElement -PassThru
            $driver2 | Should Be $driver
        }
        It "writes to the right variable in the PS environment" {
            $activeElem = $driver | Switch-ToActiveElement -PassThru | Switch-ToActiveElement
            $switchedTo_element | Should Be $activeElem
        }

        $driver.Quit()
    }
    Context "Switch-ToAlert" {
        #test setup
        $driver = New-Object OpenQA.Selenium.Firefox.FirefoxDriver  #need to use FF b.c. HtmlUnit doesn't have alerthandler
        $driver.Navigate().GoToUrl("file:///C:/Users/Matt/dotNet/PowerShellDriver/Test/Resources/testpage.html")   #load the test page

        It "switches to the alert" {
            $driver.FindElementById("alert_button").Click()
            $alert = $driver | Switch-ToAlert
            $alert.Text | Should Be "I am an alert box!"
        }
        $driver.SwitchTo().Alert().Accept()
        It "write the alert to the proper variable in the PS Session" {
            $driver.FindElementById("alert_button").Click()
            $driver2 = $driver | Switch-ToAlert -PassThru
            $switchedTo_alert.Accept()
            PesterThrow {
                $driver.FindElementById("alert_button")
            } | Should Be $false  #this will throw if the alert wasn't suppressed
        }
        It "passes the driver thru with -PassThu" {
            $driver.FindElementById("alert_button").Click()
            $driver2 = $driver | Switch-ToAlert -PassThru
            $driver2 | Should Be $driver
        }

        $driver.Quit()
    }
    Context "Switch-ToDefaultContent" {
        #test setup
        $htmlUnitCaps = [OpenQA.Selenium.Remote.DesiredCapabilities]::HtmlUnit()
        $driver = New-Object OpenQA.Selenium.Remote.RemoteWebDriver -ArgumentList @($htmlUnitCaps)
        $driver.Navigate().GoToUrl("file:///C:/Users/Matt/dotNet/PowerShellDriver/Test/Resources/testpage.html")   #load the test page
        
        It "" {

        }

        $driver.Quit()

    }
    Context "Switch-ToFrame" {
        #test setup
        $htmlUnitCaps = [OpenQA.Selenium.Remote.DesiredCapabilities]::HtmlUnit()
        $driver = New-Object OpenQA.Selenium.Remote.RemoteWebDriver -ArgumentList @($htmlUnitCaps)
        $driver.Navigate().GoToUrl("file:///C:/Users/Matt/dotNet/PowerShellDriver/Test/Resources/testpage.html")   #load the test page

        It "accepts an int32 as a param" {
            PesterThrow {
                $driver | Switch-ToFrame 0
            } | Should Be $false
        }
        $driver.Navigate().GoToUrl("file:///C:/Users/Matt/dotNet/PowerShellDriver/Test/Resources/testpage.html")
        It "accepts a String as a param" {
            PesterThrow {
                $driver | Switch-ToFrame "google_iframe"
            } | Should Be $false
        }
        $driver.Navigate().GoToUrl("file:///C:/Users/Matt/dotNet/PowerShellDriver/Test/Resources/testpage.html")
        It "accepts an IWebElement as a param" {
            $elem = $driver.FindElementById("google_iframe")
            PesterThrow {
                $driver.SwitchTo().Frame($elem)
            } | Should Be $false
        }
        $driver.Navigate().GoToUrl("file:///C:/Users/Matt/dotNet/PowerShellDriver/Test/Resources/testpage.html") 
        It "throws if an invalid type is specified as a param" {
            PesterThrow {
                $driver | Switch-ToFrame $true
            } | Should Be $true
        }
        $driver.Navigate().GoToUrl("file:///C:/Users/Matt/dotNet/PowerShellDriver/Test/Resources/testpage.html")
        It "finds the right frame" {
            $elem = $driver | Switch-ToFrame "google_iframe"
            $driver.SwitchTo().DefaultContent()
            $elem | Should Be $driver.SwitchTo().Frame("google_iframe")
        }
        
        $driver.Quit()
    }
    Context "Switch-ToWindow" {
        #test setup
        $htmlUnitCaps = [OpenQA.Selenium.Remote.DesiredCapabilities]::HtmlUnit()
        $driver = New-Object OpenQA.Selenium.Remote.RemoteWebDriver -ArgumentList @($htmlUnitCaps)
        $driver.Navigate().GoToUrl("file:///C:/Users/Matt/dotNet/PowerShellDriver/Test/Resources/testpage.html")   #load the test page
        
        It "" {

        }

        $driver.Quit()
    }
}