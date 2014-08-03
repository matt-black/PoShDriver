using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management.Automation;

using OpenQA.Selenium;

namespace PowerShellDriver.Commands
{
    public abstract class ITargetLocatorCmdlet : PSCmdlet
    {
        [Parameter(Mandatory=true,
            ValueFromPipeline=true)]
        public ITargetLocator TargetLocator
        {
            get { return _targetLocator; }
            set { _targetLocator = value; }
        }
        protected ITargetLocator _targetLocator;

        protected bool _has1Param;
        
    }
}
