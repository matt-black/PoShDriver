using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management.Automation;

using OpenQA.Selenium;

namespace PowerShellDriver.Commands
{
    /// <summary>
    /// Represents a Cmdlet that takes an <see cref="IWebElement"/> object from the pipeline
    /// </summary>
    public abstract class IWebElementCmdlet : PSCmdlet
    {
        [Parameter(Mandatory=true,
            ValueFromPipeline=true)]
        public IWebElement WebElement
        {
            get { return _webElement; }
            set { _webElement = value; }
        }
        protected IWebElement _webElement;
    }

    /// <summary>
    /// Gets the specified property of the passed-in <see cref="IWebElement"/>
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "WebElementProperty")]
    public class GetWebElementProperty : IWebElementCmdlet
    {
        #region SwitchParams for different properties
        [Parameter(ParameterSetName="Displayed",
            Mandatory=true)]
        public SwitchParameter Displayed
        {
            get { return _displayed; }
            set { _displayed = value; }
        }
        private bool _displayed;

        [Parameter(ParameterSetName="Enabled",
            Mandatory=true)]
        public SwitchParameter Enabled
        {
            get { return _enabled; }
            set { _enabled = value; }
        }
        private bool _enabled;

        [Parameter(ParameterSetName="Location",
            Mandatory=true)]
        public SwitchParameter Location
        {
            get { return _location; }
            set { _location = value; }
        }
        private bool _location;

        [Parameter(ParameterSetName="Selected",
            Mandatory=true)]
        public SwitchParameter Selected
        {
            get { return _selected; }
            set { _selected = value; }
        }
        private bool _selected;

        [Parameter(ParameterSetName="Size",
            Mandatory=true)]
        public SwitchParameter Size
        {
            get { return _size; }
            set { _size = value; }
        }
        private bool _size;

        [Parameter(ParameterSetName="TagName",
            Mandatory=true)]
        public SwitchParameter TagName
        {
            get { return _tagName; }
            set { _tagName = value; }
        }
        private bool _tagName;

        [Parameter(ParameterSetName="Text", 
            Mandatory=true)]
        public SwitchParameter Text
        {
            get { return _text; }
            set { _text = value; }
        }
        private bool _text;

        [Parameter(ParameterSetName="All",
            Mandatory=true)]
        public SwitchParameter All
        {
            get { return _all; }
            set { _all = value; }
        }
        private bool _all;
        #endregion

        /// <summary>
        /// Indicates what to write to the pipeline
        /// If true, writes the <see cref="IWebElement"/> input to the pipeline
        /// If false (unspecified), it writes the property to the pipeline
        /// </summary>
        public SwitchParameter PassThru
        {
            get { return _passThru; }
            set { _passThru = value; }
        }
        private bool _passThru = false;

        protected override void ProcessRecord()
        {
            var propertyValue = GetElementProperty(this.ParameterSetName);
            if (_passThru)
            {
                this.WritePropToVariable();
            }
            else
            {
                this.WriteObject(propertyValue);
                return;
            }
            this.WriteObject(_webElement);
        }

        #region Private support methods
        /// <summary>
        /// Generate a <see cref="Dictionary<string,object>"/> of all the properties of the <see cref="IWebElement"/> that
        /// this Cmdlet had passed to it
        /// </summary>
        /// <returns>A dictionary of properties</returns>
        private Dictionary<string, object> MakePropertiesDict()
        {
            Dictionary<string, object> propsDict = new Dictionary<string, object>();

            var props = _webElement.GetType().GetProperties();
            foreach (var prop in props)
            {
                propsDict.Add(prop.Name, prop.GetValue(_webElement, null));
            }
            return propsDict;
        }

        /// <summary>
        /// Writes the property specified by the Cmdlet's parameter to a variable in the current runspace
        /// </summary>
        private void WritePropToVariable()
        {
            StringBuilder varName = new StringBuilder("elementProperty_");
            varName.Append(this.ParameterSetName);
            var varValue = GetElementProperty(this.ParameterSetName);
            this.SessionState.PSVariable.Set(varName.ToString(), varValue);
        }

        /// <summary>
        /// Gets the value of the specified property for the _webElement
        /// </summary>
        private object GetElementProperty(string p)
        {
            var props = _webElement.GetType().GetProperties();
            var value = props
                .Where(prop => prop.Name == p)
                .Select(prop => prop.GetValue(_webElement, null));
            return value;
        }
        #endregion
    }

    [Cmdlet(VerbsLifecycle.Invoke, "WebElementMethod")]
    public class InvokeWebElementMethod : IWebElementCmdlet, IDynamicParameters
    {
        private bool _reqStringParam = false;

        #region SwitchParams for different methods
        [Parameter(ParameterSetName="Clear",
            Mandatory=true)]
        public SwitchParameter Clear
        {
            get { return _clear; }
            set { _clear = value; }
        }
        private bool _clear;

        [Parameter(ParameterSetName="Click",
            Mandatory=true)]
        public SwitchParameter Click
        {
            get { return _click; }
            set { _click = value; }
        }
        private bool _click;

        [Parameter(ParameterSetName="GetAttribute",
            Mandatory=true)]
        public SwitchParameter GetAttribute
        {
            get { return _getAttribute; }
            set
            {
                if (value == true)
                {
                    _reqStringParam = true;
                }
                _getAttribute = value;
            }
        }
        private bool _getAttribute;

        [Parameter(ParameterSetName="GetCssValue",
            Mandatory=true)]
        public SwitchParameter GetCssValue
        {
            get { return _getCssValue; }
            set
            {
                if (value == true)
                {
                    _reqStringParam = true;
                }
                _getCssValue = value;
            }
        }
        private bool _getCssValue;

        [Parameter(ParameterSetName="SendKeys",
            Mandatory=true)]
        public SwitchParameter SendKeys
        {
            get { return _sendKeys; }
            set
            {
                if (value == true)
                {
                    _reqStringParam = true;
                }
                _sendKeys = value;
            }
        }
        private bool _sendKeys;

        [Parameter(ParameterSetName="Submit",
            Mandatory=true)]
        public SwitchParameter Submit
        {
            get { return _submit; }
            set { _submit = value; }
        }
        private bool _submit;
        #endregion

        /// <summary>
        /// Indicates what to write to the pipeline
        /// If true, writes the <see cref="IWebElement"/> input to the pipeline
        /// If false (unspecified), it writes the method output to the pipeline 
        /// </summary>
        [Parameter(Mandatory = false)]
        public SwitchParameter PassThru
        {
            get { return _passThru; }
            set { _passThru = value; }
        }
        private bool _passThru;

        /// <summary>
        /// Gets the dynamic <see cref="StringParam"/> if the method
        /// requires it
        /// </summary>
        /// <returns></returns>
        public object GetDynamicParameters()
        {
            if (_reqStringParam)
            {
                _stringParam = new StringParam();
                return _stringParam;
            }
            return null;
        }
        private StringParam _stringParam;

        /// <summary>
        /// A class representing a single string parameter for a <see cref="IWebElement"/> member method
        /// </summary>
        public class StringParam
        {
            [Parameter(Mandatory=true, 
                Position=1)]
            [ValidateNotNull]
            public String Parameter
            {
                get { return strParam; }
                set { strParam = value; }
            }
            private String strParam;
        }

        protected override void ProcessRecord()
        {
            switch (this.ParameterSetName)
            {
                case "Clear":
                    _webElement.Clear();
                    return;
                case "Click":
                    _webElement.Click();
                    return;
                case "GetAttribute":
                    this.WriteObject(
                        _webElement.GetAttribute(_stringParam.Parameter));
                    return;
                case "GetCssValue":
                    this.WriteObject(
                        _webElement.GetCssValue(_stringParam.Parameter));
                    return;
                case "SendKeys":
                    _webElement.SendKeys(_stringParam.Parameter);
                    return;
                case "Submit":
                    _webElement.Submit();
                    return;
                default:
                    throw new Exception("Invalid method specified");
            }
        }
    }
}
