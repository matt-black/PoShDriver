Describe "Get-WebDriverProperty Cmdlet" {
    Context "functionality -- same returnvals as .NET bindings" {
        #test setup
        $htmlUnitCaps = [OpenQA.Selenium.Remote.DesiredCapabilities]::HtmlUnit()
        $driver = New-Object OpenQA.Selenium.Remote.RemoteWebDriver -ArgumentList @($htmlUnitCaps)
        $driver.Navigate().GoToUrl("file:///C:/Users/Matt/dotNet/PowerShellDriver/Test/Resources/testpage.html")   #load the test page

        It "returns the pagesource" {
            $driver | Get-WebDriverProperty -PageSource | Should Be $driver.PageSource
        }
        It "returns the title" {
            $driver | Get-WebDriverProperty -Title | Should Be $driver.Title
        }
        It "returns the url" {
            $driver | Get-WebDriverProperty -Url | Should Be $driver.Url
        }
        It "returns the current window handle" {
            $driver | Get-WebDriverProperty -WindowHandle | Should Be $driver.CurrentWindowHandle
        }
        It "returns all the window handles" {
            $driver | Get-WebDriverProperty -WindowHandle -All | Should Be $driver.WindowHandles
        }

        $driver.Quit()
    }
}