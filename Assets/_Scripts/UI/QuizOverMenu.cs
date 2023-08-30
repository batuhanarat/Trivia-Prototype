using System.Collections;
using System.Collections.Generic;
using _Scripts.Scriptable_Objects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuizOverMenu : MonoBehaviour
{

    [SerializeField] private TMP_Text text;
    [SerializeField] private PlayerStats _stats;
    [SerializeField] private Button playButton;
    
    // Start is called before the first frame update
    void Start()
    {
        text.text = "You have " + _stats.getQuizScore().ToString() + " shots";
        playButton.onClick.AddListener(() => OpenBasketScene());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OpenBasketScene()
    {
        ScenesManager.Instance.LoadBasketballMinigameScene();
    }
    
    
}
