Describe "Use-DriverInterface Cmdlet" {
    Context "Checking return types of the cmdlet" {
    #setup the driver
    $driver = New-Object OpenQA.Selenium.Remote.RemoteWebDriver -ArgumentList @([OpenQA.Selenium.Remote.DesiredCapabilities]::HtmlUnit())
    
        It "returns type INavigation for -INavigation flag" {
            $inav = $driver | Use-DriverAsInterface -INavigation
            $inav.GetType().IsAssignableFrom([OpenQA.Selenium.INavigation]) | Should Be $true
        }    
    }
}