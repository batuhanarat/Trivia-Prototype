using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class NumpadScript : MonoBehaviour
{
   // public static event Action<TextMeshProUGUI> numberPressed;
    public static event Action<TextMeshProUGUI> numberPressed;
    public static event Action deletionPressed;
    public static event Action<int> estimated;

   
    
     
    //public static event Action<TextMeshProUGUI> numberPressed;


    [SerializeField] private Button num0Button;
   
    [SerializeField] private Button num1Button;
    
    [SerializeField] private Button num2Button;
    [SerializeField] private Button num3Button;
    [SerializeField] private Button num4Button;
    [SerializeField] private Button num5Button;
    [SerializeField] private Button num6Button;
    [SerializeField] private Button num7Button;
    [SerializeField] private Button num8Button;
    [SerializeField] private Button num9Button;
    [SerializeField] private Button num000Button;
    [SerializeField] private Button deleteButton;
    [SerializeField] private Button estimateButton;


    

    
    // Start is called before the first frame update
    void Start()
    {
       num0Button.onClick.AddListener(() => numberPressed?.Invoke(num0Button.GetComponentInChildren<TextMeshProUGUI>()));
       num1Button.onClick.AddListener(() => numberPressed?.Invoke(num1Button.GetComponentInChildren<TextMeshProUGUI>()));
       num2Button.onClick.AddListener(() => numberPressed?.Invoke(num2Button.GetComponentInChildren<TextMeshProUGUI>()));
       num3Button.onClick.AddListener(() => numberPressed?.Invoke(num3Button.GetComponentInChildren<TextMeshProUGUI>()));
       num4Button.onClick.AddListener(() => numberPressed?.Invoke(num4Button.GetComponentInChildren<TextMeshProUGUI>()));
       num5Button.onClick.AddListener(() => numberPressed?.Invoke(num5Button.GetComponentInChildren<TextMeshProUGUI>()));
       num6Button.onClick.AddListener(() => numberPressed?.Invoke(num6Button.GetComponentInChildren<TextMeshProUGUI>()));
       num7Button.onClick.AddListener(() => numberPressed?.Invoke(num7Button.GetComponentInChildren<TextMeshProUGUI>()));
       num8Button.onClick.AddListener(() => numberPressed?.Invoke(num8Button.GetComponentInChildren<TextMeshProUGUI>()));
       num9Button.onClick.AddListener(() => numberPressed?.Invoke(num9Button.GetComponentInChildren<TextMeshProUGUI>()));
       num000Button.onClick.AddListener(() => numberPressed?.Invoke(num000Button.GetComponentInChildren<TextMeshProUGUI>()));
      
       
       deleteButton.onClick.AddListener(() => deletionPressed?.Invoke());

       
       
     
        
       estimateButton.onClick.AddListener(() => estimated?.Invoke(EstimationPanelScript.value));

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    
    
}
