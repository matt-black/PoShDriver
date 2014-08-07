using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management.Automation;

using OpenQA.Selenium;

namespace PowerShellDriver.Commands
{
    public abstract class IWebDriverCmdlet : PSCmdlet
    {
        [Parameter(Mandatory=true,
            ValueFromPipeline=true)]
        public IWebDriver WebDriver
        {
            get { return _webDriver; }
            set { _webDriver = value; }
        }
        protected IWebDriver _webDriver;
    }

    [Cmdlet(VerbsCommon.Get, "WebDriverProperty")]
    public class GetWebDriverProperty : IWebDriverCmdlet
    {
        [Parameter(ParameterSetName="PageSource",
            Mandatory=true)]
        public SwitchParameter PageSource
        {
            get { return _pageSource; }
            set { _pageSource = value; }
        }
        private bool _pageSource;

        [Parameter(ParameterSetName="Title",
            Mandatory=true)]
        public SwitchParameter Title
        {
            get { return _pageSource; }
            set { _pageSource = value; }
        }
        private bool _title;

        [Parameter(ParameterSetName="Url",
            Mandatory=true)]
        public SwitchParameter Url
        {
            get { return _url; }
            set { _url = value; }
        }
        private bool _url;

        [Parameter(ParameterSetName="WindowHandle",
            Mandatory=true)]
        public SwitchParameter WindowHandle
        {
            get { return _windowHandle; }
            set { _windowHandle = value; }
        }
        private bool _windowHandle;

        [Parameter(ParameterSetName = "WindowHandle",
            Mandatory = false)]
        public SwitchParameter All
        {
            get { return _all; }
            set { _all = value; }
        }
        internal bool _all;

        private Object returnPropVal;

        protected override void ProcessRecord()
        {
            switch (this.ParameterSetName)
            {
                case "PageSource":
                    returnPropVal = _webDriver.PageSource;
                    break;
                case "Title":
                    returnPropVal = _webDriver.Title;
                    break;
                case "Url":
                    returnPropVal = _webDriver.Url;
                    break;
                case "WindowHandle":
                    if (_all)
                        returnPropVal = _webDriver.WindowHandles;
                    else
                        returnPropVal = _webDriver.CurrentWindowHandle;
                    break;
                default:
                    throw new ArgumentException("did not specify a valid parameterset name");
            }
        }

        protected override void EndProcessing()
        {
            this.WriteObject(returnPropVal);
        }
    }
}
