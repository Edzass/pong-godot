using Godot;

public partial class Ball : CharacterBody2D
{
	private Vector2 velocity;
	private bool isStopped;

	[Export] public float BaseSpeed = 420.0f;
	[Export] public float Radius = 12.0f;
	public Game GameRef { get; set; }

	public override void _Ready()
	{
		ResetBall();
	}

	public void ResetBall()
	{
		isStopped = false;

		Vector2 viewportSize = GetViewportRect().Size;
		GlobalPosition = viewportSize / 2.0f;

		RandomNumberGenerator rng = new RandomNumberGenerator();
		rng.Randomize();

		float directionX = rng.Randf() > 0.5f ? 1.0f : -1.0f;
		float directionY = rng.RandfRange(0.2f, 0.8f) * (rng.Randf() > 0.5f ? 1.0f : -1.0f);

		velocity = new Vector2(directionX, directionY).Normalized() * BaseSpeed;
	}

	public void StopBall()
	{
		isStopped = true;
		velocity = Vector2.Zero;
	}

	public override void _PhysicsProcess(double delta)
	{
		if (isStopped)
			return;

		Vector2 movement = velocity * (float)delta;
		KinematicCollision2D collision = MoveAndCollide(movement);

		if (collision != null)
		{
			velocity = velocity.Bounce(collision.GetNormal());
		}

		Vector2 viewportSize = GetViewportRect().Size;
		float leftLimit = Radius;
		float rightLimit = viewportSize.X - Radius;
		float topLimit = Radius;
		float bottomLimit = viewportSize.Y - Radius;

		if (Position.X < leftLimit)
		{
			GameRef?.OnScore(false);
			ResetBall();
			return;
		}
		if (Position.X > rightLimit)
		{
			GameRef?.OnScore(true);
			ResetBall();
			return;
		}

		if (Position.Y < topLimit)
		{
			Position = new Vector2(Position.X, topLimit);
			velocity.Y = Mathf.Abs(velocity.Y);
		}
		else if (Position.Y > bottomLimit)
		{
			Position = new Vector2(Position.X, bottomLimit);
			velocity.Y = -Mathf.Abs(velocity.Y);
		}
	}
}
