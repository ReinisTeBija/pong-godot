// Imports Godot classes used by this script.
using Godot;
// Imports standard .NET functionality.
using System;

// Declares the Paddle class as a movable and collidable Godot node.
public partial class Paddle : CharacterBody2D
{
    [Export] public string InputUp = "p1_up";
    [Export] public string InputDown = "p1_down";
    [Export] public float Speed = 420.0f;

    private float _fixedX;

    public override void _Ready()
    {
        _fixedX = Position.X;
    }

    public override void _PhysicsProcess(double delta)
    {
        Vector2 velocity = Vector2.Zero;

        if (Input.IsActionPressed(InputUp))
            velocity.Y -= 1;
        if (Input.IsActionPressed(InputDown))
            velocity.Y += 1;

        Velocity = velocity * Speed;
        MoveAndSlide();

        // Piespiedu kārtā notur X vietā, lai fizikas "recovery" to nevarētu aizvilkt
        Position = new Vector2(_fixedX, Position.Y);
    }
}
