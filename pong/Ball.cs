// Imports Godot classes used by this script.
using Godot;
// Imports standard .NET functionality.
using System;

// Declares the Ball class as a Godot node that can move and collide.
public partial class Ball : CharacterBody2D
{
    // Stores the ball's current movement direction and speed.
    private Vector2 velocity;
    // Exposes the ball's starting speed in the Godot editor.
    [Export] public float BaseSpeed = 420.0f;
    // References the main game controller from the Godot editor.
    [Export] public Main GameController;

    // Runs once when the ball enters the scene.
    public override void _Ready()
    {
        // Places the ball in the center and gives it a random direction.
        ResetBall();
    }

    // Runs every physics frame to move the ball and check its position.
    public override void _PhysicsProcess(double delta)
    {
        Vector2 motion = velocity * (float)delta;

        for (int i = 0; i < 4 && motion.LengthSquared() > 0.01f; i++)
        {
            KinematicCollision2D collision = MoveAndCollide(motion);
            if (collision == null)
                break;

            velocity = velocity.Bounce(collision.GetNormal());
            motion = collision.GetRemainder().Bounce(collision.GetNormal());
        }

        // Checks whether the ball left the left side of the play area.
        if (Position.X < 0)
        {
            // Checks whether a game controller is available.
            if (GameController != null)
            {
                // Reports that the right player scored.
                GameController.OnScore(false);
            }
            else
            {
                // Resets the ball when no game controller is available.
                ResetBall();
            }
        }
        // Checks whether the ball left the right side of the play area.
        else if (Position.X > 1280)
        {
            // Checks whether a game controller is available.
            if (GameController != null)
            {
                // Reports that the left player scored.
                GameController.OnScore(true);
            }
            else
            {
                // Resets the ball when no game controller is available.
                ResetBall();
            }
        }
    }

    // Resets the ball's position, processing, and movement direction.
    public void ResetBall()
    {
        // Makes the ball visible.
        Show();
        // Enables regular processing for the ball.
        SetProcess(true);
        // Enables physics processing for the ball.
        SetPhysicsProcess(true);
        // Moves the ball to the center of the viewport.
        Position = GetViewportRect().Size / 2;

        // Creates a random number generator for the launch direction.
        RandomNumberGenerator rng = new RandomNumberGenerator();
        // Seeds the random number generator with a changing value.
        rng.Randomize();

        // Chooses whether the ball initially travels left or right.
        float xDir = rng.Randf() > 0.5f ? 1.0f : -1.0f;
        // Chooses a random vertical direction within a limited range.
        float yDir = rng.RandfRange(-0.8f, 0.8f);

        // Normalizes the direction and scales it to the base speed.
        velocity = new Vector2(xDir, yDir).Normalized() * BaseSpeed;
    }
}
