﻿using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Management.Automation;

using OpenQA.Selenium;

namespace PowerShellDriver.Commands
{
    public abstract class AbstractFindElemCmdlet : PSCmdlet
    {
        protected By _by; //the method to find the element by

        /// <summary>
        /// The <see cref="ISearchContext object to use to find the element"/>
        /// </summary>
        [Parameter(Mandatory=true,
            ValueFromPipeline=true)]
        public ISearchContext SearchContext
        {
            get { return _searchContext; }
            set { _searchContext = value; }
        }
        protected ISearchContext _searchContext;

        /// <summary>
        /// The string identifier. 
        /// In .NET bindings, this is passed as By.___(identifider)
        /// </summary>
        [Parameter(Mandatory=true,
            Position=0)]
        public String Identifier
        {
            get { return _identifier; }
            set { _identifier = value; }
        }
        protected String _identifier;

        #region By SwitchParameters
        [Parameter(Mandatory=true,
            ParameterSetName="ClassName")]
        public SwitchParameter ByClassName
        {
            get { return _byClassName; }
            set { _byClassName = value; }
        }
        protected bool _byClassName;

        [Parameter(Mandatory=true,
            ParameterSetName="CssSelector")]
        public SwitchParameter ByCssSelector
        {
            get { return _byCssSelector; }
            set { _byCssSelector = value; }
        }
        protected bool _byCssSelector;

        [Parameter(Mandatory=true,
            ParameterSetName="Id")]
        public SwitchParameter ById
        {
            get { return _byId; }
            set { _byId = value; }
        }
        protected bool _byId;

        [Parameter(Mandatory=true,
            ParameterSetName="LinkText")]
        public SwitchParameter ByLinkText
        {
            get { return _byLinkText; }
            set { _byLinkText = value; }
        }
        protected bool _byLinkText;

        [Parameter(Mandatory=true,
            ParameterSetName="Name")]
        public SwitchParameter ByName
        {
            get { return _byName; }
            set { _byName = value; }
        }
        protected bool _byName;

        [Parameter(Mandatory=true,
            ParameterSetName="PartialLinkText")]
        public SwitchParameter ByPartialLinkText
        {
            get { return _byPartialLinkText; }
            set { _byPartialLinkText = value; }
        }
        protected bool _byPartialLinkText;

        [Parameter(Mandatory=true,
            ParameterSetName="TagName")]
        public SwitchParameter ByTagName
        {
            get { return _byTagName; }
            set { _byTagName = value; }
        }
        protected bool _byTagName;

        [Parameter(Mandatory=true,
            ParameterSetName="XPath")]
        public SwitchParameter ByXPath
        {
            get { return _byXPath; }
            set { _byXPath = value; }
        }
        protected bool _byXPath;
        #endregion

        /// <summary>
        /// switch on the different parametersetnames and create an appropriate
        /// <see cref="By"/> function.
        /// </summary>
        protected override void BeginProcessing()
        {
            switch (ParameterSetName)
            {
                case "ClassName":
                    _by = By.ClassName(_identifier);
                    break;
                case "CssSelector":
                    _by = By.CssSelector(_identifier);
                    break;
                case "Id":
                    _by = By.Id(_identifier);
                    break;
                case "LinkText":
                    _by = By.LinkText(_identifier);
                    break;
                case "Name":
                    _by = By.Name(_identifier);
                    break;
                case "PartialLinkText":
                    _by = By.PartialLinkText(_identifier);
                    break;
                case "TagName":
                    _by = By.TagName(_identifier);
                    break;
                case "XPath":
                    _by = By.XPath(_identifier);
                    break;
                default:
                    // should only get here if ParameterSetName isn't implemented properly
                    throw new Exception("did not specify a valid ParameterSet");
            }
        }
    }

    #region ISearchContext Cmdlets
    [Cmdlet(VerbsCommon.Find, "WebElements")]
    public class FindElementsCmdlet : AbstractFindElemCmdlet
    {
        [Parameter(Mandatory=false)]
        public bool FirstOnly
        {
            get { return _first; }
            set { _first = value; }
        }
        private bool _first;
        protected override void BeginProcessing()
        {
            base.BeginProcessing();
        }
        protected override void ProcessRecord()
        {
            switch (_first)
            {
                case true:
                    this.WriteObject(_searchContext.FindElement(_by));
                    break;
                case false:
                    this.WriteObject(_searchContext.FindElements(_by));
                    break;
            }
        }
    }

    [Cmdlet(VerbsCommon.Find, "WebElement")]
    [OutputType(typeof(IWebElement))]
    public class FindElementCmdlet : AbstractFindElemCmdlet
    {
        protected override void BeginProcessing()
        {
            base.BeginProcessing();
        }
        protected override void ProcessRecord()
        {
            this.WriteObject(_searchContext.FindElement(_by));
        }
    }
    #endregion
}
