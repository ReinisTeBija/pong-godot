// Imports Godot classes used by this script.
using Godot;
// Imports standard .NET functionality.
using System;

// Declares the Main class as a 2D Godot scene node.
public partial class Main : Node2D
{
    // Stores the score for the left player.
    private int scoreLeft = 0;
    // Stores the score for the right player.
    private int scoreRight = 0;

    // References the score label assigned in the Godot editor.
    [Export] public Label ScoreLabel;
    // References the ball assigned in the Godot editor.
    [Export] public Ball BallNode;

    // Runs once when the main scene enters the tree.
    public override void _Ready()
    {
        // Displays the starting score.
        UpdateScoreLabel();
    }

    // Updates the score after a player scores.
    public void OnScore(bool leftPlayer)
    {
        // Checks whether the left player scored.
        if (leftPlayer)
        {
            // Increases the left player's score by one.
            scoreLeft++;
        }
        else
        {
            // Increases the right player's score by one.
            scoreRight++;
        }

        // Refreshes the score text after the score changes.
        UpdateScoreLabel();

        // Pārbauda, vai kāds ir sasniedzis 5 punktus
        // Checks whether either player has reached 5 points.
        if (scoreLeft >= 5 || scoreRight >= 5)
        {
            // Chooses the winner's name based on which score reached 5.
            string winner = scoreLeft >= 5 ? "Kreisais spēlētājs" : "Labais spēlētājs";
            // Displays a message announcing the winner.
            ScoreLabel.Text = $"{winner} uzvarēja!";
            // Stops the ball's regular processing.
            BallNode.SetProcess(false);
            // Stops the ball's physics processing.
            BallNode.SetPhysicsProcess(false);
            // Hides the ball after the game ends.
            BallNode.Hide();
        }
        else
        {
            // Resets the ball for the next round.
            BallNode.ResetBall();
        }
    }

    // Updates the score label with the current scores.
    private void UpdateScoreLabel()
    {
        // Checks whether the score label is assigned.
        if (ScoreLabel != null)
        {
            // Displays the left and right scores.
            ScoreLabel.Text = $"{scoreLeft} : {scoreRight}";
        }
    }
}


