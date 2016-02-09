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
$NUnitAdditionalArgs = "--x86 --labels=All"
$NUnitTestAssemblyPaths = @(
  "Src\XmlDocInspections.Plugin.Tests\bin\R92\$Configuration\XmlDocInspections.Plugin.Tests.R92.dll"
  "Src\XmlDocInspections.Plugin.Tests\bin\R100\$Configuration\XmlDocInspections.Plugin.Tests.R100.dll"
  "Src\XmlDocInspections.Plugin.Tests\bin\R20161\$Configuration\XmlDocInspections.Plugin.Tests.R20161.dll"
)
$NUnitFrameworkVersion = "net-4.5"
$TestCoverageFilter = "+[XmlDocInspections*]* -[XmlDocInspections*]ReSharperExtensionsShared.*"
$NuspecPath = "Src\XmlDocInspections.nuspec"
$NugetPackProperties = @(
    "Version=$(CalcNuGetPackageVersion 92);Configuration=$Configuration;DependencyVer=[3.0];BinDirInclude=bin\R92"
    "Version=$(CalcNuGetPackageVersion 100);Configuration=$Configuration;DependencyVer=[4.0];BinDirInclude=bin\R100"
    "Version=$(CalcNuGetPackageVersion 20161);Configuration=$Configuration;DependencyVer=[5.0];BinDirInclude=bin\R20161"
)
$NugetPushServer = "https://www.myget.org/F/ulrichb/api/v2/package"

Clean
PackageRestore
Build
Test
NugetPack

if ($NugetPushKey) {
    NugetPush
}
