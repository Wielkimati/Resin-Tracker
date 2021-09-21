# Resin Tracker ![C#](https://img.shields.io/badge/c%23-%23239120.svg?style=for-the-badge&logo=c-sharp&logoColor=white) ![.Net](https://img.shields.io/badge/.NET-5C2D91?style=for-the-badge&logo=.net&logoColor=white)
Lightweight Windows app that allows you to easily check how many resin you have without the need to open Genshin Impact.

It's basically your personal counter that you need to set up manually. As such, it doesn't get any information on your current in-game resin count, and by extension, you cannot get banned by Mihoyo in any way for using it, if you were worried about that.

Written in .Net 5.0.

## Table of Contents
* [General Information](#general-information)
* [Features](#features)
* [Setup](#setup)
* [Requirements](#requirements)
* [Setup](#setup)
* [Usage](#usage)
* [Known Issues](#known-issues)
* [Contact](#contact)
* [License](#license)
* [Support](#support)


## General Information
Would you like to know how many resin do you have in Genshin Impact on given moment, without the need of opening the game, waiting for it to load only to find out you capped your resin 5 hours ago or need to wait 20 minutes more to craft another condensed resin?

I've created Resin Tracker to address this issue and also mainly for personal use, as I can't be really bothered to use mobile apps to keep track of this and keep up with all of the inconveniences of using mobile apps for manually tracking data like in-game stamina. I much prefer to use desktop apps for this.

## Features
- Allows you to quickly check how many resin you have.
- Conveniently shows you all information when you hover over Resin Tracker's icon in your system tray.
- You can quickly reset your resin count back to 0, or set a custom value.
- Resin Tracker will also automatically calculate how many resin you gained while the program has been turned off (e.g. It can calculate how many resin you gained while you were sleeping, at work, etc.).

## Requirements
- Windows 7 - 10
- [.Net 5.0](https://dotnet.microsoft.com/download/dotnet/5.0) - theoretically you only need a .Net 5.0 runtime, but I recommend downloading whole SDK.

Resin Tracker has been tested only on Windows 10 by me, but should theoretically work on anything above Windows 7. I've seen that there are some weird graphical bugs on Windows 11, but I don't plan on doing anything about that untill that system comes out officially and I'll get it myself.

If you run into any problems on any platforms, you can create an issue - I'll be sure to look into that.

## Setup
- Grab the newest release from [releases page](https://github.com/Wielkimati/Resin-Tracker/releases).
- Extract the files anywhere you'd like on your computer and run the Resin Tracker.exe.
- Optional: Create a shortcut that you can add to autostart, so the tracker runs itself everytime you start your system. 

You can easily access autostart folder by pressing Win + R and typing "shell:startup".

## Usage

![Example screenshot](https://github.com/Wielkimati/Resin-Tracker/blob/main/Screenshot.png?raw=true)

- Clicking on resin icon allows you to reset your resin back to 0.
- Clicking on resin counter text allows you to input your custom resin count (max. 3 numbers).
- Hovering over Resin Tracker icon on the system tray will show you a baloon with all necessary info, like current resin count and how long you need to wait until your resin is full.
- You can also minimize Resin Tracker so only it's icon will show in system tray. Left-click 2 times on this icon to restore the program.

## Known Issues
- Restoring the window from system tray and inputting your own resin count are still working a bit wonky.
- Some general graphical stuff I'll be sure to smooth over in the upcoming days.

## Contact
Created by [@Wielkimati](https://github.com/Wielkimati)

if you'd like to contact me, I prefer Discord: Wielkimati#0720

## License
[![Licence](https://img.shields.io/github/license/Ileriayo/markdown-badges?style=for-the-badge)](./LICENSE)

## Support

[!["Buy Me A Coffee"](https://www.buymeacoffee.com/assets/img/custom_images/orange_img.png)](https://www.buymeacoffee.com/wielkimati)
