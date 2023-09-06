using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EstimationPanelScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;

    public static int value { get; set; }
    
    // Start is called before the first frame update
    void Start()
    {
        text.text = "";
        NumpadScript.numberPressed += ChangePanelText;
        NumpadScript.deletionPressed += DeleteText;

    }

    
    void ChangePanelText(TextMeshProUGUI txt)
    {
        if (text.text.Length > 0)
        {
            
            text.text = text.text + txt.text.Trim();
            value = int.Parse(text.text);

        }
        else
        {
            if (txt.text.Trim() != "0")
            {
                text.text = text.text + txt.text.Trim();
                value = int.Parse(text.text);

            }
        }
    }

    void DeleteText()
    {
        if (!string.IsNullOrEmpty(text.text))
        {
            text.text = text.text.Substring(0, text.text.Length - 1);
            value = value - int.Parse(text.text);
        }
        
    }
    
    
    // Update is called once per frame
   
}