using Godot;

public class Level1 : Node2D
{
	public static int DeathCounter = 0;

	public override void _Ready()
	{
		GetNode<Character>("Character")
			.GetNode<CharacterSystem>("CharacterSystem").Connect(nameof(CharacterSystem.HealthChanged), this, nameof(OnHealthChanged));
	}
	public void _on_Enemy_health_depleted(int health)
	{
		GetNode<Character>("Character")
			.GetNode<CharacterSystem>("CharacterSystem").TakeDamage(health);
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
		if (newHealth <= 0)
		{
			var character = GetNode<Character>("Character");
			character.GetNode<AnimatedSprite>("AnimatedSprites").Play("Death");
			var rnd = new System.Random();
			var value = rnd.Next(1, 3);
			if (value == 1)
			{
				character.GetNode<AudioStreamPlayer2D>("GoToMother").Play();
			}
			else
			{
				character.GetNode<AudioStreamPlayer2D>("Die").Play();
			}

			var timer = new System.Timers.Timer(3000);
			timer.Elapsed += (x, y) =>
			{
				Death();
				timer.Dispose();
			};
			timer.Start();
		}
		var healthInfo = GetNode<Character>("Character")
			.GetNode<CanvasLayer>("CanvasLayer")
				.GetNode<NinePatchRect>("NinePatchRect")
					.GetNode<NinePatchRect>("HealthHUDBar");

		healthInfo.GetNode<TextureProgress>("TextureProgress").Value = newHealth;
		healthInfo.GetNode<NinePatchRect>("HealthInfo")
			.GetNode<Label>("Label2").Text = newHealth.ToString();
	}
}
