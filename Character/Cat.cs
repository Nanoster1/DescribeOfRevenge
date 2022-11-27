using Godot;

public class Cat : KinematicBody2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {

    }
    private Vector2 velocity = Vector2.Zero;
    private bool isLeft = false;
    public override void _PhysicsProcess(float delta)
    {
        velocity.y += 10;
        if (isLeft)
        {
            GetNode<AnimatedSprite>("cats").FlipH = true;
            velocity.x -= 2;
        }
        else
        {
            GetNode<AnimatedSprite>("cats").FlipH = false;
            velocity.x += 2;
        }
        if (velocity.x >= 100)
        {
            isLeft = true;
        }
        else if (velocity.x <= -100)
        {
            isLeft = false;
        }
        velocity = MoveAndSlide(velocity, new Vector2(0, -1));
    }
}
