Describe "Getting WebElement Properties" {
    Context "Get-WebElementProperty cmdlet" {
        #setup the driver
        $htmlUnitCaps = [OpenQA.Selenium.Remote.DesiredCapabilities]::HtmlUnit()
        $driver = New-Object OpenQA.Selenium.Remote.RemoteWebDriver -ArgumentList @($htmlUnitCaps)
        $driver.Navigate().GoToUrl("file:///C:/Users/Matt/dotNet/PowerShellDriver/Test/Resources/testpage.html")   #load the test page
        #get the webelement we'll test against
        $formElem = $driver.FindElementByName("cycling")

        It "gets the 'Displayed' property" {
            $formElem | Get-WebElementProperty -Displayed | Should Be $formElem.Displayed
        }
        It "gets the 'Enabled' property" {
            $formElem | Get-WebElementProperty -Enabled | Should Be $formElem.Enabled
        }
        It "gets the 'Location' property" {
            $formElem | Get-WebElementProperty -Location | Should Be $formElem.Location
        }
        It "gets the 'Selected' property" {
            $formElem | Get-WebElementProperty -Selected | Should Be $formElem.Selected
        }
        It "gets the 'Size' property" {
            $formElem | Get-WebElementProperty -Size | Should Be $formElem.Size
        }
        It "gets the 'TagName' property" {
            $formElem | Get-WebElementProperty -TagName | Should Be $formElem.TagName
        }
        It "gets the 'Text' property" {
            $formElem | Get-WebElementProperty -Text | Should Be $formElem.Text
        }
        It "cannot find two explicitly stated properties" {
            PesterThrow {
                $formElem | Get-WebElementProperty -TagName -Text
            } | Should Be $true
        }

        $driver.Quit()
    }
}