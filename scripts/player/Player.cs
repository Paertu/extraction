using Godot;
using System;

public partial class Player : CharacterBody3D
{
	public override void _Ready()
	{
		Input.MouseMode = Input.MouseModeEnum.Captured;
	}

	public override void _PhysicsProcess(double delta)
	{
		HandleMovement(delta);
	}

	public override void _Input(InputEvent @event)
	{
		HandleCameraMovement(@event);
	}
}
