using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "ScoringManager", menuName = "Managers/Scoring")]
public class ScoringManager : ScriptableObject
{
    public List<Player> players = new List<Player>();

    // Add points to a player's score
    public void AddPoints(string playerName, int points)
    {
        Player player = players.Find(p => p.playerName == playerName);
        if (player != null)
        {
            player.AddPoints(points);
        }
    }

    // Get a player's score
    public int GetPlayerScore(string playerName)
    {
        Player player = players.Find(p => p.playerName == playerName);
        return player?.GetPoints() ?? 0;
    }

    // Reset all player scores
    public void ResetScores()
    {
        foreach (var player in players)
        {
            player.ResetScore();
        }
    }
}

