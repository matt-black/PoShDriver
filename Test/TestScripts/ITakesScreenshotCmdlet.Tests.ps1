Describe "ITakesScreenshotCmdlet" {
    Context "Basic functionality" {
        #setup the driver -- need to use Firefox because HtmlUnit doesn't support screenshots
        $driver = New-Object OpenQA.Selenium.Firefox.FirefoxDriver
        $driver.Navigate().GoToUrl("file:///C:/Users/Matt/dotNet/PowerShellDriver/Test/Resources/testpage.html")   #load the test page
        
        $dotNetShot = $driver.GetScreenshot()
        It "returns a screenshot just like .NET" {
            $poShShot = $driver | Get-WebPageScreenshot -AsSeScreenshot
            $poShShot | Should Be $dotNetShot
        }
        It "can return a screenshot as a Base64-encoded string" {
            $poShShot = $driver | Get-WebPageScreenshot -AsBase64EncodedString
            $poShShot | Should Be $dotNetShot.AsBase64EncodedString
        }
        <#It "can return a screenshot as a Byte Array" {
            $poShShot = $driver | Get-WebPageScreenshot -AsByteArray
            $poShShot | Should Be $dotNetShot.AsByteArray
        }#>
        It "can save the screenshot to a file" {
            $driver | Get-WebPageScreenshot -SaveAsFile "poShShot.jpeg" ([System.Drawing.Imaging.ImageFormat]::Jpeg)
            $poShShot_byteArray = [System.IO.File]::ReadAllBytes("poShShot.jpeg")
            $poShShot_byteArray | Should Be $dotNetShot.AsByteArray
        }
        #remove the poShShot file
        Remove-Item "poShShot.jpeg"

        $driver.Quit()
    }

    Context "passThru functionality" {
        #setup the driver -- need to use Firefox because HtmlUnit doesn't support screenshots
        $driver = New-Object OpenQA.Selenium.Firefox.FirefoxDriver
        $driver.Navigate().GoToUrl("file:///C:/Users/Matt/dotNet/PowerShellDriver/Test/Resources/testpage.html")   #load the test page
        
        $dotNetShot = $driver.GetScreenshot()
        It "writes to the 'last_screenshot' variable" {
            $poShShot = $driver | Get-WebPageScreenshot -AsSeScreenshot -PassThru
            $last_screenshot | Should Be $dotNetShot
        }
        It "passes the driver through to next cmd" {
            $poShShot = $driver | Get-WebPageScreenshot -AsSeScreenshot -PassThru | Get-WebPageScreenshot -AsBase64EncodedString
            $poShShot | Should Be $dotNetShot.AsBase64EncodedString
        }

        $driver.Quit()
    }
}