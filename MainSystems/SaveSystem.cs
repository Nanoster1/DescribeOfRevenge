using System.Reflection;
using System.IO;
using System;
using Directory = System.IO.Directory;
using File = System.IO.File;
using Path = System.IO.Path;
using System.Text.Json;

public class SaveSystem : Godot.Node
{
    public DirectoryInfo SaveDirectory => Directory.CreateDirectory(SavePath);

    public string SavePath { get; } = Path.Combine(
        new FileInfo(Assembly.GetExecutingAssembly().Location).DirectoryName,
        "Saves");

    public void SaveObject<T>(T obj, string fileName)
    {
        var filePath = Path.Combine(SavePath, fileName);
        var json = JsonSerializer.Serialize(obj);
        File.WriteAllText(filePath, json);
    }

    public T LoadObject<T>(string fileName)
    {
        var filePath = Path.Combine(SavePath, fileName);
        if (!File.Exists(filePath)) return default;
        var json = File.ReadAllText(filePath);
        return JsonSerializer.Deserialize<T>(json);
    }
}
