using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "QuizData" , fileName = "QuizData")]
    public class QuizDataSO : ScriptableObject
    {
        [SerializeField] public List<Question> questions;
    }
