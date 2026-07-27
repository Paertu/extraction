using Godot;

public partial class Player : CharacterBody3D
{
    public const float Speed = 5.0f;
    public const float JumpVelocity = 4.5f;
	public float MouseSensitivity = 0.005f;
    public float PlayerStandingHeight = 1.5f;
    public float PlayerCrouchHeight = 0.1f;
    [Export] private Camera3D _camera;
    [Export] private CollisionShape3D _CollisionShape;
    private float _CameraPitch = 0.0f;
    private float _MaxCameraPitch = 1.5f;

    private void HandleMovement(double delta)
    {
        Vector3 velocity = Velocity;

        if (!IsOnFloor())
        {
            velocity += GetGravity() * (float)delta;
        }

        if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
        {
            velocity.Y = JumpVelocity;
        }

        Vector2 inputDir = Input.GetVector("Left", "Right", "Forward", "Back");
        Vector3 direction = (Transform.Basis * new Vector3(inputDir.X, 0, inputDir.Y)).Normalized();
        
        if (direction != Vector3.Zero)
        {
            velocity.X = direction.X * Speed;
            velocity.Z = direction.Z * Speed;
        }
        else
        {
            velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
            velocity.Z = Mathf.MoveToward(Velocity.Z, 0, Speed);
        }

        if (Input.IsActionPressed("Crouch"))
        {
            var capsule = _CollisionShape.Shape as CapsuleShape3D;
            capsule.Height = PlayerCrouchHeight;
        }
        else
        {
            var capsule = _CollisionShape.Shape as CapsuleShape3D;
            capsule.Height = Mathf.Lerp(capsule.Height, PlayerStandingHeight, 0.1f);   
        }

        Velocity = velocity;
        MoveAndSlide();
    }

    private void HandleCameraMovement(InputEvent @event)
    {
        if (@event is InputEventMouseMotion mouseMotion && Input.MouseMode == Input.MouseModeEnum.Captured)
		{
			RotateY(-mouseMotion.Relative.X * MouseSensitivity);

            _CameraPitch -= mouseMotion.Relative.Y * MouseSensitivity;
            _CameraPitch = Mathf.Clamp(_CameraPitch, -_MaxCameraPitch, _MaxCameraPitch);

            Vector3 rot = _camera.Rotation;
            rot.X = _CameraPitch;
            _camera.Rotation = rot;
		}
    }
}