using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management.Automation;

using OpenQA.Selenium;

namespace PowerShellDriver.WebDriver
{
    [Cmdlet(VerbsOther.Use, "DriverAsInterface")]
    public class UseDriverInterfaceCmdlet : PSCmdlet
    {
        [Parameter(Mandatory=true,
            ValueFromPipeline=true)]
        public IWebDriver WebDriver
        {
            get { return _webDriver; }
            set { _webDriver = value; }
        }
        private IWebDriver _webDriver;

        [Parameter(ParameterSetName="INavigation",
            Mandatory=true)]
        public SwitchParameter INavigation
        {
            get { return _iNavigation; }
            set { _iNavigation = value; }
        }
        private bool _iNavigation;

        [Parameter(ParameterSetName="IOptions",
            Mandatory=true)]
        public SwitchParameter IOptions
        {
            get { return _iOptions; }
            set { _iOptions = value; }
        }
        private bool _iOptions;

        [Parameter(ParameterSetName="ITargetLocator",
            Mandatory=true)]
        public SwitchParameter ITargetLocator
        {
            get { return _iTargetLocator; }
            set { _iTargetLocator = value; }
        }
        private bool _iTargetLocator;

        protected override void ProcessRecord()
        {
            switch (this.ParameterSetName)
            {
                case "INavigation":
                    this.WriteObject(_webDriver.Navigate());
                    break;
                case "IOptions":
                    this.WriteObject(_webDriver.Manage());
                    break;
                case "ITargetLocator":
                    this.WriteObject(_webDriver.SwitchTo());
                    break;
                default:
                    break;
            }
        }
    }
}
