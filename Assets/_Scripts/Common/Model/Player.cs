using UnityEngine;

[System.Serializable]
public class Player
{
    [SerializeField]
    private string _playerName;
    public string playerName
    {
        get { return _playerName; }
        set { _playerName = value; }
    }

    [SerializeField]
    private int _points;         // Serialized field for points
    public int points
    {
        get { return _points; }
        private set { _points = value; }
    }

    public Player(string name)
    {
        _playerName = name;
        _points = 0;
    }

    public void AddPoints(int pointsToAdd)
    {
        _points += pointsToAdd;
    }

    public int GetPoints()
    {
        return _points;
    }
    public void ResetScore()
    {
        _points = 0;
    }
}
