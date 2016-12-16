[CmdletBinding()]
Param(
  [Parameter()] [string] $NugetExecutable = "Shared\.nuget\nuget.exe",
  [Parameter()] [string] $Configuration = "Debug",
  [Parameter()] [string] $Version = "0.0.1.0-dev",
  [Parameter()] [string] $BranchName,
  [Parameter()] [string] $CoverageBadgeUploadToken,
  [Parameter()] [string] $NugetPushKey
)

Set-StrictMode -Version 2.0; $ErrorActionPreference = "Stop"; $ConfirmPreference = "None"

. Shared\Build\BuildFunctions

$BuildOutputPath = "Build\Output"
$SolutionFilePath = "XmlDocInspections.sln"
$AssemblyVersionFilePath = "Src\XmlDocInspections.Plugin\Properties\AssemblyInfo.cs"
$MSBuildPath = "${env:ProgramFiles(x86)}\MSBuild\14.0\Bin\MSBuild.exe"
$NUnitAdditionalArgs = "--x86 --labels=All --agents=1"
$NUnitTestAssemblyPaths = @(
    "Src\XmlDocInspections.Plugin.Tests\bin\R20161\$Configuration\XmlDocInspections.Plugin.Tests.R20161.dll"
    "Src\XmlDocInspections.Plugin.Tests\bin\R20162\$Configuration\XmlDocInspections.Plugin.Tests.R20162.dll"
    "Src\XmlDocInspections.Plugin.Tests\bin\R20163\$Configuration\XmlDocInspections.Plugin.Tests.R20163.dll"
)
$NUnitFrameworkVersion = "net-4.5"
$TestCoverageFilter = "+[XmlDocInspections*]* -[XmlDocInspections*]ReSharperExtensionsShared.*"
$NuspecPath = "Src\XmlDocInspections.nuspec"
$NugetPackProperties = @(
    "Version=$(CalcNuGetPackageVersion 20161);Configuration=$Configuration;DependencyVer=[5.0];BinDirInclude=bin\R20161"
    "Version=$(CalcNuGetPackageVersion 20162);Configuration=$Configuration;DependencyVer=[6.0];BinDirInclude=bin\R20162"
    "Version=$(CalcNuGetPackageVersion 20163);Configuration=$Configuration;DependencyVer=[7.0];BinDirInclude=bin\R20163"
)
$NugetPushServer = "https://www.myget.org/F/ulrichb/api/v2/package"

Clean
PackageRestore
Build
NugetPack
Test

if ($NugetPushKey) {
    NugetPush
}
