Describe "InvokeIAlertCmdlet" {
    Context "Functionality" {
        #test setup -- need firefoxdriver for alerts
        $driver = New-Object OpenQA.Selenium.Firefox.FirefoxDriver
        $driver.Navigate().GoToUrl("file:///C:/Users/Matt/dotNet/PowerShellDriver/Test/Resources/testpage.html")   #load the test page

        It "accepts an alert" {
            $alert_button = $driver.FindElementById("confirmBox_button")
            $alert_button.Click()
            $alert = $driver.SwitchTo().Alert()
            $alert | Invoke-AlertMethod -Accept
            $driver.FindElementById("alert_textbox").GetAttribute("value") | Should Be "You pressed OK!"
        }
        It "dismisses an alert" {
            $alert_button = $driver.FindElementById("confirmBox_button")
            $alert_button.Click()
            $alert = $driver.SwitchTo().Alert()
            $alert | Invoke-AlertMethod -Dismiss
            $driver.FindElementById("alert_textbox").GetAttribute("value") | Should Be "You pressed Cancel!"
        }
        It "sends keys to an alert" {
            $alert_button = $driver.FindElementById("promptBox_button")
            $alert_button.Click()
            $alert = $driver.SwitchTo().Alert()
            $alert | Invoke-AlertMethod -SendKeys "Matt Black"
            $alert.Accept()
            $driver.FindElementById("alert_textbox").GetAttribute("value") | Should Be "Matt Black"
        }
        It "gets the text of the alert" {
            $alert_button = $driver.FindElementById("confirmBox_button")
            $alert_button.Click()
            $alert = $driver.SwitchTo().Alert()
            $alert | Invoke-AlertMethod -GetAlertText | Should Be "Press a button!"
        }

        $driver.Quit()
    }
}