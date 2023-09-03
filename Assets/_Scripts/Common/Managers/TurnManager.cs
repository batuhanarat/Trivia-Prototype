using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

[CreateAssetMenu(fileName = "TurnManager", menuName = "Managers/Turn")]
public class TurnManager : ScriptableObject
{
    public Player player1;
    public Player player2;
    private int currentPlayerIndex = 0;  // 0 indicates player1's turn, 1 indicates player2's turn

    public string GetCurrentPlayerName()
    {
        return (currentPlayerIndex == 0) ? player1.playerName : player2.playerName;
    }

    public void SwitchTurn()
    {
        currentPlayerIndex = 1 - currentPlayerIndex;  // Toggle between 0 and 1
    }

    public void ResetTurn()
    {
        currentPlayerIndex = 0;  // Set to player1's turn
    }

}
