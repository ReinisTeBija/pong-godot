using Godot;
using System;

public partial class Hello : Node
{
	private int counter = 0;
	private string playerName = "Pong Player";
	private Vector2 spawnPosition = new Vector2(640, 360);
	public override void _Ready()
	{
		GD.Print("Sveiks no C#!");
		GD.Print(counter);
		GD.Print(playerName);
		GD.Print(spawnPosition);
	}


	public override void _Process(double delta)
	{
	}
}
