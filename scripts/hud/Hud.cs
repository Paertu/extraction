using Godot;
using System;
public partial class Hud : CanvasLayer
{
	[Export] private Player _Player;
	[Export] private Label _SpeedLabel;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (_Player != null && _SpeedLabel != null)
		{
			_SpeedLabel.Text = $"Speed:{_Player._CurrentSpeed}";
		}
	}
}
