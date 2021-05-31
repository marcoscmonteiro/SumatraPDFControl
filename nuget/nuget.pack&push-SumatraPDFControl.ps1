# Save current dir
$CurrentDir = Get-Location

# Start Developer Shell Powershell in order to compile SumatraPDF 
. "C:\Program Files (x86)\Microsoft Visual Studio\2019\Enterprise\Common7\Tools\Launch-VsDevShell.ps1" 

# Restore current dir
Set-Location $CurrentDir

# SumatraPDF base dir (git cloned from https://github.com/marcoscmonteiro/sumatrapdf)
$SumatraPDFBaseDir =  "..\..\sumatrapdf"

# Compile SumatraPDF (x64 and Win32 plataform)
msbuild "$SumatraPDFBaseDir\vs2019\SumatraPDF.vcxproj" /p:Configuration=Release /p:Platform=x64
msbuild "$SumatraPDFBaseDir\vs2019\SumatraPDF.vcxproj" /p:Configuration=Release /p:Platform=Win32

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

# HashTable containing the repositories and the URL of their location for publishing the components
$Repositories = @{
    "SumatraPDFControl_repo1" = "https://REPO_URL1/nuget/v3/index.json"
    "SumatraPDFControl_repo2" = "https://REPO_URL2/nuget/v3/index.json"
}

# Calls function with component packaging and publishing interface
NugetPackAndPush -SolutionPath $SolutionPath -Repositories $Repositories

# To be able to read the information if it was run directly by Windows Explorer
pause