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
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                // Get arguments
                string projectDirectory = args[0];
                string presetFile = (args.Length > 1) ? args[1] : "presets.txt";

                string baseDirectory = AppContext.BaseDirectory;

                List<DirectoryPreset> presetList = LoadDirectoryPresetList($"{baseDirectory}\\{presetFile}");

                Console.WriteLine($"{projectDirectory}   {presetFile}");

                if (Directory.Exists($"{baseDirectory}\\Output") == false)
                {
                    Directory.CreateDirectory($"{baseDirectory}\\Output");
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
