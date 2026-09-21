using Godot;

public partial class Paddle : CharacterBody2D
{
	[Export] public string InputUp = "p1_up";
	[Export] public string InputDown = "p1_down";
	[Export] public float Speed = 420.0f;

	private bool IsPressedOrFallback(string actionName, Key fallbackKey)
	{
		if (Input.IsActionPressed(actionName))
		{
			return true;
		}

		if (!InputMap.HasAction(actionName))
		{
			return Input.IsKeyPressed(fallbackKey);
		}

		return false;
	}

	public override void _PhysicsProcess(double delta)
	{
		float direction = 0.0f;
		Key fallbackUp = Key.W;
		Key fallbackDown = Key.S;

		if (InputUp == "p2_up")
		{
			fallbackUp = Key.Up;
		}
		if (InputDown == "p2_down")
		{
			fallbackDown = Key.Down;
		}

		if (IsPressedOrFallback(InputUp, fallbackUp))
		{
			direction -= 1.0f;
		}
		if (IsPressedOrFallback(InputDown, fallbackDown))
		{
			direction += 1.0f;
		}

		Velocity = new Vector2(0.0f, direction * Speed);
		MoveAndSlide();

		Vector2 viewportSize = GetViewportRect().Size;
		float halfHeight = 20.0f;
		float topBoundary = 50.0f;
		float bottomBoundary = viewportSize.Y - 50.0f;
		Position = new Vector2(
			Position.X,
			Mathf.Clamp(Position.Y, topBoundary + halfHeight, bottomBoundary - halfHeight));
	}
}
