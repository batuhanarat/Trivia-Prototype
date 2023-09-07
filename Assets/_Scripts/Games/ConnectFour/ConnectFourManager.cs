using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

// How to use: ConnectFourManager connectFourManager = new ConnectFourManager();
// gameManager.SetCurrentMiniGameManager(connectFourManager);

public class ConnectFourManager : MonoBehaviour, IMiniGameManager
{
    public ScoringManager scoringManager;
    public TurnManager turnManager;
    public ConnectFourState connectFourState;
    public GameObject player1DiscPrefab;
    public GameObject player2DiscPrefab;

    public TextMeshProUGUI currentPlayerText;
    private Player currentPlayer;
    private bool isWaitingForPlayerInput = false;

    private GameObject[,] boardVisuals;
    private int[,] board; // 0: empty, 1: player 1, 2: player 2
    private const int rows = 6;
    private const int columns = 7;
    public void Start()
    {
        InitializeBoard();
        connectFourState.currentState = ConnectFourState.State.Ongoing;
    }
    public void PlayRound(Player player)
    {
        currentPlayer = player;
        isWaitingForPlayerInput = true;
    }
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Ended)
            {
                // Perform raycasting to detect clicked GameObject
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    GameObject touchedObject = hit.transform.gameObject;

                    // Check if the touched GameObject represents a column
                    if (touchedObject.tag == "Column")
                    {
                        int column = int.Parse(touchedObject.name.Replace("Column_", ""));
                        OnColumnClick(column);
                    }
                }
            }
        }
    }
    private void OnColumnClick(int column)
    {
        // Check whose turn it is
        int currentPlayerIndex = turnManager.GetCurrentPlayerIndex() + 1;

        if (DropDisc(column, currentPlayerIndex))
        {
            UpdateBoardVisuals(column);
            if (CheckWin(currentPlayerIndex))
            {
                Debug.Log("Player " + currentPlayerIndex + " wins!");
                connectFourState.currentState = ConnectFourState.State.Win;
            }
            else
            {
                // Quiz gateway
                
            }
        }
        else
        {
            Debug.Log("Column full!");
        }
    }
    public void Initialize(ScoringManager scoringManager, TurnManager turnManager)
    {
        this.scoringManager = scoringManager;
        this.turnManager = turnManager;
    }

    public void StartGame()
    {
        InitializeBoard();
        turnManager.ResetTurn();
        connectFourState.currentState = ConnectFourState.State.Ongoing;
    }

    public void EndGame()
    {
        turnManager.ResetTurn();
    }

    public void DropDisc(int column)
    {
        var currentPlayerIndex = turnManager.GetCurrentPlayerIndex() + 1;
        if(DropDisc(column, currentPlayerIndex))
        {
            turnManager.SwitchTurn();
        }
        else
        {
            Debug.Log("oraya giremen aga");
        }
    }
    public void UpdateUI()
    {
        string currentPlayer = turnManager.GetCurrentPlayerName();
        currentPlayerText.text = "Current Player: " + currentPlayer;
    }
    private bool DropDisc(int column, int player)
    {
        for (int row = 5; row >= 0; row--) 
        {
            if (board[column, row] == 0) 
            {
                board[column, row] = player;
                return true;
            }
        }
        return false; 
    }

    private bool CheckWin(int player) //TO DO: vertical and diagonaly checks
    {
        for (int row = 0; row < 6; row++) // horizantal
        {
            for (int col = 0; col < 4; col++) // Only go up to the 4th column to avoid out-of-bounds
            {
                if (board[col, row] == player &&
                    board[col + 1, row] == player &&
                    board[col + 2, row] == player &&
                    board[col + 3, row] == player)
                {
                    return true; 
                }
            }
        }
        return false;
    }

    private void InitializeBoard()
    {
        board = new int[rows, columns];
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                board[i, j] = 0;
            }
        }

    }
    private void UpdateBoardVisuals(int column)
    {
        for (int row = 0; row < 6; row++)
        {
            int player = board[column, row];
            GameObject discPrefab = (player == 1) ? player1DiscPrefab : player2DiscPrefab;

            // Create new disc in this cell
            if (player != 0)
            {
                boardVisuals[column, row] = Instantiate(discPrefab, new Vector3(column, 1.5f, 0), Quaternion.identity);
            }
        }
    }

}
