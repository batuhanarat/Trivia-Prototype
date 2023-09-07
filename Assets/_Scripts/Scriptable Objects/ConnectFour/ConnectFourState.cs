using UnityEngine;

[CreateAssetMenu(fileName = "GameState", menuName = "Connect4/GameState")]
public class ConnectFourState : ScriptableObject
{
    public enum State { Ongoing, Win, Draw, Lose }
    public State currentState;
    // Initialization and reset logic
}