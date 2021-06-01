<# 
.Description

This Script must be executed by PowerShell and allows the packaging (nuget pack) and respective publication (nuget push) of the Nuget packages for the components of a configured solution
through the -SolutionPath and -Repositories parameters of the NugetPackAndPush function. At the end of this comment you can see examples of how to fill these parameters

The components of the solution can be selected through the interface in order to be packaged in the .\ Nupkg folder.
The interface will request confirmation for publication of the components generated for the NuGet repositories defined in the -Repositories parameter

Log files with the result of packaging and publishing operations are stored in the .\Log folder.

This Script must be imported from another script as in the example below:

  . "c:\COMPLETE\PATH\WHERE\IS\nuget.pack&push.psi"


Important notes: 

1) Before you can run this script, you need to run the following command in PowerShell:
  
  Set-ExecutionPolicy -ExecutionPolicy Bypass -Scope CurrentUser

2) To understand how to create NUGET packages see documentation at https://docs.microsoft.com/en-us/nuget/create-packages/creating-a-package

In short:

a) Download the nuget.exe application from https://www.nuget.org/downloads and place it in a folder accessible by the entire system (included in PATH) or in the folder where this script will be executed
b) You must create a .nuspec file based on a pre-existing project using the
   "nuget spec" command. This command must be executed from a console window and in a directory where the project is present (extension .csproj or .vbproj)
   class library (see manual above for details)
c) Edit the generated .nuspec file as instructed in the manual above

3) To upload the package to the repositories mapped in the steps above (done automatically by this script)

In the last part of the NugetPackAndPush function, you are asked whether packaged packages should be published (push) in the repository configured in the $ Repositories variable.
If the package has already been uploaded with the same version number, an error similar to the one shown below will occur:

  Package '... .\nuget .\nupkg\SumatraPDFControl.1.0.0.nupkg' already exists at feed 'https://...'

Then it will be necessary to prepare a new version of the component. So, the .nuspec file project must be <version> tag changed to a version not used before.
Once the changes were saved, a new attempt should be made to execute this script.

4) The following variables must be prepared before executing the NugetPackAndPush function that makes
   packaging and publishing the components: 

       . $SolutionPath
       . $Repositories

   
  Below is the documentation and an example of how they should be filled out::

    # Complete path to reach the solution from the location of the execution of this Script.
    $SolutionPath = "..\SumatraPDFControl.sln"    

    # HashTable containing the repositories with URL and APIKEY for publishing the components
    $Repositories = @{
      "Component_repo1" = @("https://COMPONENT_REPO_URL1/nuget/.../index.json", "REPO_URL1_APIKEY"),
      "Component_repo2" = @("https://COMPONENT_REPO_URL2/nuget/.../index.json", "REPO_URL2_APIKEY")
    }

    # Calls function with component packaging and publishing interface
    NugetPackAndPush -SolutionPath $SolutionPath -Repositories $Repositories

#> 

<# Generate Package.config file from the project file so that the Nuget utility automatically adds it to the generated package
the external references to the project #>
function GeneratePackageConfig {
    param(
      [String]$ProjectDir,
      [String]$ProjectNameWithExtension,
      [String]$PackageConfigFile
    )

    # Search first for C # projects and then the existence of VB.NET projects. If neither exists, it exits the function
    $ProjectPath = $ProjectDir + "\" + $ProjectNameWithExtension
    if (-not (Test-Path -Path $ProjectPath)) { 
      write-error "Project file $ProjectNameWithExtension not found" -Category CloseError  
      return
    }     
    
    [xml]$xmldata = Get-Content $ProjectPath

    # Retrieve Framework version
    $tf = $xmldata.Project.PropertyGroup | ForEach-Object {$_.TargetFrameworkVersion} | select-object 
    [String[]]$tfarr = $tf.Substring(1).Split(".")
    $tfver = "net" + $tfarr[0].ToString() + $tfarr[1].ToString()
    
    # Generates package.config file excluding the previous one if it exists   
    $xmlWriter = New-Object System.Xml.XmlTextWriter($PackageConfigFile, $Null)
    $xmlWriter.Formatting = 'Indented'
    $xmlWriter.Indentation = 1
    $XmlWriter.IndentChar = "`t"
    $xmlWriter.WriteStartDocument()
    $xmlWriter.WriteStartElement('packages')

    # Adds all references automatically
    $xmldata.Project.ItemGroup | ForEach-Object{$_.PackageReference} | select-object | ForEach-Object { 
        $xmlWriter.WriteStartElement('package') 
        $xmlWriter.WriteAttributeString('id', $_.Include)
        $xmlWriter.WriteAttributeString('version', $_.Version)
        $xmlWriter.WriteAttributeString('targetFramework', $tfver)
        $xmlWriter.WriteEndElement()
    } 

    $xmlWriter.WriteEndElement()
    $xmlWriter.WriteEndDocument()
    $xmlWriter.Flush()
    $xmlWriter.Close()
}

