using Godot;
using System;

public partial class Ball : CharacterBody2D
{
    private Vector2 velocity;
    [Export] public float BaseSpeed = 420.0f;
    [Export] public Main GameController;

    public override void _Ready()
    {
        ResetBall();
    }

    public override void _PhysicsProcess(double delta)
    {
        KinematicCollision2D collision = MoveAndCollide(velocity * (float)delta);

        if (collision != null)
        {
            velocity = velocity.Bounce(collision.GetNormal());
        }

        // Pārbauda, vai bumba izlidojusi ārpus ekrāna kreisajā vai labajā pusē
        if (Position.X < 0)
        {
            // Bumba izlidoja pa kreisi -> punktu saņem labais spēlētājs
            if (GameController != null)
            {
                GameController.OnScore(false);
            }
            else
            {
                ResetBall();
            }
        }
        else if (Position.X > 1280)
        {
            // Bumba izlidoja pa labi -> punktu saņem kreisais spēlētājs
            if (GameController != null)
            {
                GameController.OnScore(true);
            }
            else
            {
                ResetBall();
            }
        }
    }

    public void ResetBall()
    {
        Show();
        SetProcess(true);
        SetPhysicsProcess(true);
        Position = GetViewportRect().Size / 2;

        RandomNumberGenerator rng = new RandomNumberGenerator();
        rng.Randomize();

        float xDir = rng.Randf() > 0.5f ? 1.0f : -1.0f;
        float yDir = rng.RandfRange(-0.8f, 0.8f);

        velocity = new Vector2(xDir, yDir).Normalized() * BaseSpeed;
    }
}
