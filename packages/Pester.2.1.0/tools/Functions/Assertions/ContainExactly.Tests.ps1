$here = Split-Path -Parent $MyInvocation.MyCommand.Path
. "$here\Test-Assertion.ps1"
. "$here\ContainExactly.ps1"

Describe "PesterContainExactly" {

    Context "when testing file contents" {
        Setup -File "test.txt" "this is line 1`nPester is awesome"
        It "returns true if the file contains the specified content exactly" {
            Test-PositiveAssertion (PesterContainExactly "$TestDrive\test.txt" "Pester")
        }

        It "returns false if the file does not contain the specified content exactly" {
            Test-NegativeAssertion (PesterContainExactly "$TestDrive\test.txt" "pESTER")
        }
    }
}

