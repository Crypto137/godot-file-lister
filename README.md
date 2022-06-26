# Godot File Lister
This is a small console application I wrote in C# for creating lists of resource files that can be used with Godot Engine projects.


The reason I made this is because Godot games have limited access to resource directories once the game gets exported, so if you want to load resources dynamically you have to know the paths beforehand. You can handle this either by hardcoding a lot of stuff, which is a bad idea, or using listfiles.


With this tool you can quickly create listfiles by providing presets. When you use this application you need to provide a Godot project directory. By default it loads presets from presets.txt, but you can also provide an optional PresetFileName argument if you want to use different presets for different cases (i.e. different projects).

> GodotFileLister.exe ProjectDirectoryPath [PresetFileName]


Preset files are tsv tables that follow the following structure:


> RelativePath	ExtensionMask	OutputFileName


So an actual preset file would look like this:


> ui\icons\	*.png	icons.txt


This will generate an icons.txt file that will include Godot resource paths beginning with res:// to all PNG files in the specified path. All output files go the Output directory.