using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management.Automation;

using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace PowerShellDriver.Commands
{
    /// <summary>
    /// Represents a Cmdlet that takes in a <see cref="INavigation"/> object and acts on that object's methods
    /// to move it through a browser's history or to another webpage
    /// </summary>
    public abstract class INavigationCmdlet : PSCmdlet
    {
        /// <summary>
        /// The <see cref="INavigation"/> object to operate on
        /// </summary>
        [Parameter(Mandatory = true,
            ValueFromPipeline = true)]
        [ValidateNotNull]
        public INavigation Navigator
        {
            get { return _navigator; }
            set { _navigator = value; }
        }
        protected INavigation _navigator;

        /// <summary>
        /// The navigation action to take
        /// </summary>
        protected Action navigationAction;

        protected override void ProcessRecord()
        {
            try
            {
                navigationAction.Invoke();
            }
            catch (WebDriverException ex)
            {
                //form ErrorRecord
                ErrorRecord err = new ErrorRecord(ex, "navigation", ErrorCategory.InvalidOperation, _navigator);
                this.WriteError(err);

                throw (ex); //rethrow
            }

        }
    }

    /// <summary>
    /// Move back a single entry in the browser's history
    /// </summary>
    [Cmdlet(VerbsCommon.Move, "BackInBrowserHistory")]
    public class MoveBackInBrowserHistoryCmdlet : INavigationCmdlet
    {
        protected override void BeginProcessing()
        {
            this.navigationAction = 
                () => _navigator.Back();
        }
        protected override void ProcessRecord()
        {
            base.ProcessRecord();
        }
    }

    /// <summary>
    /// Move a single "item" forward in the browser's history
    /// </summary>
    [Cmdlet(VerbsCommon.Move, "ForwardInBrowserHistory")]
    public class MoveForwardInBrowserHistoryCmdlet : INavigationCmdlet
    {
        protected override void BeginProcessing()
        {
            this.navigationAction =
                () => _navigator.Forward();
        }
        protected override void ProcessRecord()
        {
            base.ProcessRecord();
        }
    }

    /// <summary>
    /// Refreshes the current webpage
    /// </summary>
    [Cmdlet(VerbsData.Update, "CurrentWebPage")]
    public class UpdateCurrentWebPageCmdlet : INavigationCmdlet
    {
        protected override void BeginProcessing()
        {
            this.navigationAction =
                () => _navigator.Refresh();
        }
        protected override void ProcessRecord()
        {
            base.ProcessRecord();
        }
    }

    /// <summary>
    /// Load a new web page in the current browser window
    /// </summary>
    [Cmdlet(VerbsCommon.Move, "BrowserToUrl")]
    public class MoveBrowserToUrlCmdlet : INavigationCmdlet
    {
        /// <summary>
        /// The URL of the site to go to
        /// </summary>
        [Parameter(Mandatory=true,
            Position=0)]
        [ValidateNotNullOrEmpty]
        public String Url
        {
            get { return _url; }
            set { _url = value; }
        }
        private String _url;

        protected override void BeginProcessing()
        {
            this.navigationAction =
                () => _navigator.GoToUrl(_url);
        }
        protected override void ProcessRecord()
        {
            base.ProcessRecord();
        }
    }
}
