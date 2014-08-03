using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management.Automation;

using OpenQA.Selenium;

namespace PowerShellDriver.WebDriver
{
    [Cmdlet(VerbsOther.Use, "DriverInterface")]
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

        [Parameter(ParameterSetName="Navigate",
            Mandatory=true)]
        public SwitchParameter Navigate
        {
            get { return _iNavigation; }
            set { _iNavigation = value; }
        }
        private bool _iNavigation;

        [Parameter(ParameterSetName="Manage",
            Mandatory=true)]
        public SwitchParameter Manage
        {
            get { return _iOptions; }
            set { _iOptions = value; }
        }
        private bool _iOptions;

        [Parameter(ParameterSetName="SwitchTo",
            Mandatory=true)]
        public SwitchParameter SwitchTo
        {
            get { return _iTargetLocator; }
            set { _iTargetLocator = value; }
        }
        private bool _iTargetLocator;

        protected override void ProcessRecord()
        {
            switch (this.ParameterSetName)
            {
                case "Navigate":
                    this.WriteObject(_webDriver.Navigate());
                    break;
                case "Manage":
                    this.WriteObject(_webDriver.Manage());
                    break;
                case "SwitchTo":
                    this.WriteObject(_webDriver.SwitchTo());
                    break;
                default:
                    break;
            }
        }
    }
}
