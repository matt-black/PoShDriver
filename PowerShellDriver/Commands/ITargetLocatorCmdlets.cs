using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Management.Automation;

using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;

namespace PowerShellDriver.Commands
{
    public abstract class ITargetLocatorCmdlet : PSCmdlet
    {
        [Parameter(Mandatory=true,
            ValueFromPipeline=true)]
        [ValidateNotNull]
        public RemoteWebDriver Driver
        {
            get { return _driver; }
            set 
            { 
                _driver = value;
                _targetLocator = _driver.SwitchTo();
            }
        }
        protected RemoteWebDriver _driver;
        protected ITargetLocator _targetLocator;

        protected Func<Object> switchToAction;
        protected object returnVal;

        protected override void ProcessRecord()
        {
            returnVal = switchToAction.Invoke();
        }
        protected override void EndProcessing()
        {
            this.WriteObject(returnVal);
        }
    }

    /// <summary>
    /// Represents an <see cref="ITargetLocatorCmdlet"/> that implements a -PassThru switch for the WebDriver
    /// </summary>
    public abstract class ITargetLocatorWithPassThruCmdlet : ITargetLocatorCmdlet
    {
        [Parameter(Mandatory=false)]
        public SwitchParameter PassThru
        {
            get { return _passThru; }
            set { _passThru = value; }
        }
        protected bool _passThru;

        protected String passThruVarName;

        /// <summary>
        /// If we get to here, write the driver back out to the pipeline.
        /// Control flow to this in implementing classes.
        /// </summary>
        protected override void EndProcessing()
        {
            if (_passThru)
            {
                //write object to variable in the environment
                this.SessionState.PSVariable.Set(passThruVarName, returnVal);

                //write the driver back to the pipeline
                this.WriteObject(_driver); 
            }
            else
            {
                this.WriteObject(returnVal);
            }
        }
    }

    [Cmdlet(VerbsCommon.Switch, "ToActiveElement")]
    public class SwitchToActiveElementCmdlet : ITargetLocatorWithPassThruCmdlet
    {
        protected override void BeginProcessing()
        {
            this.passThruVarName = "switchedTo_element";
            this.switchToAction = (
                () => _targetLocator.ActiveElement());
        }
    }

    [Cmdlet(VerbsCommon.Switch, "ToAlert")]
    public class SwitchToAlertCmdlet : ITargetLocatorWithPassThruCmdlet
    {
        protected override void BeginProcessing()
        {
            this.passThruVarName = "switchedTo_alert";
            this.switchToAction = (
                () => _targetLocator.Alert());
        }
    }

    [Cmdlet(VerbsCommon.Switch, "ToDefaultContent")]
    public class SwitchToDefaultContent : ITargetLocatorCmdlet
    {
        protected override void BeginProcessing()
        {
            this.switchToAction = (
                () => _targetLocator.DefaultContent());
        }
    }

    [Cmdlet(VerbsCommon.Switch, "ToFrame")]
    public class SwitchToFrame : ITargetLocatorCmdlet
    {
        [Parameter(Mandatory = true,
            Position = 0)]
        [ValidateNotNullOrEmpty]
        public dynamic Frame
        {
            get { return _frame; }
            set { _frame = value; }
        }
        private dynamic _frame;

        protected override void BeginProcessing()
        {
            this.switchToAction = (
                () => _targetLocator.Frame(_frame));
        }
    }

    [Cmdlet(VerbsCommon.Switch, "ToWindow")]
    public class SwitchToWindowCmdlet : ITargetLocatorCmdlet
    {
        [Parameter(Mandatory=true,
            Position=0)]
        public String WindowName
        {
            get { return _windowName; }
            set { _windowName = value; }
        }
        private String _windowName;

        protected override void BeginProcessing()
        {
            this.switchToAction = (
                () => _targetLocator.Window(_windowName));
        }
    }
}