# Function that automates the generation process (pack) of components in nupkg format in the folder .\Nupkg
function NugetPack {

  param(
    [Parameter(Mandatory=$True)]
    [Int[]]$Itens
  )

  # Cleaning the packaging and log directories to avoid confusion with old packaging
  Remove-Item .\nupkg\*.*
  Remove-Item .\log\*.*

  # Restores external packages referenced by the solution
  Write-Output "Performing nuget restore of referenced external packages"
  nuget restore "$SolutionDir\$SolutionName" -PackagesDirectory "$SolutionDir\packages" > "log\NugetRestore.log"
  Write-Output "Restore finished. More details in log\NugetRestore.log"

  # Packages each selected component
  foreach ($item in $Itens) {     
    $ExcludePackageConfig = $False
    $ProjectDir = $SolutionDir + "\" + $Components[$item][1]
    $ProjectName = $Components[$item][0]
    $ProjectNameWithExtension = $Components[$item][2]
    $PackageConfigFile = $ProjectDir + "\" + "packages.config"    
    
    # If the packages.config file does not exist, it calls the generation routine
    if (-not (Test-Path $PackageConfigFile)) {
        GeneratePackageConfig -ProjectDir $ProjectDir -ProjectNameWithExtension $ProjectNameWithExtension -PackageConfigFile $PackageConfigFile

        # If the packages.config file has not been generated, it stops the entire script 
        if (-not (Test-Path $PackageConfigFile)) {
          Write-Output "Error generating file $PackageConfigFile for project $ProjectName em $ProjectDir"
          exit
        }
        $ExcludePackageConfig = $True
    }  

    Write-Output ""
    Write-Output "Packaging $ProjectName..."
    nuget pack "$ProjectDir" -IncludeReferencedProjects -OutputDirectory .\nupkg -Build -Prop Configuration=Release -SolutionDirectory "$SolutionDir" > "log\$ProjectName.log"
    Write-Output "Packaging finished. More details in log\$ProjectName.log"

    # Only exclude the packages.config file if it was generated during packaging
    if ($ExcludePackageConfig -eq $True) { Remove-Item $PackageConfigFile }
  }

} 

