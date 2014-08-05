Describe "Get-WebElementProperty Cmdlet" {
    Context "Get individual properties" {
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
    Context "Get all properties" {
        #setup the driver
        $htmlUnitCaps = [OpenQA.Selenium.Remote.DesiredCapabilities]::HtmlUnit()
        $driver = New-Object OpenQA.Selenium.Remote.RemoteWebDriver -ArgumentList @($htmlUnitCaps)
        $driver.Navigate().GoToUrl("file:///C:/Users/Matt/dotNet/PowerShellDriver/Test/Resources/testpage.html")  #load the page

        #form the hashtable
        $formElem = $driver.FindElementByName("cycling")
        $formElemPropsHT = @{"Displayed"=$formElem.Displayed;
            "Enabled"=$formElem.Enabled;
            "Location"=$formElem.Location;
            "Selected"=$formElem.Selected;
            "Size"=$formElem.Size;
            "TagName"=$formElem.TagName;
            "Text"=$formElem.Text}

        It "returns a hashtable" {
            $formElemHT = $formElem | Get-WebElementProperty -All
            $formElemHT.GetType() | Should Be $formElemPropsHT.GetType()
        }
        It "matches all the properties of the .NET bindings" {
            $formElemPropsHT_poSh = $formElem | Get-WebElementProperty -All
            foreach ($key in $formElemPropsHT.Keys)
            {
                $formElemPropsHT_poSh[$key] | Should Be $formElemPropsHT[$key]
            }
        }
        It "supports the -PassThru param" {
            $formElem | Get-WebElementProperty -All -PassThru
            foreach ($key in $formElemPropsHT.Keys)
            {
                $elementProperty_All[$key] | Should Be $formElemPropsHT[$key]
            }
        }
        $driver.Quit()  #quit the driver after test completion
    }
    Context "Get properties while using PassThru" {
        #setup the driver
        $htmlUnitCaps = [OpenQA.Selenium.Remote.DesiredCapabilities]::HtmlUnit()
        $driver = New-Object OpenQA.Selenium.Remote.RemoteWebDriver -ArgumentList @($htmlUnitCaps)
        $driver.Navigate().GoToUrl("file:///C:/Users/Matt/dotNet/PowerShellDriver/Test/Resources/testpage.html")   #load the test page
        #get the webelement we'll test against
        $formElem = $driver.FindElementByName("cycling")

        It "writes to the current runspace" {
            $formElem | Get-WebElementProperty -Displayed -PassThru
            $elementProperty_Displayed | Should Be $formElem.Displayed
        }
        It "passes the webelement through" {
            $testprop = $formElem | Get-WebElementProperty -Displayed -PassThru | Get-WebElementProperty -Enabled
            $testprop | Should Be $formElem.Enabled
        }
        It "overwrites the same property" {
            $formElem | Get-WebElementProperty -Displayed -PassThru
            #elementProperty_Displayed should be True
            $formElem2 = $driver.FindElementById("invisible div")
            $formElem2 | Get-WebElementProperty -Displayed -PassThru
            $elementProperty_Displayed | Should Be $formElem2.Displayed
        }

        $driver.Quit()
    }
}

Describe "Invoke-WebElementMethod Cmdlet" {
    Context "Invoke methods on a WebElement" {
        #setup the driver
        $htmlUnitCaps = [OpenQA.Selenium.Remote.DesiredCapabilities]::HtmlUnit()
        $driver = New-Object OpenQA.Selenium.Remote.RemoteWebDriver -ArgumentList @($htmlUnitCaps)
        $driver.Navigate().GoToUrl("file:///C:/Users/Matt/dotNet/PowerShellDriver/Test/Resources/testpage.html")   #load the test page
        
        It "clears an element" {
            $elem = $driver.FindElementById("i_am_a_textbox")
            $elem.GetAttribute("value") | Should Be "i has no focus"  #make sure the box isn't already empty
            $elem | Invoke-WebElementMethod -Clear
            $elem.GetAttribute("value") | Should BeNullOrEmpty
        }
        It "clicks an element" {
            $elem = $driver.FindElementByName("cycling")
            $elem | Invoke-WebElementMethod -Click
            $elem.Selected | Should Be $true
        }
        It "gets the given attribute for an element" {
            $elem = $driver.FindElementByName("cycling")
            $type = $elem | Invoke-WebElementMethod -GetAttribute "type"
            $type | Should Be $elem.GetAttribute("type")
        }
        It "gets the given css value for an element" {
            $title = $driver.FindElementByTagName("h1")
            $cssValue = $title | Invoke-WebElementMethod -GetCssValue "color"
            $cssValue | Should be $title.GetCssValue("color")
        }
        It "sends keys to an element" {
            $textbox = $driver.FindElementByName("lastname")
            $textbox | Invoke-WebElementMethod -SendKeys "text_string1"
            $textbox.GetAttribute("value") | Should Be "text_string1"
        }
        It "submits a form" {
            $form = $driver.FindElementById("basic-form")
            $form | Invoke-WebElementMethod -Submit
            $driver.Url | Should Match "firstname"
        }

        $driver.Quit()
    }

    Context "Checking parameters" {
        #setup the driver
        $htmlUnitCaps = [OpenQA.Selenium.Remote.DesiredCapabilities]::HtmlUnit()
        $driver = New-Object OpenQA.Selenium.Remote.RemoteWebDriver -ArgumentList @($htmlUnitCaps)
        $driver.Navigate().GoToUrl("file:///C:/Users/Matt/dotNet/PowerShellDriver/Test/Resources/testpage.html")   #load the test page
        $elem = $driver.FindElementById("i_am_a_textbox")

        It "can only invoke one method at a time" {
            PesterThrow {
                $elem | Invoke-WebElementMethod -Clear -Click
            } | Should Be $true
        }
        It "throws if req'd string param not supplied" {
            PesterThrow {
                $elem | Invoke-WebElementMethod -SendKeys
            } | Should Be $true
        }

        $driver.Quit()
    }

    Context "PassThru parameter verification" {
        #setup the driver
        $htmlUnitCaps = [OpenQA.Selenium.Remote.DesiredCapabilities]::HtmlUnit()
        $driver = New-Object OpenQA.Selenium.Remote.RemoteWebDriver -ArgumentList @($htmlUnitCaps)
        $driver.Navigate().GoToUrl("file:///C:/Users/Matt/dotNet/PowerShellDriver/Test/Resources/testpage.html")   #load the test page
        $elem = $driver.FindElementById("i_am_a_textbox")

        It "can write to the current PS runspace" {
            $elem | Invoke-WebElementMethod -GetAttribute "value" -PassThru | Invoke-WebElementMethod -Click
            $elementMethod_GetAttribute | Should Be "i has no focus"
        }
        It "can pass the WebElement through with stringparams first" {
            $newelem = $driver.FindElementByName("lastname")
            $newelem | Invoke-WebElementMethod -SendKeys "new text" -PassThru | Invoke-WebElementMethod -Clear
            $newelem.GetAttribute("value") | Should Be ([System.String]::Empty)
        }
        It "can pass the WebElement through without stringparams first" {
            $elem | Invoke-WebElementMethod -Clear -PassThru | Invoke-WebElementMethod -SendKeys "new value"
            $elem.GetAttribute("value") | Should Be "new value"
        }
        $driver.Quit()
    }
}