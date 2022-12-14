using Godot;

public class MainMenu : Control
{
    public void OnNewGameButtonPressed()
    {
        GetNode<LevelManager>("/root/LevelManager").StartFirstLevel();
    }

    public void _on_AboutButton_pressed()
    {
        GetTree().ChangeScene("res://scenes/gui/Menu/CreditsMenu.tscn");
    }
    public void _on_QuitButton_pressed()
    {
        GetTree().Quit();
    }
}
