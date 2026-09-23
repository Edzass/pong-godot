using Godot;

public partial class Game : Node2D
{
	private int scoreLeft = 0;
	private int scoreRight = 0;
	private bool gameStarted;
	private bool paused;
	private Node2D playField;
	private Paddle leftPaddle;
	private Paddle rightPaddle;

	[Export] public Ball BallNode;
	[Export] public Label ScoreLabel;
	[Export] public Label WinnerLabel;
	[Export] public Button StartButton;
	[Export] public Button ResetButton;
	[Export] public Button PauseButton;
	[Export] public OptionButton DifficultyOption;
	[Export] public Control StartScreen;
	[Export] public Label StartTitle;
	[Export] public Label PauseLabel;

	public override void _Ready()
	{
		BallNode ??= GetNode<Ball>("Speles/Bumba");
		ScoreLabel ??= GetNode<Label>("UI/Punkti");
		WinnerLabel ??= GetNodeOrNull<Label>("UI/Uzvaretajs");
		StartButton ??= GetNode<Button>("UI/SakumaEkrans/Sakt");
		ResetButton ??= GetNode<Button>("UI/Reset");
		PauseButton ??= GetNode<Button>("UI/Pause");
		DifficultyOption ??= GetNode<OptionButton>("UI/SakumaEkrans/Grutiba");
		StartScreen ??= GetNode<Control>("UI/SakumaEkrans");
		StartTitle ??= GetNode<Label>("UI/SakumaEkrans/Virsraksts");
		PauseLabel ??= GetNode<Label>("UI/PauzeTeksts");
		playField = GetNode<Node2D>("Speles");
		leftPaddle = GetNode<Paddle>("Speles/PaddleLeft");
		rightPaddle = GetNode<Paddle>("Speles/PaddleRight");

		StartButton.Pressed += StartGame;
		ResetButton.Pressed += ResetGame;
		PauseButton.Pressed += TogglePause;

		if (BallNode != null)
		{
			BallNode.GameRef = this;
			BallNode.StopBall();
		}

		if (WinnerLabel != null)
		{
			WinnerLabel.Text = "";
		}

		playField.ProcessMode = ProcessModeEnum.Disabled;
		ResetButton.Visible = false;
		PauseButton.Visible = false;
		PauseLabel.Visible = false;
		UpdateScoreUI();
	}

	private void StartGame()
	{
		scoreLeft = 0;
		scoreRight = 0;
		gameStarted = true;
		paused = false;
		StartScreen.Visible = false;
		ResetButton.Visible = true;
		PauseButton.Visible = true;
		PauseLabel.Visible = false;
		StartButton.Text = "Sākt spēli";
		WinnerLabel.Text = "";
		playField.ProcessMode = ProcessModeEnum.Inherit;
		ApplyDifficulty();
		BallNode.ResetBall();
		UpdateScoreUI();
	}

	private void ResetGame()
	{
		StartGame();
	}

	private void TogglePause()
	{
		if (!gameStarted)
		{
			return;
		}

		paused = !paused;
		playField.ProcessMode = paused
			? ProcessModeEnum.Disabled
			: ProcessModeEnum.Inherit;
		PauseButton.Text = paused ? "Turpināt" : "Pauze";
		PauseLabel.Text = paused ? "PAUZE" : "";
	}

	private void ApplyDifficulty()
	{
		float speed = DifficultyOption.Selected switch
		{
			0 => 180.0f,
			2 => 330.0f,
			_ => 250.0f
		};

		BallNode.BaseSpeed = speed;
		leftPaddle.Speed = speed;
		rightPaddle.Speed = speed;
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
			EndGame("Kreisais spēlētājs uzvarēja!", true);
		}
		else if (scoreRight >= 5)
		{
			EndGame("Labais spēlētājs uzvarēja!", false);
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

	private void EndGame(string winnerMessage, bool leftPlayer)
	{
		gameStarted = false;
		playField.ProcessMode = ProcessModeEnum.Disabled;
		ResetButton.Visible = false;
		PauseButton.Visible = false;
		PauseLabel.Visible = false;
		StartScreen.Visible = true;
		StartTitle.Text = "Spēle beigusies";
		StartButton.Text = "Spēlēt vēlreiz";

		if (WinnerLabel != null)
		{
			WinnerLabel.Text = winnerMessage;
			WinnerLabel.Position = new Vector2(leftPlayer ? 35.0f : 720.0f, 130.0f);
			WinnerLabel.Size = new Vector2(500.0f, 60.0f);
			WinnerLabel.HorizontalAlignment = leftPlayer
				? HorizontalAlignment.Left
				: HorizontalAlignment.Right;
		}

		BallNode?.StopBall();
	}
}
