using Godot;

public class Character : KinematicBody2D
{
    public CharacterSystem CharSystem { get; private set; }
    public AnimatedSprite animatedSprite { get; private set; }
    private const float gravity = 15f;
    private const float jumpForce = 350;
    private Vector2 velocity = Vector2.Zero;
    private LastDirection? direction = null;
    private System.Timers.Timer timer = new System.Timers.Timer(4000);
    private bool isCalm2 = false;
    private bool isDamage = false;
    public override void _Ready()
    {
        animatedSprite = GetNode<AnimatedSprite>("AnimatedSprites");
        CharSystem = GetNode<CharacterSystem>(nameof(CharacterSystem));
        timer.Elapsed += (x, y) =>
        {
            isCalm2 = true;

        };
        timer.AutoReset = false;
        animatedSprite.Connect("animation_finished", this, "_finish");
    }
    public void _finish()
    {
        if (animatedSprite.Animation == "Attack")
        {
            isDamage = false;
            GetNode<AudioStreamPlayer2D>("Attack").Playing = false;
        }
    }

    public override void _PhysicsProcess(float delta)
    {
        if (!(animatedSprite.Animation == "Death"))
        {
            if (Input.IsActionPressed("move_left"))
            {
                velocity.x = -CharSystem.Speed;
                animatedSprite.FlipH = true;
                timer.Stop();
                isCalm2 = false;
            }

            else if (Input.IsActionPressed("move_right"))
            {
                velocity.x = CharSystem.Speed;
                animatedSprite.FlipH = false;
                timer.Stop();
                isCalm2 = false;
            }
            else
            {
                velocity.x = 0;
                if (!timer.Enabled)
                {
                    timer.Start();
                }

            }

            if (Input.IsActionJustPressed("move_up") && IsOnFloor())
            {
                velocity.y = -jumpForce;
                direction = LastDirection.Up;
                timer.Stop();
                isCalm2 = false;
                _play_jump_animation();

            }

            else if (Input.IsActionJustPressed("move_up") && direction == LastDirection.Up)
            {
                velocity.y = -jumpForce;
                direction = null;
                timer.Stop();
                isCalm2 = false;
                _play_jump_animation();
            }
            else if (Input.IsActionJustPressed("move_down") && IsOnFloor())
            {
                velocity.y = 1;
                direction = LastDirection.Down;
                timer.Stop();
                isCalm2 = false;
            }
            if (Input.IsActionJustPressed("attack"))
            {
                animatedSprite.Play("Attack");
                GetNode<AudioStreamPlayer2D>("Attack").Playing = true;
                isDamage = true;
                timer.Stop();
                isCalm2 = false;
                var attack = ResourceLoader.Load<PackedScene>("res://Character/DamageArea.tscn");
                Area2D newGround = attack.Instance<Area2D>();
                GD.Print(Position);

                AddChild(newGround);

                if (animatedSprite.FlipH)
                {
                    newGround.Position = new Vector2(newGround.Position.x - 50, newGround.Position.y);
                }
                else
                {
                    newGround.Position = new Vector2(newGround.Position.x + 50, newGround.Position.y);
                }
            }
            else
            {
                _play_animation(velocity);

            }
            if (!GetNode<AudioStreamPlayer2D>("Run").Playing)
            {
                GetNode<AudioStreamPlayer2D>("Run").Playing = true;
            }

            velocity.y += gravity;
            velocity = MoveAndSlide(velocity, new Vector2(0, -1));
        }
    }
    public void _play_jump_animation()
    {
        if (!isDamage)
        {
            animatedSprite.Play("jump");
            GetNode<AudioStreamPlayer2D>("Run").Playing = false;
        }
    }
    public void _play_animation(Vector2 direction)
    {
        if (!isDamage)
        {
            if (direction == Vector2.Zero && IsOnFloor())
            {
                if (isCalm2)
                {
                    animatedSprite.Play("calm2");
                }
                else
                {
                    animatedSprite.Play("calm");
                }
                GetNode<AudioStreamPlayer2D>("Run").Playing = false;
            }
            else if (direction.y > 10)
            {
                animatedSprite.Play("down");
                GetNode<AudioStreamPlayer2D>("Run").Playing = false;
            }
            else if (direction.y == 0)
            {
                animatedSprite.Play("run");
                if (!GetNode<AudioStreamPlayer2D>("Run").Playing)
                {
                    GetNode<AudioStreamPlayer2D>("Run").Playing = true;
                }

            }
        }

    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
        timer.Dispose();
    }
}
enum LastDirection
{
	Up,
	Down,
	Left,
	Right
}
