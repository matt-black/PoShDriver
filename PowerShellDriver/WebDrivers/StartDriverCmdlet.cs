using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management.Automation;

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;

namespace PowerShellDriver.WebDrivers
{
    [Cmdlet(VerbsLifecycle.Start, "WebDriver")]
    public class StartDriverCmdlet : PSCmdlet, IDynamicParameters
    {
        #region Chrome param set
        [Parameter(Mandatory=true,
            ParameterSetName="chrome",
            Position=0)]
        public SwitchParameter Chrome
        {
            get { return _isChrome; }
            set { _isChrome = value; }
        }
        private bool _isChrome;

        /* CHROME PARAMETERS
         * Currently implemented: string-path to EXE, ChromeOptions
         */

        [Parameter(ParameterSetName="chrome",
            Mandatory=false,
            Position=1)]
        [ValidateNotNull]
        public String Path
        {
            get { return _pathToChromeDriver; }
            set { _pathToChromeDriver = value; }
        }
        private String _pathToChromeDriver;

        [Parameter(ParameterSetName = "chrome",
            Mandatory = false,
            Position = 2)]
        [ValidateNotNull]
        public ChromeOptions ChromeOptions
        {
            get { return _chromeOptions; }
            set { _chromeOptions = value; }
        }
        private ChromeOptions _chromeOptions;
        #endregion

        #region Firefox param set
        [Parameter(Mandatory=true,
            ParameterSetName="firefox",
            Position=0)]
        public SwitchParameter Firefox
        {
            get { return _isFirefox; }
            set { _isFirefox = value; }
        }
        private bool _isFirefox;

        /* FIREFOX PARAMETERS
         * Current impl: FirefoxProfile, FirefoxBinary
         */
        [Parameter(ParameterSetName="firefox",
            Mandatory=false,
            Position=2)]
        [ValidateNotNull]
        public FirefoxProfile Profile
        {
            get { return _ffProfile; }
            set { _ffProfile = value; }
        }
        private FirefoxProfile _ffProfile;

        [Parameter(ParameterSetName="firefox",
            Mandatory=false,
            Position=1)]
        [ValidateNotNull]
        public FirefoxBinary Binary
        {
            get { return _ffBinary; }
            set { _ffBinary = value; }
        }
        private FirefoxBinary _ffBinary;
        #endregion

        #region Remote dynamic param set
        [Parameter(Mandatory=false)]
        public SwitchParameter Remote
        {
            get { return _isRemote; }
            set { _isRemote = value; }
        }
        private bool _isRemote;

        /// <summary>
        /// Implements the IDynamicParameters interface
        /// Retrieves <see cref="RemoteParameters"/> if the -Remote switch is specified
        /// </summary>
        public object GetDynamicParameters()
        {
            if (_isRemote)
            {
                _remoteParams = new RemoteParameters();
                return _remoteParams;
            }
            return null;
        }
        private RemoteParameters _remoteParams;

        /// <summary>
        /// Any parameters that are relevant to a <see cref="RemoteWebDriver"/>
        /// </summary>
        public class RemoteParameters
        {
            [Parameter]
            [ValidateNotNullOrEmpty]
            public String Url
            {
                get { return _remoteUrl; }
                set { _remoteUrl = value; }
            }
            private String _remoteUrl;
        }
        #endregion

        /// <summary>
        /// Form a <see cref="DesiredCapabilities"/> object if the -Remote switch is specified
        /// </summary>
        protected override void ProcessRecord()
        {
            if (_isRemote) //it's a RemoteWebDriver
            {
                DesiredCapabilities remoteCaps = this.MakeCapabilities();
            }
        }

        /// <summary>
        /// Creates a <see cref="DesiredCapabilities"/> object based on the properties of the 
        /// </summary>
        /// <returns></returns>
        private DesiredCapabilities MakeCapabilities()
        {
            //get the browser switch to initialize the object
            DesiredCapabilities capabilities = new DesiredCapabilities();
            if (_isFirefox)
            {
                capabilities.SetCapability(CapabilityType.BrowserName, "firefox");
            }
            else if (_isChrome)
            {
                capabilities.SetCapability(CapabilityType.BrowserName, "chrome");
            }

            return capabilities;
        }
    }
}
