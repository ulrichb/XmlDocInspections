[CmdletBinding()]
Param(
  [Parameter()] [string] $NugetExecutable = "Shared\.nuget\nuget.exe",
  [Parameter()] [string] $Configuration = "Debug",
  [Parameter()] [string] $Version = "0.0.1.0",
  [Parameter()] [string] $NugetPushKey
)

Set-StrictMode -Version 2.0; $ErrorActionPreference = "Stop"; $ConfirmPreference = "None"

$BuildOutputPath = "Build\Output"
$SolutionFile = "XmlDocInspections.sln"
$AssemblyVersionFilePath = "Src\XmlDocInspections.Plugin\Properties\AssemblyInfo.cs"
$MSBuildPath = "${env:ProgramFiles(x86)}\MSBuild\12.0\Bin\MSBuild.exe"
$NUnitExecutable = "nunit-console-x86.exe"
$NUnitTestAssemblyPaths = @("Src\XmlDocInspections.Plugin.Tests\bin.R82\$Configuration\XmlDocInspections.Plugin.Tests.dll")
$NUnitFrameworkVersion = "net-4.5"
$NuspecPath = "Src\XmlDocInspections.nuspec"
$NugetPackProperties = @(
    "Configuration=$Configuration;DependencyId=ReSharper;DependencyVer=[8.2,8.3);PackageIdPostfix=;TitlePostfix=;BinDirInclude=bin.R82;TargetDir=ReSharper\v8.2\plugins",
    "Configuration=$Configuration;DependencyId=Wave;DependencyVer=[1.0];PackageIdPostfix=.Wave01;TitlePostfix= for ReSharper 9;BinDirInclude=bin.R90;TargetDir=dotFiles"
)
$NugetPushServer = "https://www.myget.org/F/ulrichb/api/v2/package"

. Shared\Build\BuildFunctions

Clean
PackageRestore
UpdateAssemblyVersion
Build
Test
NugetPack

if ($NugetPushKey) {
    NugetPush
}
