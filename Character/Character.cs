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
	public override void _Ready()
	{
		animatedSprite = GetNode<AnimatedSprite>("AnimatedSprites");
		CharSystem = GetNode<CharacterSystem>(nameof(CharacterSystem));
		timer.Elapsed += (x, y) =>
		{
			isCalm2 = true;

		};
		timer.AutoReset = false;
	}


	public override void _PhysicsProcess(float delta)
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

		velocity.y += gravity;
		velocity = MoveAndSlide(velocity, new Vector2(0, -1));
		_play_animation(velocity);
	}
	public void _play_jump_animation()
	{
		animatedSprite.Play("jump");
	}
	public void _play_animation(Vector2 direction)
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

		}
		else if (direction.y >= 0)
		{
			animatedSprite.Play("down");
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
