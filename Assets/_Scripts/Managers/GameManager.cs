using UnityEngine;

public class GameManager : MonoBehaviour
{
    // References to the scriptable objects
    public ScoringManager scoringManager;
    public TurnManager turnManager;

    // Instance of the QuizManager
    private QuizManager quizManager;
    private IMiniGameManager currentMiniGameManager;

    private void Start()
    {
        quizManager = FindObjectOfType<QuizManager>();
        SetCurrentMiniGameManager(FindObjectOfType<BasketballManager>());
        StartCurrentMiniGame();
        // Subscribe to the quiz finished event
        //quizManager.OnQuizFinished += HandleQuizFinished; TODO BATU
    }

    public void SetCurrentMiniGameManager(IMiniGameManager miniGameManager)
    {
        currentMiniGameManager = miniGameManager;
        currentMiniGameManager.Initialize(scoringManager, turnManager);
    }
    public void StartCurrentMiniGame()
    {
        currentMiniGameManager?.StartGame();
    }

    public void EndCurrentMiniGame()
    {
        currentMiniGameManager?.EndGame();
    }
    private void HandleQuizFinished(bool isCorrect)
    {
        if (isCorrect)
        {
            // Add points to the current player's score
            UpdateScore(turnManager.GetCurrentPlayerName(), 1); 
        }

        // End the current player's turn
        EndTurn();
    }

    // Start a new game or round
    public void StartNewRound()
    {
        scoringManager.ResetScores();
        turnManager.ResetTurn();
        // multiplayer stuff?
    }

    // End the current player's turn
    public void EndTurn()
    {
        turnManager.SwitchTurn();
        // add stuff
    }

    // Get the current player's name
    public string GetCurrentPlayerName()
    {
        return turnManager.GetCurrentPlayerName();
    }

    // Update the score for a player
    public void UpdateScore(string playerName, int points)
    {
        scoringManager.AddPoints(playerName, points);
    }

    private void OnDestroy()
    {
       // quizManager.OnQuizFinished -= HandleQuizFinished; TODO BATU
    }
}
