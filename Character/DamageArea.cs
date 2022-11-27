using Godot;

public class DamageArea : Area2D
{
    private bool damaged = false;
    public override void _Ready()
    {
        GetNode<Timer>("Timer").Start(0.2f);
    }
    public override void _Process(float delta)
    {
        if (!damaged && GetOverlappingBodies().Count != 0)
        {
            foreach (var body in GetOverlappingBodies())
            {
                var enemy = body as KinematicBody2D;
                if (enemy != null && enemy.Name.Contains("Enemy"))
                {
                    var rnd = new System.Random();
                    enemy.GetParent().RemoveChild(enemy);
                    //GetParent().GetNode<AudioStreamPlayer2D>("Mother").Playing = true;
                    var value = rnd.Next(1, 4);
                    if (value == 1)
                    {
                        GetParent().GetNode<AudioStreamPlayer2D>("Mother").Play();
                    }
                    else if (value == 2)
                    {
                        GetParent().GetNode<AudioStreamPlayer2D>("Mother2").Play();
                    }
                    else
                    {
                        GetParent().GetNode<AudioStreamPlayer2D>("Get").Play();
                    }
                }
            }
            damaged = true;

        }
    }
    public void _on_Timer_timeout()
    {
        GetParent().RemoveChild(this);
        QueueFree();
    }
}
