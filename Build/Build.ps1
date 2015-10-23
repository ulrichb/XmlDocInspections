[CmdletBinding()]
Param(
  [Parameter()] [string] $NugetExecutable = "Shared\.nuget\nuget.exe",
  [Parameter()] [string] $Configuration = "Debug",
  [Parameter()] [string] $Version = "0.0.1.0-dev",
  [Parameter()] [string] $NugetPushKey
)

Set-StrictMode -Version 2.0; $ErrorActionPreference = "Stop"; $ConfirmPreference = "None"

. Shared\Build\BuildFunctions

$BuildOutputPath = "Build\Output"
$SolutionFilePath = "XmlDocInspections.sln"
$AssemblyVersionFilePath = "Src\XmlDocInspections.Plugin\Properties\AssemblyInfo.cs"
$MSBuildPath = "${env:ProgramFiles(x86)}\MSBuild\14.0\Bin\MSBuild.exe"
$NUnitExecutable = "nunit-console-x86.exe"
$NUnitTestAssemblyPaths = @(
  "Src\XmlDocInspections.Plugin.Tests\bin\R91\$Configuration\XmlDocInspections.Plugin.Tests.R91.dll"
  "Src\XmlDocInspections.Plugin.Tests\bin\R92\$Configuration\XmlDocInspections.Plugin.Tests.R92.dll"
)
$NUnitFrameworkVersion = "net-4.5"
$TestCoverageFilter = "+[XmlDocInspections*]* -[XmlDocInspections*]ReSharperExtensionsShared.*"
$NuspecPath = "Src\XmlDocInspections.nuspec"
$NugetPackProperties = @(
    "Version=$(CalcNuGetPackageVersion 91);Configuration=$Configuration;DependencyVer=[2.0];BinDirInclude=bin\R91"
    "Version=$(CalcNuGetPackageVersion 92);Configuration=$Configuration;DependencyVer=[3.0];BinDirInclude=bin\R92"
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
