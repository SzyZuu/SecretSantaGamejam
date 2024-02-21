using Godot;
using System;

public partial class ghost_enemy : CharacterBody2D
{
	//[Export] private Area2D area = null;
	[Export] private CharacterBody2D player = null;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Area2D area = GetNode<Area2D>("Area2D");
		area.BodyEntered += OnBodyEntered;
		AddCollisionExceptionWith(player);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		AnimatedSprite2D enemySprite = GetNode<AnimatedSprite2D>("EnemySprite");
		enemySprite.Animation = "default";

		Vector2 motion = Vector2.Zero;
		motion += Position.DirectionTo(player.Position);
		Velocity = motion * 200;

		if (Position.DistanceTo(player.Position) < 2)
		{
			Velocity = Vector2.Zero;
		}
		
		MoveAndSlide();
	}

	private void OnBodyEntered(dynamic body)
	{
		if (body.HasMethod("Hit"))
		{
			body.Hit(50);
		}
	}
}
