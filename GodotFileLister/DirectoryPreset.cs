using System;
using CsvHelper.Configuration.Attributes;

namespace GodotFileLister
{
    public class DirectoryPreset
    {
        public DirectoryPreset(string path, string extension, string outputFileName)
        {
            Path = path;
            Extension = extension;
            OutputFileName = outputFileName;
        }

        [Index(0)]
        public string Path { get; private set; }
        [Index(1)]
        public string Extension { get; private set; }
        [Index(2)]
        public string OutputFileName { get; private set; }
    }
}
