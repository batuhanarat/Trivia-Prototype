using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Question
{
    public string questionInfo;         //question text
    public Sprite questionImage;        //image for Image Type
    public QuestionType questionType;   //question type
    public AudioClip audioClip;         //audio for audio type
    public List<string> options;        //options to select
    public int correctAnsIndex;         //correct option index
}
