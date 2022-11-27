using Godot;
using System;

public class Cat : KinematicBody2D
{

    private Vector2 _velocity = Vector2.Right * 55;
    private Vector2 _newVelocity = Vector2.Zero;
    private Vector2 position = Vector2.Zero;
    private bool _isDown = false;

    public override void _Ready()
    {

    }

    public void OnCharacterMove(Vector2 velocity, Vector2 position)
    {

        _velocity = velocity;
    }

    public override void _PhysicsProcess(float delta)
    {
        Vector2 range = default;
        if (_velocity.x < 0)
        {
            GetNode<AnimatedSprite>("AnimatedSprite").FlipH = true;
            if (Position.x - position.x <= 50 && _velocity.x >= 0)
            {
                range = Vector2.Right * 110;
            }

        }
        else if (_newVelocity.x > 0)
        {
            GetNode<AnimatedSprite>("AnimatedSprite").FlipH = false;
            if (Position.x - position.x >= 50 && _velocity.x <= 0)
            {
                range = Vector2.Left * 110;
            }

            //Position += Vector2.Left * 110;
        }

        Position += range;
        if (!IsOnFloor() && position.y > Position.y)
        {
            _isDown = true;

        }
        else if (IsOnFloor())
        {
            _isDown = false;
        }
        if (_isDown)
        {
            _velocity.y = 300;
        }
        GD.Print(_isDown);
        MoveAndSlide(_velocity);
        _velocity = _newVelocity;
    }
}
