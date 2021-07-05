# Script to compile and pack SumatraPDFControl (along with SumatraPDF.exe) into a nupkg to publish in nuget.org
# Requires Visual Studio 2019 installed (can be cummunity version) and nuget.exe in same script dir or in a dir present in PATH enviroment variable

# In order to function it's necessary to set current dir to same location of script 
Set-Location (Split-Path $MyInvocation.MyCommand.Path)
Clear-Host

Write-Output "SumatraPDFControl Nuget Pack and Push script running..."

# Save current dir
$CurrentDir = Get-Location

# Start Developer Shell Powershell in order to compile SumatraPDF 
$vsPath = &(Join-Path ${env:ProgramFiles(x86)} "\Microsoft Visual Studio\Installer\vswhere.exe") -property installationpath
. "$vsPath\Common7\Tools\Launch-VsDevShell.ps1"

# Restore current dir
Set-Location $CurrentDir

# Get ApiKey from secret file (not versioned on GIT)
$NugetOrgApiKey = Get-Content ~/Onedrive/Documentos/nuget/NUGET.ORG.APIKEY.TXT

# HashTable containing the repositories with URL and ApiKey for publishing the components
$Repositories = @{
    "Nuget.Org" = @( "https://api.nuget.org/v3/index.json", $NugetOrgApiKey )
}

Remove-Item -Recurse -Force $CurrentDir\nupkg

mkdir $CurrentDir\nupkg

# Build SumatraPDFControl component
msbuild ..\SumatraPDFControl\SumatraPDFControl.csproj -t:pack -p:Configuration=Release

Copy-Item ..\SumatraPDFControl\bin\Release\*.nupkg $CurrentDir\nupkg

# Includes functions required to package and publish components
# Read comments at the beginning of the file below witch contains important information
. ".\nuget.pack&push.ps1"

NugetPush -Repositories $Repositories -AutoPublish ""

# To be able to read the information if it was run directly by Windows Explorer
#pause