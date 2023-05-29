using Godot;
using System;

public partial class PlayerCharac : CharacterBody2D
{
	public const float Speed = 300.0f;
	
	
	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();
	
	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		if (direction != Vector2.Zero)
		{
			velocity.X = direction.X * Speed;
			velocity.Y = direction.Y * Speed;
		}
		else
		{
			velocity.X = 0;
			velocity.Y = 0;
		}
		
		Velocity = velocity;
		MoveAndSlide();
	}
}
