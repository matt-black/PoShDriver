Describe "Browser history navigation tests" {
    $website = "http://google.com"
    Context "Navigating back" {
        #test setup
        $htmlUnitCaps = [OpenQA.Selenium.Remote.DesiredCapabilities]::HtmlUnit()
        $driver = New-Object OpenQA.Selenium.Remote.RemoteWebDriver -ArgumentList @($htmlUnitCaps)
        $driver.Navigate().GoToUrl("file:///C:/Users/Matt/dotNet/PowerShellDriver/Test/Resources/testpage.html")   #load the test page

        It "matches .NET for back w/o history" {
            $dotNet = PesterThrow { $driver.Navigate().Back() }
            $poSh = PesterThrow { $driver | Move-BackInBrowserHistory }
            $poSh | Should Be $dotNet
        }
        It "can navigate back in browser history" {
            $origTitle = $driver.Title
            $driver.Navigate().GoToUrl($website)
            Move-BackInBrowserHistory -Navigator $driver
            $driver.Title | Should Be $origTitle
        }

        #close the driver
        $driver.Quit()
    }
    Context "Navigating forward" {
        #test setup
        $htmlUnitCaps = [OpenQA.Selenium.Remote.DesiredCapabilities]::HtmlUnit()
        $driver = New-Object OpenQA.Selenium.Remote.RemoteWebDriver -ArgumentList @($htmlUnitCaps)
        $driver.Navigate().GoToUrl("file:///C:/Users/Matt/dotNet/PowerShellDriver/Test/Resources/testpage.html")   #load the test page

        #tests
        It "throws if there is no forward history" {
            $dotNet = PesterThrow { $driver.Navigate().Forward() }
            $poSh = PesterThrow { $driver | Move-ForwardInBrowserHistory }
            $poSh | Should Be $dotNet
        }
        It "can navigate forward in browser history" {
            $driver.Navigate().GoToUrl($website)
            $siteTitle = $driver.Title
            $driver.Navigate().Back()
            $driver | Move-ForwardInBrowserHistory
            $driver.Title | Should Be $siteTitle
        }

        #close the driver
        $driver.Quit()
    }
    Context "Going to a Url" {
        #test setup
        $htmlUnitCaps = [OpenQA.Selenium.Remote.DesiredCapabilities]::HtmlUnit()
        $driver = New-Object OpenQA.Selenium.Remote.RemoteWebDriver -ArgumentList @($htmlUnitCaps)
        $driver.Navigate().GoToUrl("file:///C:/Users/Matt/dotNet/PowerShellDriver/Test/Resources/testpage.html")   #load the test page

        #tests
        It "can navigate to a URL" {
            #get the right title to check
            $driver.Navigate().GoToUrl($website)
            $siteTitle = $driver.Title
            $driver.Navigate().Back()
            #go back and then use our cmdlet to get back to the original page
            $driver | Move-BrowserToUrl $website
            $driver.Title | Should Be $siteTitle
        }

        #close the driver
        $driver.Quit()
    }
}