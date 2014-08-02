Describe "Find-WebElement Cmdlet" {
    Context "Checking By implementations" {
        #setup the driver
        $htmlUnitCaps = [OpenQA.Selenium.Remote.DesiredCapabilities]::HtmlUnit()
        $driver = New-Object OpenQA.Selenium.Remote.RemoteWebDriver -ArgumentList @($htmlUnitCaps)
        $driver.Navigate().GoToUrl("file:///C:/Users/Matt/dotNet/PowerShellDriver/Test/Resources/testpage.html")   #load the test page

        #tests for finding elements by different methods
        It "can find by ClassName" {
            $elemClNa_dotNet = $driver.FindElementByClassName("saucelabsrip")
            $elemClNa_poSh = $driver | Find-WebElement "saucelabsrip" -ByClassName
            $elemClNa_poSh | Should Be $elemClNa_poSh   
        }
        It "can find by CssSelector" {
            $elemCss_dotNet = $driver.FindElementByCssSelector("#hobby-checkboxes")
            $elemCss_poSh = $driver | Find-WebElement "#hobby-checkboxes" -ByCssSelector
        }
        It "can find by Id" {
            $elemId_dotNet = $driver.FindElementById("alerts")
            $elemId_poSh = $driver | Find-WebElement "alerts" -ById
            $elemId_poSh | Should Be $elemId_dotNet
        }
        It "can find by LinkText" {
            $elemLt_dotNet = $driver.FindElementByLinkText("this page")
            $elemLt_poSh = $driver | Find-WebElement "this page" -ByLinkText
            $elemLt_poSh | Should Be $elemLt_dotNet
        }
        It "can find by Name" {
            $elemName_dotNet = $driver.FindElementByName("cycling")
            $elemName_poSh = $driver | Find-WebElement "cycling" -ByName
            $elemName_poSh | Should Be $elemName_dotNet
        }
        It "can find by PartialLinkText" {
            $elemPlt_dotNet = $driver.FindElementByPartialLinkText("this p")
            $elemPlt_poSh = $driver | Find-WebElement "this p" -ByPartialLinkText
            $elemPlt_poSh | Should Be $elemPlt_dotNet
        }
        It "can find by TagName" {
            $elemTN_dotNet = $driver.FindElementByTagName("h1")
            $elemTN_poSh = $driver | Find-WebElement "h1" -ByTagName
            $elemTN_poSh | Should Be $elemTN_dotNet
        }
        It "can find by XPath" {
            $elemXp_dotNet = $driver.FindElementByXPath("//*[@id=`"home`"]/h1")
            $elemXp_poSh = $driver | Find-WebElement "//*[@id=`"home`"]/h1" -ByXPath
            $elemXp_poSh | Should Be $elemXp_dotNet
        }

        #close out the driver
        $driver.Quit()
    }
}

Describe "Find-WebElements Cmdlet" {

}