using Godot;
using System;

public partial class Guard : CharacterBody3D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		HandleGravity(delta);
	}

	private void HandleGravity(double delta)
	{
		Vector3 velocity = Velocity;

		if (!IsOnFloor())
		{
			velocity += GetGravity() * (float)delta;
		}
		Velocity = velocity;
		MoveAndSlide();
	}
}
