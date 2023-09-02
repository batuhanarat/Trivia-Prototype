using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Hoop : MonoBehaviour
{
    public BasketballManager basketballManager; // A public variable to set the BasketballManager
    public AudioClip swiftyEffect;

    private void Start()
    {
        // If the basketballManager is not set in the inspector, find it
        if (basketballManager == null)
        {
            basketballManager = FindObjectOfType<BasketballManager>();
        }
    }

    

  
}
