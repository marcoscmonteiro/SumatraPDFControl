# SumatraPDFControl

## Windows Forms Control based on [SumatraPDFReader](https://www.sumatrapdfreader.org/) to view and read Portable Document Files (PDF)

It allows you to construct Windows Forms application capable to view and read Portable Document Files (PDF) with all features present in great [SumatraPDFReader](https://www.sumatrapdfreader.org/) made by [Krzysztof Kowalczyk](https://blog.kowalczyk.info/). 

* Download compiled version from [NuGet.org](https://www.nuget.org/packages/SumatraPDFControl/)

* Go to [SumatraPDFControl site](https://sumatrapdfcontrol.mcmonteiro.com) to view documentation 
and [SumatraPDFControl API](https://sumatrapdfcontrol.mcmonteiro.com/api/SumatraPDF.html)

* [Source code in GitHub](https://github.com/marcoscmonteiro/sumatrapdfcontrol) licenced under [GPLv3](https://github.com/marcoscmonteiro/SumatraPDFControl/blob/master/LICENSE)

* GIT Clone C# Project from [GitHub](https://github.com/marcoscmonteiro/sumatrapdfcontrol) and open SumatraPDFControl.sln with Visual Studio 2019 (works with community version).
 * SumatraPDFControlTest included in soluction shows SumatraPDFControl in action.
 * Bugs, issues or improvements? Report them in [GitHub Project Issues](https://github.com/marcoscmonteiro/SumatraPDFControl/issues)
 
## Supported .NET Framework / Core

* Works with [.NET Framework](https://dotnet.microsoft.com/download/dotnet-framework) version 2.0 or greater

* Works with [.NET Core](https://dotnet.microsoft.com/download/dotnet) version 3.1 or greater

## Dependent NuGet.org packages

SumatraPDFControl uses a specific compiled [SumatraPDF forked code](https://github.com/marcoscmonteiro/sumatrapdf) 
which enables SumatraPDF working in an enhanced plugin mode. Compiled versions are distributed like NuGet packages:

* [x86 - 32 bits](https://www.nuget.org/packages/SumatraPDF.PluginMode.x86) for Windows 32bit archicheture (works also with 64 bits Windows architecture)
* [x64 - 64 bits](https://www.nuget.org/packages/SumatraPDF.PluginMode.x64) for Windows 64bit archicheture
* At least 1 of these packages have to be referenced by Windows Forms project using SumatraPDFControl.
