// Scriptable Objects
using UnityEngine;

[CreateAssetMenu(fileName = "BoardState", menuName = "Connect4/BoardState")]
public class BoardState : ScriptableObject
{
    public int[,] grid; // 2D array representing the board
    // Initialization and reset logic
}
