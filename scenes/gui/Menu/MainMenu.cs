using Godot;
using System;

public class MainMenu : Control
{
	public void OnNewGameButtonPressed()
	{
		GetNode<LevelManager>("/root/LevelManager").StartFirstLevel();
	}
}
