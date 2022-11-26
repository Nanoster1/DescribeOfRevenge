using System;
using System.Collections.Generic;
using Godot;

public class LevelManager : Node
{
    private int _currentLevel = 1;
    public IReadOnlyList<string> Levels { get; } = new List<string>()
    {
    };

    public void StartFirstLevel()
    {
        _currentLevel = 0;
        NextLevel();
    }

    public void NextLevel()
    {
        if (_currentLevel + 1 >= Levels.Count)
        {
            GetTree().ChangeScene("res://scenes/gui/MainMenu.tscn");
            return;
        }

        GetTree().ChangeScene(Levels[++_currentLevel]);
    }
}