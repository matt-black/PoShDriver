using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management.Automation;

using OpenQA.Selenium;

namespace PowerShellDriver.Commands
{
    [Cmdlet(VerbsLifecycle.Invoke, "AlertMethod")]
    public class InvokeIAlertCmdlet : PSCmdlet
    {
        [Parameter(Mandatory=true,
            ValueFromPipeline=true)]
        public IAlert Alert
        {
            get { return _alert; }
            set { _alert = value; }
        }
        private IAlert _alert;

        /*[Parameter(Mandatory=false)]
        public SwitchParameter PassThru
        {
            get { return _passThru; }
            set { _passThru = value; }
        }
        private bool _passThru;*/

        [Parameter(ParameterSetName="Accept",
            Mandatory=true)]
        public SwitchParameter Accept
        {
            get { return _accept; }
            set { _accept = value; }
        }
        private bool _accept;

        [Parameter(ParameterSetName="Dismiss",
            Mandatory=true)]
        public SwitchParameter Dismiss
        {
            get { return _dismiss; }
            set { _dismiss = value; }
        }
        private bool _dismiss;

        [Parameter(ParameterSetName="TextProp",
            Mandatory=true)]
        public SwitchParameter GetAlertText
        {
            get { return _getAlertText; }
            set { _getAlertText = value; }
        }
        private bool _getAlertText;

        [Parameter(ParameterSetName = "SendKeys",
            Mandatory = true)]
        public SwitchParameter SendKeys
        {
            get { return _sendKeys; }
            set { _sendKeys = value; }
        }
        private bool _sendKeys;

        [Parameter(ParameterSetName="SendKeys",
            Mandatory=true,
            Position=1)]
        [ValidateNotNull]
        public String Text
        {
            get { return _text; }
            set { _text = value; }
        }
        private String _text;

        protected override void ProcessRecord()
        {
            switch (this.ParameterSetName)
            {
                case "Accept":
                    _alert.Accept();
                    break;
                case "Dismiss":
                    _alert.Dismiss();
                    break;
                case "SendKeys":
                    _alert.SendKeys(_text);
                    break;
                case "TextProp":
                    this.WriteObject(_alert.Text);
                    break;
                default:
                    throw new Exception("did not specify a valid parameter set");
            }
        }
    }
}
