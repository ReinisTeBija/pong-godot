using Godot;
using System;

public partial class Paddle : CharacterBody2D
{
    [Export] public string InputUp = "p1_up";
    [Export] public string InputDown = "p1_down";
    [Export] public float Speed = 420.0f;

    public override void _PhysicsProcess(double delta)
    {
        Vector2 velocity = Velocity;
        velocity.Y = 0;

        if (Input.IsActionPressed(InputUp))
        {
            velocity.Y -= 1;
        }

        if (Input.IsActionPressed(InputDown))
        {
            velocity.Y += 1;
        }

        Velocity = velocity * Speed;
        MoveAndSlide();
    }
}
