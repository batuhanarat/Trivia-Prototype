using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    public static ScenesManager Instance;
    public Animator playButtonAnimator;
    public Animator textAnimator;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this);
    }
    
    public enum Scene
    {
        QuizScene,
        BasketballMinigameScene,
        ConnectFourMinigameScene
    }

    public void LoadScene(Scene scene)
    {
        SceneManager.LoadScene(scene.ToString());
    }
   
    public void LoadQuizScene()
    {
        SceneManager.LoadScene(Scene.QuizScene.ToString());
    }

    public void LoadBasketballGame()
    {
        StartCoroutine(BasketballMinigameSceneCourotine());
    }
    public void LoadConnectFourMinigameScene()
    {
        StartCoroutine(ConnectFourMinigameSceneCourotine());
    }
    private IEnumerator BasketballMinigameSceneCourotine()
    {
        textAnimator.SetTrigger("appear");
        playButtonAnimator.SetTrigger("isTrigger");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(Scene.BasketballMinigameScene.ToString());
    }
    private IEnumerator ConnectFourMinigameSceneCourotine()
    {
        textAnimator.SetTrigger("appear");
        playButtonAnimator.SetTrigger("isTrigger");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(Scene.ConnectFourMinigameScene.ToString());
    }



}
