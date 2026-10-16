using Godot;
using System;

public partial class ShadowCheck : OmniLight3D
{
	[Export] private Node3D _player;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Console.Write("ShadowCheck Ready");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		var spaceState = GetWorld3D().DirectSpaceState;
		
		var RayIntersectResult = spaceState.IntersectRay(PhysicsRayQueryParameters3D.Create(GlobalPosition, _player.GlobalPosition));
		if (RayIntersectResult.Count > 0)
		{
			var collider = RayIntersectResult["collider"].Obj;
			if (collider == _player)
			{
				GD.Print("Player IS visible");
			}
			else
			{
				GD.Print("Player NOT visible");	
			}
		}
	}
}
