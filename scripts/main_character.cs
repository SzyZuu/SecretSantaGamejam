using Godot;
using System;

public partial class main_character : CharacterBody2D
{
	[Export] private int _speed = 250;
	Vector2 _velocity = Vector2.Zero;
	private int _health = 100;
		
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}

	public override void _PhysicsProcess(double delta)
	{
		AnimatedSprite2D playerSprite = GetNode<AnimatedSprite2D>("Sprite2D");
		
		if (Velocity.X > 1 || Velocity.X < 0 || Velocity.Y > 1 || Velocity.Y < 0)
		{
			playerSprite.Animation = "running";
		}
		else
		{
			playerSprite.Animation = "default";
		}
		
		Vector2 direction = Input.GetVector("player_left", "player_right", "player_up", "player_down");
		_velocity = direction * _speed;
		Velocity = _velocity;
		MoveAndSlide();
		
		if (Velocity.X < 0)
		{
			playerSprite.FlipH = true;
		}else if (Velocity.X > 1)
		{
			playerSprite.FlipH = false;
		}
	}

	public void Hit(int damage)
	{
		_health -= damage;
		GD.Print(_health);
	}
}
