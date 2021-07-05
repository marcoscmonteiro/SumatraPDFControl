# Script to compile and pack SumatraPDFControl (along with SumatraPDF.exe) into a nupkg to publish in nuget.org
# Requires Visual Studio 2019 installed (can be cummunity version) and nuget.exe in same script dir or in a dir present in PATH enviroment variable

# In order to function it's necessary to set current dir to same location of script 
Set-Location (Split-Path $MyInvocation.MyCommand.Path)
Clear-Host

Write-Output "SumatraPDFControl Nuget Pack and Push script running..."

# Save current dir
$CurrentDir = Get-Location

# Start Developer Shell Powershell in order to compile SumatraPDF or SumatraPDFControl.net5
foreach ($vsVersion in @("Enterprise", "Professional", "Community")) {
    $Vs2019Dir = "C:\Program Files (x86)\Microsoft Visual Studio\2019\$vsVersion\Common7\Tools\Launch-VsDevShell.ps1"
    Write-Output $Vs2019Dir
    if (Test-Path $Vs2019Dir) {
        . $Vs2019Dir
        break
    }
}

# Restore current dir
Set-Location $CurrentDir

$s = Read-Host -prompt "Do you want to recompile SumatraPDF.exe (x86/x64) (y/n)?"

# SumatraPDF base dir (git cloned from https://github.com/marcoscmonteiro/sumatrapdf)
$SumatraPDFBaseDir =  "..\..\sumatrapdf"

if ($s.ToLower() -eq "y") {

    # Compile SumatraPDF (x64 and Win32 plataform)
    msbuild "$SumatraPDFBaseDir\vs2019\SumatraPDF.vcxproj" /p:Configuration=Release /p:Platform=x64
    msbuild "$SumatraPDFBaseDir\vs2019\SumatraPDF.vcxproj" /p:Configuration=Release /p:Platform=Win32

}

# Copy x86/x64 executables to be packed bu nuget
if (-not (Test-Path .\SumatraPDF.x86)) { mkdir .\SumatraPDF.x86 > $null }
if (-not (Test-Path .\SumatraPDF.x64)) { mkdir .\SumatraPDF.x64 > $null }
Copy-Item "$SumatraPDFBaseDir\out\Rel32\SumatraPDF.exe" .\SumatraPDF.x86
Copy-Item "$SumatraPDFBaseDir\out\Rel64\SumatraPDF.exe" .\SumatraPDF.x64

# Includes functions required to package and publish components
# Read comments at the beginning of the file below witch contains important information
. ".\nuget.pack&push.ps1"

# Complete path to reach the solution from the location of the execution of this Script.
$SolutionPath = "..\SumatraPDFControl.sln"    

# Get ApiKey from secret file (not versioned on GIT)
$NugetOrgApiKey = Get-Content ~/Onedrive/Documentos/nuget/NUGET.ORG.APIKEY.TXT

# HashTable containing the repositories with URL and ApiKey for publishing the components
$Repositories = @{
    "Nuget.Org" = @( "https://api.nuget.org/v3/index.json", $NugetOrgApiKey )
}

# Build .net 5 SumatraPDFControl component
msbuild ..\SumatraPDFCOntrol.net5 /t:ReBuild /p:Configuration=Release

# Calls function with component packaging and publishing interface
NugetPackAndPush -SolutionPath $SolutionPath -Repositories $Repositories -ProjectList "*" -AutoPublish "" -AutoGenPackageConfig "n"

# To be able to read the information if it was run directly by Windows Explorer
#pause