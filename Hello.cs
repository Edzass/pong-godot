using Godot;
using System;

public partial class Hello : Node
{
	// Called when the node enters the scene tree for the first time.
	private int counter = 0;
	private string playerName = "Edgars";
	private Vector2 spawnPosition = new Vector2(640, 360);
	public override void _Ready()
	{

		GD.Print("Sveiks " + playerName + " !");
		GD.Print(counter);
		GD.Print(spawnPosition);
	
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
