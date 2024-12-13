using System;
using System.Diagnostics;
using System.Threading;

class Program
{
    static void Main()
    {
        string title = "Auto-formatting SDCard to Internal and SDCard storage";
        Console.Title = title;
        Console.WriteLine("Loading...");
        int rondo = 1000;
        int rondoNow = 0;

        while (true)
        {
            Console.Clear();
            Console.WriteLine(title);
            Console.WriteLine();
            Console.WriteLine("Supported devices with Android Marshmallow and higher.");
            Console.WriteLine("Ensure USB debugging is enabled");
            Console.WriteLine("-- Attention! You will lose all data on the memory card! --");
            Console.WriteLine();
            Console.WriteLine("Select the formatting mode:");
            Console.WriteLine("1 - Internal [==--------] SDCard");
            Console.WriteLine("2 - Internal [=====-----] SDCard");
            Console.WriteLine("3 - Internal [========--] SDCard");
            Console.WriteLine("4 - Custom");
            Console.WriteLine();

            string begin = Console.ReadLine();
            int smmix = 0;

            switch (begin)
            {
                case "1":
                    smmix = 75;
                    break;
                case "2":
                    smmix = 50;
                    break;
                case "3":
                    smmix = 25;
                    break;
                case "4":
                    smmix = SetCustom();
                    break;
                default:
                    continue;
            }

            StartFormatting(smmix, rondo, ref rondoNow);
        }
    }

    static int SetCustom()
    {
        int smmix = 0;
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Indicate the percentage memory for SDCard partition");
            Console.WriteLine("Please specify number from 0 to 90");
            string input = Console.ReadLine();

            if (int.TryParse(input, out smmix) && smmix >= 0 && smmix <= 90)
            {
                return smmix;
            }
            else
            {
                Console.WriteLine("Incorrect data! Specify a number less than or equal to 90!");
                Thread.Sleep(3000);
            }
        }
    }

    static void StartFormatting(int smmix, int rondo, ref int rondoNow)
    {
        string adbPath = @"adb\win\adb.exe"; // Adjust the path as necessary
        ExecuteCommand($"{adbPath} kill-server");

        Console.Clear();
        Console.WriteLine("Start ADB Server...");
        ExecuteCommand($"{adbPath} start-server");

        string deviceModel = GetDeviceModel(adbPath);
        if (string.IsNullOrEmpty(deviceModel))
        {
            ErrorExit(adbPath, "[ERROR] device not found");
            return;
        }

        Console.Clear();
        Console.WriteLine($"Device: {deviceModel}");

        string devDisksInf = GetDeviceDisksInfo(adbPath);
        if (string.IsNullOrEmpty(devDisksInf))
        {
            ErrorExit(adbPath, "[ERROR] SDCard not found!");
            return;
        }

        Console.WriteLine($"Formatting {devDisksInf}...");
        ExecuteCommand($"{adbPath} shell sm partition {devDisksInf} mixed {smmix}");

        Console.WriteLine("Kill ADB Server...");
        ExecuteCommand($"{adbPath} kill-server");
        Console.WriteLine("Done =)");
        Console.ReadLine();
    }

    static string GetDeviceModel(string adbPath)
    {
        return ExecuteCommand($"{adbPath} shell getprop ro.product.model");
    }

    static string GetDeviceDisksInfo(string adbPath)
    {
        return ExecuteCommand($"{adbPath} shell sm list-disks adoptable");
    }

    static void ErrorExit(string adbPath, string errorMessage)
    {
        Console.WriteLine("Kill ADB Server...");
        ExecuteCommand($"{adbPath} kill-server");
        Console.WriteLine(errorMessage);
        Console.WriteLine("Done, error =(");
        Console.ReadLine();
    }

    static string ExecuteCommand(string command)
    {
        ProcessStartInfo processInfo = new ProcessStartInfo("cmd.exe", "/c " + command)
        {
            RedirectStandardOutput = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        using (Process process = Process.Start(processInfo))
        {
            using (System.IO.StreamReader reader = process.StandardOutput)
            {
                string result = reader.ReadToEnd();
                process.WaitForExit();
                Console.WriteLine(result.Trim()); // Print the ADB output to the console
                return result.Trim(); // Return the output as a string
            }
        }
    }
}
