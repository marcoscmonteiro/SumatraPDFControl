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