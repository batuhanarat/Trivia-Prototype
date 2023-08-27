using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuizUI : MonoBehaviour
{
   
    [SerializeField] private QuizManager quizManager;               //ref to the QuizManager script
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private GameObject  gamePanel;
    [SerializeField] private Color correctCol, wrongCol, normalCol; //color of buttons
    [SerializeField] private Image questionImg;                     //image component to show image
  //  [SerializeField] private AudioSource questionAudio;             //audio source for audio clip
    [SerializeField] private TMP_Text questionInfoText;                 //text to show question
    [SerializeField] private List<Button> options;                  //options button reference
    
    
    private Question question;          //store current question data
    private bool answered = false;      //bool to keep track if answered or not

   // public TMP_Text TimerText { get => timerText; }                     //getter
    public TMP_Text ScoreText { get => scoreText; }                     //getter
    
    //public GameObject GameOverPanel { get => gameOverPanel; }       //getter

    
    private void Start()
    {
        //add the listner to all the buttons
        for (int i = 0; i < options.Count; i++)
        {
            Button localBtn = options[i];
            localBtn.onClick.AddListener(() => OnClick(localBtn));
        }
    }
     public void SetQuestion(Question question)
    {
        //set the question
        this.question = question;
        //check for questionType
        switch (question.questionType)
        {
            case QuestionType.TEXT:
                questionImg.transform.parent.gameObject.SetActive(false);   //deactivate image holder
                break;
            case QuestionType.IMAGE:
                questionImg.transform.parent.gameObject.SetActive(true);    //activate image holder
 //               questionImg.transform.gameObject.SetActive(true);           //activate questionImg
                questionImg.sprite = question.questionImage;                //set the image sprite
                break;
           
        }

        questionInfoText.text = question.questionInfo;                      //set the question text

        //suffle the list of options
      //  List<string> ansOptions = ShuffleList.ShuffleListItems<string>(question.options);

        //assign options to respective option buttons
        
        for (int i = 0; i < options.Count; i++)
        {
            //set the child text
            options[i].GetComponentInChildren<TMP_Text>().text = question.options[i];
            options[i].name = i.ToString();    //set the name of button
            options[i].image.color = normalCol; //set color of button to normal
        }

        answered = false;                       

    }

    void OnClick(Button btn)
    {
        if (quizManager.GameStatus == GameStatus.PLAYING)
        {
            //if answered is false
            if (!answered)
            {
                //set answered true
                answered = true;
                //get the bool value
                bool val = quizManager.Answer( int.Parse(btn.name));

                //if its true
                if (val)
                {
                    //set color to correct
                    btn.image.color = correctCol;
                    StartCoroutine(BlinkImg(btn.image));
                }
                else
                {
                    //else set it to wrong color
                    
                    
                    btn.image.color = wrongCol;
                }
            }
        }
    }
    IEnumerator BlinkImg(Image img)
    {
        for (int i = 0; i < 2; i++)
        {
            img.color = Color.white;
            yield return new WaitForSeconds(0.1f);
            img.color = correctCol;
            yield return new WaitForSeconds(0.1f);
        }
    }
    
    
}
