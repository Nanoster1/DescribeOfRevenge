using Godot;

public class Character : KinematicBody2D
{
    public CharacterSystem CharSystem { get; private set; }
    public Sprite PlayerImage { get; private set; }

    public override void _Ready()
    {
        PlayerImage = (Sprite)GetNode("Person");
        CharSystem = (CharacterSystem)GetNode(nameof(CharacterSystem));
    }

    public override void _PhysicsProcess(float delta)
    {

        Vector2 direction = Vector2.Down;
        float gravity = 9.8f;
        if (Input.IsActionPressed("move_left"))
        {
            direction.x = -1;
            PlayerImage.FlipH = true;
        }

        if (Input.IsActionPressed("move_right"))
        {
            direction.x = +1;
            PlayerImage.FlipH = false;
        }

        if (Input.IsActionPressed("move_up") && IsOnFloor())
        {
            direction.y -= gravity;
        }

        if (Input.IsActionPressed("move_down") && IsOnFloor())
        {
            direction.y += gravity;
        }
        MoveAndSlide(direction * CharSystem.Speed, Vector2.Up);
    }
}
