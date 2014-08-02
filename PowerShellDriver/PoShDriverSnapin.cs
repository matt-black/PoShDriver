using System;
using System.Collections.Generic;
using System.Text;
using System.Management.Automation;
using System.ComponentModel;

namespace PowerShellDriver
{
    /// <summary>
    /// Controls the Snapin parameters for this project. 
    /// Allows it to be installed as a snapin.
    /// </summary>
    [RunInstaller(true)]
    public class PowerShellDriverSnapIn : PSSnapIn
    {
        public override string Description
        {
            get { return "A Selenium WebDriver framework for Windows Powershell"; }
        }

        public override string Name
        {
            get { return "PoShDriver"; }
        }

        public override string Vendor
        {
            get { return "Matt Black"; }
        }
    }
}
