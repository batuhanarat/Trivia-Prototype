using UnityEngine;

public class BasketballManager : MonoBehaviour
{
    public static BasketballManager Instance; // Singleton instance

    public ScoringManager scoringManager; // Reference to ScoringManager ScriptableObject
    public TurnManager turnManager; // Reference to TurnManager ScriptableObject

    private void Awake()
    {
        // Singleton setup
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // This method is called when a basket is scored
    public void ScoreBasket()
    {
        string currentPlayer = turnManager.GetCurrentPlayerName();
        scoringManager.AddPoints(currentPlayer, 1); // Assuming 1 point per basket
    }
}
