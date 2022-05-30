# Genshin Impact Launcher [![GitHub release (latest by date)](https://img.shields.io/github/v/release/FoxPride/GenshinLauncher)](https://github.com/FoxPride/GenshinLauncher/releases/latest)

A Genshin Impact launcher with account management made using C# and WPF. If you enjoyed this project, consider [contributing](https://github.com/FoxPride/GenshinLauncher#contributing) or 🌟 starring the repository. Thank you~

> **_NOTE:_**  This version is an rewrite of the [GenshinLauncher](https://github.com/sabihoshi/GenshinLauncher)

## Why was this version created?
The main focus here has been to update the tools to net6.0 and add account management system.

- Visual part was completly rewritten with [WPF UI](https://github.com/lepoco/wpfui)
- MVVM logic updated using [.NET Community Toolkit](https://github.com/CommunityToolkit/dotnet)

## **[Download latest version](https://github.com/FoxPride/GenshinLauncher/releases/latest)** ![GitHub all releases](https://img.shields.io/github/downloads/FoxPride/GenshinLauncher/total?style=social)

![GenshinLauncher](https://user-images.githubusercontent.com/30706733/170900441-ec549fa3-3dd5-4eaa-9bc9-a5466b602828.png)

## How to use

1. [Download the program](https://github.com/FoxPride/GenshinLauncher/releases/latest) and then run, no need for installation.


## Features
* Change the set resolution of the game.
* Set the quality preset of the game.
* Allow for borderless fullscreen gameplay.
* Change the location of the .exe to a custom one.
* Manage your genshin impact accounts.
* Relaunch genshin impact with selected account.

## About

### Can this get me banned?
No it will not. This only uses [Unity command line arguments](https://docs.unity3d.com/Manual/CommandLineArguments.html) that are built in itself. It essentially just runs the game with extra flags such as `-popupwindow`. [Here is miHoYo's response](https://genshin.mihoyo.com/en/news/detail/5763) to using 3rd party tools.

# Contributing
When contributing to this repository, please first discuss the change you wish to make via issue, email (c1ee3517@opayq.com), or any other method with me or the maintainers of this repository before making a change.

This project has a [Code of Conduct](CONTRIBUTING.md), please follow it in all your interactions with the project.

## Pull Request Process

1. Do not include the build itself where the project is cleaned using `dotnet clean`.
2. Update the README.md with details of changes to the project, new features, and others that are applicable.
3. Increase the version number of the project and the README.md to the new version that this
   Pull Request would represent. The versioning scheme we use is [SemVer](http://semver.org/).
4. You may merge the Pull Request in once you have the the approval of the maintainers.

## Build
If you just want to run the program, there are precompiled releases that can be found in [here](https://github.com/FoxPride/GenshinLauncher/releases).
### Requirements
* [Git](https://git-scm.com) for cloning the project
* [.NET 6.0](https://dotnet.microsoft.com/download/dotnet/6.0) SDK

#### Publish a single binary for Windows
```bat
git clone https://github.com/FoxPride/GenshinLauncher.git
cd GenshinLauncher\GenshinLauncher

dotnet publish -r win-x64 --framework net6.0 -o bin\publish --no-self-contained -p:PublishSingleFile=true
```
> For other runtimes, visit the [RID Catalog](https://docs.microsoft.com/en-us/dotnet/core/rid-catalog) and change the runtime value.

#### Build the project (not necessary if you published)
```bat
git clone https://github.com/FoxPride/GenshinLauncher.git
cd GenshinLauncher

dotnet build
```

#### Publish the project using defaults
```bat
git clone https://github.com/FoxPride/GenshinLauncher.git
cd GenshinLauncher

dotnet publish
```

# License
* This project is under the [MIT](LICENSE.md) license.
* All rights reserved by © Cognosphere Pte. Ltd. This project is not affiliated, nor endorsed by HoYoverse. Genshin Impact™ and other properties belong to their respective owners.
* This project uses third-party libraries or other resources that may be
distributed under [different licenses](/THIRD-PARTY-NOTICES.md).