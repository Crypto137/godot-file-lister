using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;
using CsvHelper.Configuration;

namespace GodotFileLister
{
    class Program
    {
        const string outputPrefix = "res://";

        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                // Get arguments
                string projectDirectory = args[0];
                string presetFile = (args.Length > 1) ? args[1] : "presets.txt";    // Default to presets.txt if no preset file is specified

                // Get base directory
                string baseDirectory = AppContext.BaseDirectory;

                // Load directory preset list
                List<DirectoryPreset> presetList = LoadDirectoryPresetList($"{baseDirectory}\\{presetFile}");

                // Prepare an output directory if there isn't one
                if (Directory.Exists($"{baseDirectory}\\Output") == false)
                {
                    Directory.CreateDirectory($"{baseDirectory}\\Output");
                }

                // Parse each directory according to the preset list
                foreach (DirectoryPreset preset in presetList)
                {
                    // Get all files from the directory specified by the preset
                    string[] files = Directory.GetFiles($"{projectDirectory}\\{preset.Path}", preset.Extension);

                    // Process raw file paths
                    for (int i = 0; i < files.Length; i++)
                    {
                        files[i] = files[i].Remove(0, projectDirectory.Length + 1);     // Add 1 to also remove a slash at the beginning of the path
                        files[i] = files[i].Replace('\\', '/');                         // Replace all slashes
                        files[i] = $"{outputPrefix}{files[i]}";                         // Add prefix for Godot resource paths
                    }

                    // Write file list
                    using (StreamWriter writer = new StreamWriter($"{baseDirectory}\\Output\\{preset.OutputFileName}", false))
                    {
                        foreach (string file in files)
                        {
                            writer.WriteLine(file);
                        }
                    }

                    Console.WriteLine($"Parsed {preset.Path} to {preset.OutputFileName}");
                }
            }
            else
            {
                // Show usage hint if no arguments are provided
                Console.WriteLine("Usage: GodotFileLister.exe ProjectDirectory [PresetFile]");
            }

            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }

        static List<DirectoryPreset> LoadDirectoryPresetList(string presetFilePath)
        {
            List<DirectoryPreset> list = new List<DirectoryPreset>();

            // Set up CsvHelper configuration
            CsvConfiguration config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = false,
                Delimiter = "\t",
            };

            // Load the specified file as a list
            if (File.Exists(presetFilePath))
            {
                using (StreamReader reader = new StreamReader(presetFilePath))
                using (CsvReader csv = new CsvReader(reader, config))
                {
                    list = csv.GetRecords<DirectoryPreset>().ToList();
                }
            }

            return list;
        }
    }
}
