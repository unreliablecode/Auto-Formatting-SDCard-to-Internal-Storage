# Auto-Formatting SDCard to Internal Storage

## Description
This C# console application automates the process of formatting an SD card to internal storage on Android devices using ADB (Android Debug Bridge) commands. It provides a user-friendly interface to select formatting options and ensures that the necessary ADB commands are executed correctly.

## Features
- Supports devices running Android Marshmallow and higher.
- Allows users to choose between predefined formatting modes or specify a custom percentage for internal storage allocation.
- Displays real-time ADB command output in the console.
- Error handling for common issues such as device connection problems and missing SD cards.

## Prerequisites
- .NET Core or .NET Framework installed on your machine.
- ADB (Android Debug Bridge) installed and accessible from the command line.
- An Android device with USB debugging enabled.
- A USB cable to connect the Android device to your computer.

## Installation
1. Clone the repository:
   ```bash
   git clone https://github.com/unreliablecode/AutoFormattingSDCard.git
   cd AutoFormattingSDCard
   ```

2. Open the project in your preferred C# IDE (e.g., Visual Studio).

3. Ensure that the path to `adb.exe` is correctly set in the code:
   ```csharp
   string adbPath = @"adb\win\adb.exe"; // Adjust the path as necessary
   ```

4. Build the project.

## Usage
1. Connect your Android device to your computer via USB.
2. Ensure that USB debugging is enabled on your device.
3. Run the application from your IDE or command line.
4. Follow the on-screen prompts to select the formatting mode:
   - **1** - Internal [==--------] SDCard
   - **2** - Internal [=====-----] SDCard
   - **3** - Internal [========--] SDCard
   - **4** - Custom (specify a percentage from 0 to 90)
5. The application will display the ADB output and notify you when the formatting is complete.

## Acknowledgments
- Thanks to the open-source community for their contributions to ADB and C# development.
- Special thanks to the developers of the Android platform for providing the tools necessary for this project.
