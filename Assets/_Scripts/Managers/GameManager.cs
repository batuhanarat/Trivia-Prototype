using UnityEngine;

public class GameManager : MonoBehaviour
{
    // References to the scriptable objects
    public ScoringManager scoringManager;
    public TurnManager turnManager;

    // Instance of the QuizManager
    private QuizManager quizManager;

    private void Start()
    {
        quizManager = FindObjectOfType<QuizManager>();

        // Subscribe to the quiz finished event
        //quizManager.OnQuizFinished += HandleQuizFinished; TODO BATU
    }

    private void HandleQuizFinished(bool isCorrect)
    {
        if (isCorrect)
        {
            // Add points to the current player's score
            UpdateScore(turnManager.GetCurrentPlayerName(), 10); // Assuming 10 points for a correct answer
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
        // Unsubscribe from the quiz finished event to avoid memory leaks
       // quizManager.OnQuizFinished -= HandleQuizFinished; TODO BATU
    }
}
