using Godot;
using System;

public partial class Main : Node2D
{
    private int scoreLeft = 0;
    private int scoreRight = 0;

    [Export] public Label ScoreLabel;
    [Export] public Ball BallNode;

    public override void _Ready()
    {
        UpdateScoreLabel();
    }

    public void OnScore(bool leftPlayer)
    {
        if (leftPlayer)
        {
            scoreLeft++;
        }
        else
        {
            scoreRight++;
        }

        UpdateScoreLabel();

        // Pārbauda, vai kāds ir sasniedzis 5 punktus
        if (scoreLeft >= 5 || scoreRight >= 5)
        {
            string winner = scoreLeft >= 5 ? "Kreisais spēlētājs" : "Labais spēlētājs";
            ScoreLabel.Text = $"{winner} uzvarēja!";
            BallNode.SetProcess(false);
            BallNode.SetPhysicsProcess(false);
            BallNode.Hide();
        }
        else
        {
            BallNode.ResetBall();
        }
    }

    private void UpdateScoreLabel()
    {
        if (ScoreLabel != null)
        {
            ScoreLabel.Text = $"{scoreLeft} : {scoreRight}";
        }
    }
}


