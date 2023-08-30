using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    public static ScenesManager Instance;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this);
    }
    
    public enum Scene
    {
        QuizScene,
        BasketballMinigameScene
    }

    public void LoadScene(Scene scene)
    {
        SceneManager.LoadScene(scene.ToString());
    }
   
    public void LoadQuizScene()
    {
        SceneManager.LoadScene(Scene.QuizScene.ToString());
    }
    public void LoadBasketballMinigameScene()
    {

        SceneManager.LoadScene(Scene.BasketballMinigameScene.ToString());

    }
}
