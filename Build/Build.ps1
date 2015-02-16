[CmdletBinding()]
Param(
  [Parameter()] [string] $NugetExecutable = ".nuget\nuget.exe",
  [Parameter()] [string] $MSBuildPath = "${env:ProgramFiles(x86)}\MSBuild\12.0\Bin\MSBuild.exe",
  [Parameter()] [string] $Configuration = "Debug",
  [Parameter()] [string] $Version = "0.0.1.0"
)

Set-StrictMode -Version 2.0; $ErrorActionPreference = "Stop"; $ConfirmPreference = "None"

$BuildOutputPath = Join-Path "Build" "Output"
$SolutionFile = "XmlDocInspections.sln"
$AssemblyVersionFilePath = "XmlDocInspections.Plugin\Properties\AssemblyInfo.cs"
$NUnitExecutable = "nunit-console-x86.exe"
$NUnitTestAssemblyPaths = @("XmlDocInspections.Plugin.Tests\bin.R82\$Configuration\XmlDocInspections.Plugin.Tests.dll")
$NUnitFrameworkVersion = "net-4.5"
$NuspecPath = "XmlDocInspections.nuspec"
$NugetPackProperties = @(
    "Configuration=$Configuration;DependencyId=ReSharper;DependencyVer=[8.2,8.3);PackageIdPostfix=;TitlePostfix=;BinDirInclude=bin.R82;TargetDir=ReSharper\v8.2\plugins\\",
    "Configuration=$Configuration;DependencyId=Wave;DependencyVer=[1.0];PackageIdPostfix=.Wave01;TitlePostfix= for ReSharper 9;BinDirInclude=bin.R90;TargetDir=dotFiles\\"
)

function Main() {
    Clean
    UpdateAssemblyVersion
    Build
    Test
    NugetPack
}

function Clean() {
    New-Item $BuildOutputPath -Type Directory -Force | Out-Null
    Remove-Item $BuildOutputPath\* -Recurse -Force
}

function PackageRestore() {
    Write-Host "Restoring packages ..."
    & $NugetExecutable restore $SolutionFile
    if ($LastExitCode -ne 0) { throw "MSBuild failed with exit code $LastExitCode." }
}

function UpdateAssemblyVersion() {
    Write-Host "Updating assembly version in `"$AssemblyVersionFilePath`" ..."
    $assemblyVersionFileContent = [System.IO.File]::ReadAllText($AssemblyVersionFilePath)
    $newContent = ($assemblyVersionFileContent -Replace "(Assembly(?:File)?Version)\s*\(\s*`"[^`"]+`"\s*\)","`$1(`"$Version`")")
    [System.IO.File]::WriteAllText($AssemblyVersionFilePath, $newContent)
}

function Build() {
    Write-Host "Starting build ..."
    & $MSBuildPath $SolutionFile /v:m /t:Build "/p:Configuration=$Configuration"
    if ($LastExitCode -ne 0) { throw "MSBuild failed with exit code $LastExitCode." }
}

function Test() {
    Write-Host "Running tests ..."
    $nunitExePath = Join-Path (GetSolutionPackagePath "NUnit.Runners") "tools\$NUnitExecutable"
    if ($env:APPVEYOR) { $nunitExePath = $NUnitExecutable }

    $testResultsPath = Join-Path $BuildOutputPath "TestResult.xml"

    & $nunitExePath ($NUnitTestAssemblyPaths -Join " ") /nologo /framework=$NUnitFrameworkVersion /domain=Multiple /result=$testResultsPath
    if ($LastExitCode -ne 0) { throw "NUnit failed with exit code $LastExitCode." }
}

function NugetPack() {
    Write-Host "Creating NuGet packages ..."
    $NugetPackProperties | % {
        & $NugetExecutable pack $NuspecPath -Version $Version -Properties $_ -OutputDirectory $BuildOutputPath -NoPackageAnalysis
        if ($LastExitCode -ne 0) { throw "NuGet failed with exit code $LastExitCode." }
    }
}

function GetSolutionPackagePath([string] $packageId) {
    [xml] $xml = Get-Content (Join-Path ".nuget" "packages.config")
    $version = $xml.SelectNodes("/packages/package[@id = '$packageId']/@version") | Select -ExpandProperty Value
    return Join-Path "packages" "$packageId.$version"
}

Main
