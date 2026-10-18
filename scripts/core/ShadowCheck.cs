using Godot;
using System;

public partial class ShadowCheck : OmniLight3D
{
	[Export] private Node3D _player;
	private MeshInstance3D _planeMeshInstance;
	private Vector3 _lampPosition;
	private StandardMaterial3D _planeMaterial;
	public bool isHidden;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		var lightPosition = GlobalPosition;
		var planeMesh = new PlaneMesh();

		_planeMeshInstance = new MeshInstance3D();
		_planeMeshInstance.Mesh = planeMesh;

		_planeMaterial = new StandardMaterial3D();
		_planeMeshInstance.MaterialOverride = _planeMaterial;
		_planeMaterial.ShadingMode = StandardMaterial3D.ShadingModeEnum.Unshaded;
		AddChild(_planeMeshInstance);
		_lampPosition = _planeMeshInstance.GlobalPosition;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		var playerLocation = _player.GlobalPosition;
		_planeMeshInstance.LookAt(playerLocation);
		var dist = _lampPosition.DistanceTo(playerLocation);
		_planeMeshInstance.Scale = new Vector3(0.1f, 1.5f, dist);

		_planeMaterial.AlbedoColor = Colors.Red;

		var spaceState = GetWorld3D().DirectSpaceState;
		
		var RayIntersectResult = spaceState.IntersectRay(PhysicsRayQueryParameters3D.Create(GlobalPosition, _player.GlobalPosition));
		if (RayIntersectResult.Count > 0)
		{
			var collider = RayIntersectResult["collider"].Obj;
			if (collider == _player)
			{
				_planeMaterial.AlbedoColor = Colors.Red;
				isHidden = false;
				GD.Print(isHidden);
			}
			else
			{
				_planeMaterial.AlbedoColor = Colors.Green;
				isHidden = true;
				GD.Print(isHidden);
			}
		}
	}
}
