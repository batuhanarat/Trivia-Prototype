using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts.Scriptable_Objects;
using Unity.Netcode;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class QuizManager : NetworkBehaviour
{
    [SerializeField] private QuizUI quizGameUI;
    [SerializeField] private SoundManager _soundManager;
    [SerializeField] private List<QuizDataSO> quizDataList;
    [SerializeField] private float timeInSeconds;
    [SerializeField] private AudioClip correctAnswerSound, wrongAnswerSound ;
    [SerializeField] private PlayerStats _stats;
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
    
    private void Update()
    {
        if (gameStatus == GameStatus.PLAYING)
        {
            currentTime -= Time.deltaTime;
            SetTime(currentTime);
        }
    }

    public override void OnNetworkSpawn()
    {
        if (!IsOwner)
        {
            Destroy(this);
        }
        
    }


    void SetTime(float value)
    {
        TimeSpan time = TimeSpan.FromSeconds(currentTime); //set the time value
        quizGameUI.TimerText.text = time.ToString("ss");   //convert time to Time format
     
        if (currentTime <= 0)
        {
            //Game Over
            GameEnd();
        }
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
            SoundManager.Instance.PlayAudioEffect(correctAnswerSound);

            //Yes, Ans is correct
            correctAnswerCount++;
            correct = true;
            gameScore += 1;
            quizGameUI.ScoreText.text = "Score: " +gameScore.ToString();
        }
        else
        {
            SoundManager.Instance.PlayAudioEffect(wrongAnswerSound);
            //No, Ans is wrong
            
            //logic for wait to other player in order to move the next question
        }

        if (gameStatus == GameStatus.PLAYING)
        {
            if (questions.Count > 0)
                //control the logic to see other player is ready
            {
                
                //call SelectQuestion method again after 1s
                Invoke("SelectQuestion", 0.4f);
                currentTime = timeInSeconds;
            }
            else
            {
                _stats.setQuizScore(gameScore);
                SoundManager.Instance.MuteTickTock();

                GameEnd();
            }
        }
        //return the value of correct bool
        return correct;
    }
    private void GameEnd()
    {
        gameStatus = GameStatus.NEXT;
        quizGameUI.QuizOverMenu.SetActive(true);
        


    }
    
    

}
