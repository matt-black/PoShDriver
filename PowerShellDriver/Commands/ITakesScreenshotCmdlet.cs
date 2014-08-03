using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Management.Automation;

using OpenQA.Selenium;

namespace PowerShellDriver.Commands
{
    [Cmdlet(VerbsCommon.Get, "WebPageScreenshot")]
    public class ITakesScreenshotCmdlet : PSCmdlet
    {
        [Parameter(Mandatory=true,
            ValueFromPipeline=true)]
        public ITakesScreenshot ScreenshotTaker
        {
            get { return _screenshotTaker; }
            set { _screenshotTaker = value; }
        }
        private ITakesScreenshot _screenshotTaker;

        [Parameter(Mandatory=false)]
        public SwitchParameter PassThru
        {
            get { return _passThru; }
            set { _passThru = value; }
        }
        private bool _passThru;

        [Parameter(ParameterSetName="seScreenshot",
            Mandatory=true)]
        public SwitchParameter AsSeScreenshot
        {
            get { return _asSeScreenshot; }
            set { _asSeScreenshot = value; }
        }
        private bool _asSeScreenshot;

        [Parameter(ParameterSetName="string",
            Mandatory=true)]
        public SwitchParameter AsBase64EncodedString
        {
            get { return _asBase64EncodedString; }
            set { _asBase64EncodedString = value; }
        }
        private bool _asBase64EncodedString;

        [Parameter(ParameterSetName="byteArray",
            Mandatory=true)]
        public SwitchParameter AsByteArray
        {
            get { return _asByteArray; }
            set { _asByteArray = value; }
        }
        private bool _asByteArray;

        [Parameter(ParameterSetName="file",
            Mandatory=true,
            Position=0)]
        public SwitchParameter SaveAsFile
        {
            get { return _asFile; }
            set { _asFile = value; }
        }
        private bool _asFile;

        [Parameter(ParameterSetName="file",
            Mandatory=true,
            Position=1)]
        public String FileName
        {
            get { return _fileName; }
            set { _fileName = value; }
        }
        private String _fileName;

        [Parameter(ParameterSetName="file",
            Mandatory=true,
            Position=2)]
        public ImageFormat Format
        {
            get { return _format; }
            set { _format = value; }
        }
        private ImageFormat _format;

        protected override void ProcessRecord()
        {
            Screenshot screenShot = _screenshotTaker.GetScreenshot();
            object returnVal;
            switch (this.ParameterSetName)
            {
                case "string":
                    returnVal = screenShot.AsBase64EncodedString;
                    break;
                case "byteArray":
                    returnVal = screenShot.AsByteArray;
                    break;
                case "file":
                    returnVal = null;
                    screenShot.SaveAsFile(_fileName, _format);
                    break;
                case "seScreehot":
                default:
                    returnVal = screenShot;
                    break;
            }
            //decide what to write to the pipeline based on -PassThru param
            if (_passThru)
            {
                //write the value to a variable in Powershell
                this.SessionState.PSVariable.Set("last_screenshot", returnVal);
            }
            else
            {
                this.WriteObject(returnVal);
                return;
            }
            this.WriteObject(_screenshotTaker);
        }
    }
}
