using Godot;

public partial class Game : Node2D
{
	private int scoreLeft = 0;
	private int scoreRight = 0;

	[Export] public Ball BallNode;
	[Export] public Label ScoreLabel;
	[Export] public Label WinnerLabel;

	public override void _Ready()
	{
		BallNode ??= GetNode<Ball>("Speles/Bumba");
		ScoreLabel ??= GetNode<Label>("UI/Punkti");
		WinnerLabel ??= GetNodeOrNull<Label>("UI/Uzvaretajs");

		if (BallNode != null)
		{
			BallNode.GameRef = this;
		}

		if (WinnerLabel != null)
		{
			WinnerLabel.Text = "";
		}

		UpdateScoreUI();
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

		UpdateScoreUI();

		if (scoreLeft >= 5)
		{
			EndGame("Kreisais spēlētājs uzvarēja!");
		}
		else if (scoreRight >= 5)
		{
			EndGame("Labais spēlētājs uzvarēja!");
		}
		else
		{
			BallNode?.ResetBall();
		}
	}

	private void UpdateScoreUI()
	{
		if (ScoreLabel != null)
		{
			ScoreLabel.Text = $"{scoreLeft} : {scoreRight}";
		}
	}

	private void EndGame(string winnerMessage)
	{
		if (WinnerLabel != null)
		{
			WinnerLabel.Text = winnerMessage;
		}

		BallNode?.StopBall();
	}
}