# Function to perform the publication of all components previously packaged in the directory. \ Nupkg for the configured repositories
# in the $Repositories variable
function NugetPush {
  param(    
    [HashTable]$Repositories,
    [String]$AutoPublish
  )

    Write-Output ""
    Write-Output "List of repositories for publication:"
    Write-Output ""
    $Repositories.GetEnumerator() | ForEach-Object -process { $_.Key + " - " + $_.Value }
    Write-Output ""

    if ($AutoPublish -eq "")
    {
      $s = Read-Host -prompt "Do you want to perform the publication (nuget push) of the components packaged above in the repositories listed (y/n)?"
    } else 
    {
      $s = $AutoPublish.ToLower()
    }

    if ($s -eq "y") { 
      Write-Output "Publishing projects to repositories" | Tee-Object log\NugetPush.log
      foreach ($repo in $Repositories.Keys) {
        $RepoURL = $Repositories[$repo][0]
        $ApiKey = $Repositories[$repo][1]
        Write-Output "Publishing projects in $RepoURL (note: if version already exists an error will be logged)" | Tee-Object log\NugetPush.log -Append
        nuget push -Source "$RepoURL" -ApiKey $ApiKey -SkipDuplicate nupkg\*.nupkg >> log\NugetPush.log
      }  
      Write-Output "Published" | Tee-Object log\NugetPush.log -Append
      Get-Content .\log\NugetPush.log 
    }
}

 
# Main function for displaying packaging and publishing interface
# It should only be called after filling in the $ SolutionPath and $ Repositories variables
# as indicated in the initial comments
function NugetPackAndPush() {
  param(    
    [String]$SolutionPath, # Complete path solution (.sln file)
    [HashTable]$Repositories, # Hash table (see example in comments above)
    [String]$ProjectList, # Numeric list (space separated) with projects to be packed and published or "*" to all projetcs
    [String]$AutoPublish # String char (y/n) if script will prompt to publish projects
  )

  Clear-Host

  # Search for nuget.exe
  if (test-path .\nuget.exe) {
    # nuget.exe was found in the local folder and will be used
    set-alias nuget .\nuget 
  } else {
      try {   
          nuget > $null
          # nuget.exe was found in the folder present in the PATH environment variable and will be used
      }
      catch [System.SystemException] {
        write-error (
          "The executable nuget.exe could not be found in the current folder or in PATH environment variable " + 
          "Download nuget.exe using: https://dist.nuget.org/win-x86-commandline/latest/nuget.exe " +
          "Copy it to the script execution folder or in a globally accessible folder (present in PATH environment variable)")

        exit
      }      
  }

  if (-not (Test-Path $SolutionPath)) { 
    Write-Error -Message "Non-existent solution file: $SolutionPath"
    exit
  }

  # Extract name and directory of the solution in separate variables
  $SolutionName = $SolutionPath | Split-Path -Leaf
  $SolutionDir = (Resolve-Path $SolutionPath) | Split-Path 
  
  # Creates HashTable containing the name of each relevant project and respective folders (relative to the $SolutionDir folder) where they are present
  $Components = @{}
  # Scans solution file for projects (with .nuspec file present) to complete HashTable $Components
  [Int]$Key = 1
  $SolutionProjectPattern = "(?x)^ Project \( "" \{ [0-9A-F]{8}-[0-9A-F]{4}-[0-9A-F]{4}-[0-9A-F]{4}-[0-9A-F]{12} \} "" \)\s* = \s*"" (?<name> [^""]* ) "" , \s+"" (?<path> [^""]* ) "" , \s+"
  Get-Content -Path "$SolutionDir\$SolutionName" |
      ForEach-Object {        
          if ($_ -match $SolutionProjectPattern) {
              $projnuspecfile = $SolutionDir + "\" + ($Matches['path'] | Split-Path) + "\" + $Matches['name'] + ".nuspec"
              if (Test-Path  $projnuspecfile) {
                  $Components.Add($Key, @($Matches['name'], ($Matches['path'] | Split-Path), ($Matches['path'] | Split-Path -Leaf)))
                  $Key = $Key+1
              }
          }
      }
  
  # Ask which components will be packaged:
  Write-Output "Solution: $SolutionDir\$SolutionName"
  Write-Output ""
  Write-Output "Projects available to pack:"
  Write-Output ""
  $Components.GetEnumerator() | Sort-Object -Property name | ForEach-Object -process { $_.Key.ToString().PadLeft(3) + " - " + $_.Value[0].PadRight(25) + " (" + $SolutionDir + "\" + $_.Value[1] + ")" }

  Write-Output ""
  if ($ProjectList -eq "") 
  {
    $strItens = Read-Host -prompt "Enter project numbers separated by space (* = all)"
  } else 
  {
    $strItens = $ProjectList 
  }
  
  if ($strItens -eq "") { exit }

  if ($strItens -eq "*") 
  { 
      [Int[]] $Itens = $Components.Keys 
  } else
  {
      [Int[]] $Itens = $strItens.split(" ")
  }

  # Create nupkg directory and log for packaging output 
  if (-not (Test-Path .\nupkg)) { mkdir .\nupkg > $null }
  if (-not (Test-Path .\log)) { mkdir .\log > $null }

  Write-Output ""
  Write-Output "Cleaning the obj folder of all projects in order to eliminate cache of old references to nuget packages that can cause an error during the projects build"
  foreach ($id in $Components.Keys) {
    $ProjectDirObj = $SolutionDir + "\" + $Components[$id][1] + "\obj"
    if (Test-Path "$ProjectDirObj") { 
      Write-Output "Excluindo $ProjectDirObj"
      Remove-Item "$ProjectDirObj" -Recurse 
    }
  }
  Write-Output ""

  # Generate (pack) selected projects
  NugetPack($Itens)

  # Sending packages generated above to Nuget repositories
  Write-Output ""
  Write-Output "List of packaged components (nuget pack):"
  Get-ChildItem .\nupkg
  $TargetNumber = $Itens.Count
  $RealNumber = (Get-ChildItem .\nupkg | measure-object -Line).Lines

  # Summary of quantity of components packaged and ordered for packaging
  Write-Output ""
  Write-Output "# Target packaged projects: $TargetNumber"
  Write-Output "# Real packaged projects  : $RealNumber"

  # If the packaging phase has generated fewer packages than previously requested, issue a warning
  if (-not ($RealNumber -eq $TargetNumber)) {
      Write-Output ""
      Write-Warning "The total of packaged projects does not correspond to the total of projects selected for packaging"
      Write-Warning "Review the generation log files in order to detect any possible errors during packaging"
  }
  
  # Proceed with the display of the publishing interface of the packaged projects
  NugetPush -Repositories $Repositories -AutoPublish $AutoPublish
}