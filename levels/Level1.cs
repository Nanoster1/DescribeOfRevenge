using Godot;
using System;

public class Level1 : Node2D
{
    public override void _Ready()
    {
        GetNode<Character>("Character")
            .GetNode<CharacterSystem>("CharacterSystem").Connect(nameof(CharacterSystem.HealthChanged), this, nameof(OnHealthChanged));
    }

    public void OnHealthChanged(int oldHealth, int newHealth)
    {
        var healthInfo = GetNode<Character>("Character")
            .GetNode<CanvasLayer>("CanvasLayer")
                .GetNode<NinePatchRect>("NinePatchRect")
                    .GetNode<NinePatchRect>("HealthHUDBar");

        healthInfo.GetNode<TextureProgress>("TextureProgress").Value = newHealth;
        healthInfo.GetNode<NinePatchRect>("HealthInfo")
            .GetNode<Label>("Label2").Text = newHealth.ToString();
    }
}
