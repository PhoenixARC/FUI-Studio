using System;
using System.IO;

namespace FUI_Studio.Classes
{
    static public class Settings
    {
        public static bool CheckForUpdates { get; set; } = false;
        public static bool IsPortable { get; set; } = false;
        public static string TempDir { get; set; }

        private static void InitializeSettingsFile()
        {
            Save();
        }

        public static void Load()
        {
            if (!File.Exists(Environment.CurrentDirectory + "/settings.ini"))
            {
                InitializeSettingsFile();
            }
            string settings = File.ReadAllText(Environment.CurrentDirectory + "/settings.ini").Replace(" ", "");
            string[] Lines = settings.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);
            foreach (string Line in Lines)
            {
                if (Line.StartsWith("*")) continue; // ignore comments
                var args = Line.Split('=');
                if (args.Length < 2)
                    continue;
                string Param = args[0];
                string Value = args[1];
                Console.WriteLine(Param + ": " + Value);
                switch (Param)
                {
                    case "IsPortable":
                        IsPortable = Value.ToLower() == "true";
                        continue;
                    case "AutoUpdate":
                        CheckForUpdates = Value.ToLower() == "true";
                        continue;
                    default:
                        Console.WriteLine($"Unknown setting: {Param} ");
                        continue;
                }
            }
        }

        public static void Save()
        {
            File.WriteAllText(Environment.CurrentDirectory + "/settings.ini",
                "**Settings**\n" +
                " * you can change any variable here or in the editor!\n" +
                $"IsPortable = {IsPortable.ToString().ToLower()}\n" +
                $"AutoUpdate = {CheckForUpdates.ToString().ToLower()}\n"
                );
        }
    }
}
