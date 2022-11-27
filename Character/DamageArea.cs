using Godot;

public class DamageArea : Area2D
{
    private bool damaged = false;
    public override void _Ready()
    {
        GetNode<Timer>("Timer").Start(0.5f);
    }
    public override void _Process(float delta)
    {
        if (!damaged && GetOverlappingBodies().Count != 0)
        {
            foreach (var body in GetOverlappingBodies())
            {
                GD.Print(body.GetType());
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
