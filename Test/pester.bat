@echo off
SET DIR=%~dp0%

CALL :resolve "%DIR%\.." PROJ_ROOT

REM get the args and look for the help arg
SET ARGS=%*
if NOT '%1'=='' SET ARGS=%ARGS:"=\"%
if '%1'=='/?' goto usage
if '%1'=='-?' goto usage
if '%1'=='?' goto usage
if '%1'=='/help' goto usage
if '%1'=='help' goto usage

:starttests
@%SystemRoot%\syswow64\WindowsPowerShell\v1.0\powershell.exe -NonInteractive -NoProfile -ExecutionPolicy Bypass -Command ^
 "& Import-Module '%PROJ_ROOT%\packages\Pester.3.0.0-beta\tools\Pester.psm1'; & Add-Type -Path '%PROJ_ROOT%\packages\Selenium.WebDriver.2.42.0\lib\net40\WebDriver.dll' ; & Add-PSSnapin PoShDriver; & { Set-StrictMode -Version Latest; Invoke-Pester -Path %DIR% -OutputXml Test.xml -EnableExit %ARGS%}"

goto finish

:usage
if NOT '%2'=='' goto help

echo To run pester for tests, just call pester or runtests with no arguments
echo.
echo Example: pester
echo.
echo For Detailed help information, call pester help with a help topic. See
echo help topic about_Pester for a list of all topics at the end
echo.
echo Example: pester help about_Pester
echo.
goto finish

:help
@PowerShell -NonInteractive -NoProfile -ExecutionPolicy Bypass -Command ^
  "& Import-Module '%DIR%..\Pester.psm1'; & { Get-Help %2}"

:finish
exit %errorlevel%

:resolve
SET %2=%~f1
GOTO :starttests