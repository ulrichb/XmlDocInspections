[CmdletBinding()]
Param(
  [Parameter()] [string] $NugetExecutable = ".nuget\nuget.exe",
  [Parameter()] [string] $MSBuildPath = "${env:ProgramFiles(x86)}\MSBuild\12.0\Bin\MSBuild.exe",
  [Parameter()] [string] $Configuration = "Debug",
  [Parameter()] [string] $Version = "0.0.1.0",
  [Parameter()] [string] $NugetPushKey
)

Set-StrictMode -Version 2.0; $ErrorActionPreference = "Stop"; $ConfirmPreference = "None"

$BuildOutputPath = Join-Path "Build" "Output"
$SolutionFile = "XmlDocInspections.sln"
$AssemblyVersionFilePath = "Src\XmlDocInspections.Plugin\Properties\AssemblyInfo.cs"
$NUnitExecutable = "nunit-console-x86.exe"
$NUnitTestAssemblyPaths = @("Src\XmlDocInspections.Plugin.Tests\bin.R82\$Configuration\XmlDocInspections.Plugin.Tests.dll")
$NUnitFrameworkVersion = "net-4.5"
$NuspecPath = "Src\XmlDocInspections.nuspec"
$NugetPackProperties = @(
    "Configuration=$Configuration;DependencyId=ReSharper;DependencyVer=[8.2,8.3);PackageIdPostfix=;TitlePostfix=;BinDirInclude=bin.R82;TargetDir=ReSharper\v8.2\plugins",
    "Configuration=$Configuration;DependencyId=Wave;DependencyVer=[1.0];PackageIdPostfix=.Wave01;TitlePostfix= for ReSharper 9;BinDirInclude=bin.R90;TargetDir=dotFiles"
)
$NugetPushServer = "https://www.myget.org/F/ulrichb/api/v2/package"

function Main() {
    Clean
    PackageRestore
    UpdateAssemblyVersion
    Build
    Test
    NugetPack

    if ($NugetPushKey) {
        NugetPush
    }
}

function Clean() {
    New-Item $BuildOutputPath -Type Directory -Force | Out-Null
    Remove-Item $BuildOutputPath\* -Recurse -Force
}

function PackageRestore() {
    Write-Host "Restoring packages ..."
    Exec { & $NugetExecutable restore $SolutionFile }
}

function UpdateAssemblyVersion() {
    Write-Host "Updating assembly version in `"$AssemblyVersionFilePath`" ..."
    $assemblyVersionFileContent = [System.IO.File]::ReadAllText($AssemblyVersionFilePath)
    $newContent = ($assemblyVersionFileContent -Replace "(Assembly(?:File)?Version)\s*\(\s*`"[^`"]+`"\s*\)","`$1(`"$Version`")")
    [System.IO.File]::WriteAllText($AssemblyVersionFilePath, $newContent)
}

function Build() {
    Write-Host "Starting build ..."
    Exec { & $MSBuildPath $SolutionFile /v:m /t:Build "/p:Configuration=$Configuration" }
}

function Test() {
    Write-Host "Running tests ..."
    $nunitExePath = Join-Path (GetSolutionPackagePath "NUnit.Runners") "tools\$NUnitExecutable"
    if ($env:APPVEYOR) { $nunitExePath = $NUnitExecutable }

    $testResultsPath = Join-Path $BuildOutputPath "TestResult.xml"

    Exec { & $nunitExePath ($NUnitTestAssemblyPaths -Join " ") /nologo /framework=$NUnitFrameworkVersion /domain=Multiple /result=$testResultsPath }
}

function NugetPack() {
    Write-Host "Creating NuGet packages ..."
    $NugetPackProperties | % {
        Exec { & $NugetExecutable pack $NuspecPath -Version $Version -Properties $_ -OutputDirectory $BuildOutputPath -NoPackageAnalysis }
    }
}

function NugetPush() {
    Write-Host "Pushing NuGet packages ..."
    gci (Join-Path $BuildOutputPath "*.nupkg") | % {
        Exec { & $NugetExecutable push $_ $NugetPushKey -Source $NugetPushServer }
    }
}

function GetSolutionPackagePath([string] $packageId) {
    [xml] $xml = Get-Content (Join-Path ".nuget" "packages.config")
    $version = $xml.SelectNodes("/packages/package[@id = '$packageId']/@version") | Select -ExpandProperty Value
    return Join-Path "packages" "$packageId.$version"
}

function Exec([scriptblock] $cmd) {
    & $cmd
    if ($LastExitCode -ne 0) {
        throw "The following call failed with exit code $LastExitCode. '$cmd'"
    }
}

Main
