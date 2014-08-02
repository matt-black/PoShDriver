using System;
using System.Collections;
using System.Collections.Generic;
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

        protected override void ProcessRecord()
        {
            switch (this.ParameterSetName)
            {
                case "Displayed":
                    this.WriteObject(_webElement.Displayed);
                    return;
                case "Enabled":
                    this.WriteObject(_webElement.Displayed);
                    return;
                case "Location":
                    this.WriteObject(_webElement.Location);
                    return;
                case "Selected":
                    this.WriteObject(_webElement.Selected);
                    return;
                case "Size":
                    this.WriteObject(_webElement.Size);
                    return;
                case "TagName":
                    this.WriteObject(_webElement.TagName);
                    return;
                case "Text":
                    this.WriteObject(_webElement.Text);
                    return;
                case "All":
                    Dictionary<string, object> propsDict = new Dictionary<string, object>();

                    var props = _webElement.GetType().GetProperties();
                    foreach (var prop in props)
                    {
                        propsDict.Add(prop.Name, prop.GetValue(_webElement, null));
                    }
                    this.WriteObject(new Hashtable(propsDict));
                    return;
                default:
                    throw new Exception("did not specify a valid parameterset");
            }
        }
    }
}
