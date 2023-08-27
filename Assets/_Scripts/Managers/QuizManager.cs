using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizManager : MonoBehaviour
{
    [SerializeField] private QuizUI quizGameUI;
    [SerializeField] private List<QuizDataSO> quizDataList;
    [SerializeField] private float timeInSeconds;


    
    private int correctAnswerCount = 0;
    private List<Question> questions;
    private Question selectedQuestion = new Question();
    private int gameScore;
    private float currentTime;
    private QuizDataSO dataScriptable;
    
    private GameStatus gameStatus = GameStatus.NEXT;
    public GameStatus GameStatus { get { return gameStatus; } }
    public List<QuizDataSO> QuizData { get => quizDataList; }
    
    public void Start()
    {
        correctAnswerCount = 0;
        gameScore = 0;
        currentTime = timeInSeconds;
        
        //set the questions data
        questions = new List<Question>();
        dataScriptable = quizDataList[0];
        questions.AddRange(dataScriptable.questions);
        
        //select the question
        SelectQuestion();
        gameStatus = GameStatus.PLAYING;
    }
    
    /// <summary>
    /// Method used to randomly select the question form questions data
    /// </summary>
    private void SelectQuestion()
    {
        //get the random number
        int val = UnityEngine.Random.Range(0, questions.Count);
        //set the selectedQuetion
        selectedQuestion = questions[val];
        //send the question to quizGameUI
        quizGameUI.SetQuestion(selectedQuestion);

        questions.RemoveAt(val);
    }
    public bool Answer(int selectedOptionIndex)
    {
        //set default to false
        bool correct = false;
        //if selected answer is similar to the correctAns
        if (selectedQuestion.correctAnsIndex == selectedOptionIndex)
        {
            //Yes, Ans is correct
            correctAnswerCount++;
            correct = true;
            gameScore += 1;
            quizGameUI.ScoreText.text = "Score:" + gameScore +"/10";
        }
        else
        {
            //No, Ans is wrong
        }

        if (gameStatus == GameStatus.PLAYING)
        {
            if (questions.Count > 0)
            {
                //call SelectQuestion method again after 1s
                Invoke("SelectQuestion", 0.4f);
            }
            else
            {
                GameEnd();
            }
        }
        //return the value of correct bool
        return correct;
    }
    private void GameEnd()
    {
        gameStatus = GameStatus.NEXT;
       // quizGameUI.GameOverPanel.SetActive(true);


    }
    
    

}
