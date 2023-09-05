using _Scripts.Scriptable_Objects;
using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class BasketballManager : MonoBehaviour, IMiniGameManager
{
    [SerializeField] private ParticleSystem confetti;
    public ScoringManager scoringManager; // Reference to ScoringManager 
    public TurnManager turnManager; // Reference to TurnManager 
    public PlayerStats playerStat; // Batu's scriptable object reference
    public Basketball basketball; // Reference to the Basketball object
    public TextMeshProUGUI shotsLeftText;  
    public TextMeshProUGUI shotsMadeText;
    public TextMeshProUGUI gameStatus;
    public TextMeshProUGUI attempText;
    public Animator animator;
    public Color boomColor;
    public Color failColor;
    public GameObject panel;
    public event Action OnScoreBasket;  // Event for a successful shot
    public event Action OnFailShot;     // Event for a failed shot
    private int currentShots;
    private int successfulShots = 0;  // New variable to keep track of successful shots

    private bool hasShot = false; // Whether the player has shot the ball

    public bool GameHasEnded { get; private set; } // To keep track of the game state

    private void Awake()
    {
        //basketball.basketballManager = this;
        GameHasEnded = false;
        confetti.Stop();

    }
    public void Initialize(ScoringManager scoringManager, TurnManager turnManager)
    {
        this.scoringManager = scoringManager;
        this.turnManager = turnManager;
    }
    public void StartGame()
    {
        //currentShots = shotsData.numberOfShots;
        currentShots = playerStat.quizScore;
        GameHasEnded = false;
        UpdateUI();
    }
    // This method is called when a basket is scored
    public void ScoreBasket()
    {
        if (basketball.currentState == ShotState.InPlay)
        {
            basketball.currentState = ShotState.Scored;
            currentShots--;
            successfulShots++;
            basketball.Score();
            //attempText.color = boomColor;

            StartCoroutine(DisplayMessage("Boom!", 0.7f));

            scoringManager.AddPoints(turnManager.GetCurrentPlayerName(), 1);
        //    OnScoreBasket?.Invoke();  // Fire the event
            UpdateUI();
            if (currentShots <= 0)
            {
                EndGame();
            }
        }
    }
    public void ResetShot()
    {
        if (basketball != null)
        {
            basketball.ResetBall(ref hasShot);
        }
    }
    // This method is called when a shot is failed
    public void FailShot()
    {
        if (basketball.currentState == ShotState.Failed)
        {
            basketball.currentState = ShotState.Failed;
            currentShots--;
            //attempText.color = failColor;
            StartCoroutine(DisplayMessage("Failed!", 0.7f));

            UpdateUI();
            if (currentShots <= 0)
            {
                EndGame();
            }
            //OnFailShot?.Invoke();
        }
    }
    // Coroutine to display a message for a specific duration
    private IEnumerator DisplayMessage(string message, float duration)
    {
        // do this through a UI text element

        attempText.text = message;
        
        animator.SetBool("isPopped", true);

        yield return new WaitForSeconds(duration);
        animator.SetBool("isPopped", false);


        // Hide the message
        attempText.text = "";
    }

    // Handle game ending logic

    public void EndGame()
    {
        hasShot = false;
        StartCoroutine(ExecuteEndGame());
    }
    public IEnumerator ExecuteEndGame()
    {
        yield return new WaitForSeconds(0.5f);
        GameHasEnded = true;
        gameStatus.text = "Your Score: " + successfulShots;
        panel.SetActive(true);
        // TO DO: display the final score, trigger animations
        Debug.Log("Game Over");

        // TO DO: Trigger UI
    }

    public void FireConfetti()
    {
        confetti.Play();
    }

        private void UpdateUI()
    {
        shotsLeftText.text = "Shots: " +currentShots;
        shotsMadeText.text = "Score: " + (successfulShots); 
    }
}
