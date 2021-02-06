# Vigicrues.Client
[![Nuget](https://img.shields.io/nuget/v/Vigicrues.Client?logo=nuget)](https://www.nuget.org/packages/Vigicrues.Client/)
[![MyGet](https://img.shields.io/myget/mcartoixa/vpre/Vigicrues.Client)](https://www.myget.org/feed/mcartoixa/package/nuget/Vigicrues.Client)
[![Release](https://badgen.net/github/release/mcartoixa/Vigicrues.Client?icon=github)](https://github.com/mcartoixa/Vigicrues.Client/releases)

[![Build status](https://ci.appveyor.com/api/projects/status/dxhegfa9ow48axtn?svg=true)](https://ci.appveyor.com/project/mcartoixa/vigicrues-client)
[![Build](https://github.com/mcartoixa/Vigicrues.Client/workflows/Build/badge.svg)](https://github.com/mcartoixa/Vigicrues.Client/actions)
[![Code coverage](https://codecov.io/gh/mcartoixa/Vigicrues.Client/branch/main/graph/badge.svg)](https://codecov.io/gh/mcartoixa/Vigicrues.Client)

This library provides a .NET wrapper around [the Vigicrues API](https://www.vigicrues.gouv.fr/services/1/), which reports information about flooding hazards in France.

## Usage
Releases for this library [can be found on NuGet](https://www.nuget.org/packages/Vigicrues.Client/). Developement releases [can be found on MyGet](https://www.myget.org/feed/mcartoixa/package/nuget/Vigicrues.Client) (and will most certainly break your project...).

## Development
### Prerequisites
This library requires:
* [.NET Core SDK v 3.1](https://dotnet.microsoft.com/download/visual-studio-sdks).

### Build
The build system for this project is mainly organized around MSBuild script files that can be called from [any CI provider](appveyorl.yml) as well as on your computer (use [`build.bat`](build.bat) or [`build.sh`](build.sh) depending on your platform):
* `build.bat clean`: cleans the project.
* `build.bat compile`: builds the project.
* `build.bat test`: executes the automated tests.
  * code coverage is gathered and results can be viewed in the `tmp\tst` folder.
* `build.bat analyze`: analyzes the project.
  * lines of code are counted using the [CLOC utility](https://github.com/AlDanial/cloc).
* `build.bat package`: packages the project.
  * the library is packaged in a NuGet package file in the `tmp\out\bin` folder.
* `build.bat build`: `compile` + `test`  + `analyze`.
* `build.bat rebuild`: `clean` + `build` (the default).
* `build.bat relaese`: `rebuild` + `package`.
