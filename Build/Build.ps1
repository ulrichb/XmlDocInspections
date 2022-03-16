[CmdletBinding()]
Param(
  [Parameter()] [string] $NugetExecutable = "Shared\.nuget\nuget.exe",
  [Parameter()] [string] $Configuration = "Debug",
  [Parameter()] [string] $Version = "0.0.0.1",
  [Parameter()] [string] $BranchName,
  [Parameter()] [string] $CoverageBadgeUploadToken,
  [Parameter()] [string] $NugetPushKey
)

Set-StrictMode -Version 2.0; $ErrorActionPreference = "Stop"; $ConfirmPreference = "None"
trap { $error[0] | Format-List -Force; $host.SetShouldExit(1) }

. Shared\Build\BuildFunctions

$BuildOutputPath = "Build\Output"
$SolutionFilePath = "XmlDocInspections.sln"
$NUnitAdditionalArgs = "--x86 --labels=All --agents=1"
$NUnitTestAssemblyPaths = @(
    "Src\XmlDocInspections.Plugin.Tests\bin\RD20213\$Configuration\XmlDocInspections.Plugin.Tests.RD20213.dll"
    "Src\XmlDocInspections.Plugin.Tests\bin\RS20213\$Configuration\XmlDocInspections.Plugin.Tests.RS20213.dll"
)
$NUnitFrameworkVersion = "net-4.5"
$TestCoverageFilter = "+[XmlDocInspections*]* -[XmlDocInspections*]ReSharperExtensionsShared.*"
$NuspecPath = "Src\XmlDocInspections.Plugin\XmlDocInspections.nuspec"
$NugetPackProperties = @(
    "Version=$(CalcNuGetPackageVersion 20213);Configuration=$Configuration;DependencyVer=[213.0];BinDirInclude=bin\RS20213"
)
$RiderPluginProject = "Src\RiderPlugin"
$NugetPushServer = "https://www.myget.org/F/ulrichb/api/v2/package"

Clean
PackageRestore
Build
NugetPack
BuildRiderPlugin
Test

if ($NugetPushKey) {
    NugetPush
}
