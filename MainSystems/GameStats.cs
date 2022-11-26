using System.Collections.Generic;
using Godot;
using System;

public class GameStats : Node
{
    public class Stats
    {
        public int Score { get; set; } = 0;
        public Dictionary<string, string> TriesForLevel { get; set; } = new Dictionary<string, string>();
        public string MaxLevel { get; set; } = "Level1";
    }

    public Stats stats;
    public override void _Ready()
    {
        var saveSystem = GetNode<SaveSystem>($"/root/{nameof(SaveSystem)}");
        var stats = saveSystem.LoadObject<Stats>("stats.json") ?? new Stats();
    }

    public void SaveStats()
    {
        var saveSystem = GetNode<SaveSystem>($"/root/{nameof(SaveSystem)}");
        saveSystem.SaveObject(stats, "stats.json");
    }
}
