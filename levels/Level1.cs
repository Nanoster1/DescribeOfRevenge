using Godot;
using System;

public class Level1 : Node2D
{
    public static int DeathCounter = 0;

    public override void _Ready()
    {
        GetNode<Character>("Character")
            .GetNode<CharacterSystem>("CharacterSystem").Connect(nameof(CharacterSystem.HealthChanged), this, nameof(OnHealthChanged));
    }

    public void _on_Area2D_body_exited(Node body)
    {
        if (body is Character)
        {
            Death();
        }
    }

    public void Death()
    {
        if (DeathCounter > 0)
        {
            GetNode<LevelManager>("/root/LevelManager").StartFirstLevel();
            DeathCounter = 0;
            return;
        }

        DeathCounter++;
        GetTree().ReloadCurrentScene();
    }

    public void OnHealthChanged(int oldHealth, int newHealth)
    {
        if (newHealth <= 0) Death();
        var healthInfo = GetNode<Character>("Character")
            .GetNode<CanvasLayer>("CanvasLayer")
                .GetNode<NinePatchRect>("NinePatchRect")
                    .GetNode<NinePatchRect>("HealthHUDBar");

        healthInfo.GetNode<TextureProgress>("TextureProgress").Value = newHealth;
        healthInfo.GetNode<NinePatchRect>("HealthInfo")
            .GetNode<Label>("Label2").Text = newHealth.ToString();
    }
}
