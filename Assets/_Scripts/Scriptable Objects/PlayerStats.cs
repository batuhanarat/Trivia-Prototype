using Unity.VisualScripting;
using UnityEngine;

namespace _Scripts.Scriptable_Objects
{
    [CreateAssetMenu(menuName = "Stats", fileName = "Stats" )]
    public class PlayerStats : ScriptableObject
    {

        [SerializeField] public int quizScore = 0;
        [SerializeField] public int gameScore = 0;


        void Start()
        {
            quizScore = 0;
            
        }

        public void setQuizScore(int _Score)
        {
            quizScore = _Score;
        }
        public int getQuizScore()
        {
            return quizScore;
        }
        
        public void setGameScore(int _Score)
        {
            gameScore = _Score;
        }
        public int getGameScore()
        {
            return gameScore;
        }
    }
}