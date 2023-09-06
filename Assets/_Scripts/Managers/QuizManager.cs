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
        NumpadScript.estimated += Estimate;

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

    public void Estimate(int value)
    {
        bool correct = false;
        double successRate = CalculateSuccessRate(value);
        double fakeSuccessRateofOpponent = 45.0;

        if (successRate > fakeSuccessRateofOpponent)
        {
            correct = true;
           
        } 
        correctAnswerCount++;
        StartCoroutine(PlayWithFakeOpponent(correct,successRate,fakeSuccessRateofOpponent, (double)selectedQuestion.correctAnsIndex));

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

    }

    public double CalculateSuccessRate(int value)
    {
        double answer = (double)value;
        double correctAnswer = (double) selectedQuestion.correctAnsIndex;
        double failRate = (Math.Abs(answer - correctAnswer) / correctAnswer) * 100.0;
        double successRate = 100.0 - failRate;
        return successRate;
    }

    public IEnumerator PlayWithFakeOpponent(bool correct,double own, double opponent,double correctAnswer)
    {
        DeactivateTimer();
        quizGameUI.ActivatePlayerWait();
        yield return new WaitForSeconds(2.0f);
        quizGameUI.ShowStatistics(correct, own,opponent,correctAnswer);


    }
    

    public void DeactivateTimer()
    {
        gameStatus = GameStatus.WAITING;
        SoundManager.Instance.MuteTickTock();
        
    }

    public void ReActivateTimer()
    {
        gameStatus = GameStatus.WAITING;
        SoundManager.Instance.UnMuteTickTock();


        
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
