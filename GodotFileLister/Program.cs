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
            string projectDirectory = "";
            string presetFile = "presets.txt";

            List<DirectoryPreset> presetList = LoadDirectoryPresetList($"{AppContext.BaseDirectory}\\{presetFile}");

            foreach (DirectoryPreset preset in presetList)
            {
                Console.WriteLine($"{preset.Path}   {preset.Extension}  {preset.OutputFileName}");
            }

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
