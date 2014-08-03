using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management.Automation;

using OpenQA.Selenium;

namespace PowerShellDriver.Commands
{
    [Cmdlet(VerbsLifecycle.Invoke, "JavaScriptExecution")]
    public class IJavaScriptExecutorCmdlet : PSCmdlet
    {
        /// <summary>
        /// An object that implements <see cref="IJavaScriptExecutor"/>
        /// </summary>
        [Parameter(Mandatory=true,
            ValueFromPipeline=true)]
        public IJavaScriptExecutor JSExecutor
        {
            get { return _jsExecutor; }
            set { _jsExecutor = value; }
        }
        private IJavaScriptExecutor _jsExecutor;

        /// <summary>
        /// The JavaScript code to execute
        /// </summary>
        [Parameter(Mandatory=true,
            Position=0)]
        public String Script
        {
            get { return _script; }
            set { _script = value; }
        }
        private String _script;

        /// <summary>
        /// The arguments to the script
        /// </summary>
        [Parameter(Mandatory=false,
            Position=1)]
        public Object[] Args
        {
            get { return _args; }
            set { _args = value; }
        }
        private Object[] _args = new Object[0];

        /// <summary>
        /// Execute the script asynchronously
        /// </summary>
        [Parameter(Mandatory=false)]
        public SwitchParameter Async
        {
            get { return _async; }
            set { _async = value; }
        }
        private bool _async;

        [Parameter(Mandatory=false)]
        public SwitchParameter PassThru
        {
            get { return _passThru; }
            set { _passThru = value; }
        }
        private bool _passThru;

        protected override void ProcessRecord()
        {
            object returnVal = (_async) ?
                _jsExecutor.ExecuteAsyncScript(_script, _args) :
                _jsExecutor.ExecuteScript(_script, _args);

            if (_passThru)
                WriteVarAndPassThru(returnVal, _jsExecutor);
            else
                this.WriteObject(returnVal);
        }

        #region Private support methods
        private void WriteVarAndPassThru(object variable, object passThruVar)
        {
            this.SessionState.PSVariable.Set("script_result", variable);
            this.WriteObject(passThruVar);
            return;
        }
        #endregion
    }
}
