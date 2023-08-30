using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button readyButtonPlayer1, readyButtonPlayer2;
    [SerializeField] private Button playButton;

    private bool isPlayer1Ready = false;
    private bool isPlayer2Ready = false;

    // Start is called before the first frame update
    void Start()
    {
        // Disable the guest's ready button initially
        //readyButtonPlayer1.interactable = NetworkManager.Singleton.IsServer;
        //readyButtonPlayer2.interactable = !NetworkManager.Singleton.IsServer;

        readyButtonPlayer1.onClick.AddListener(() => MakeReady1());
        readyButtonPlayer2.onClick.AddListener(() => MakeReady2());
        playButton.onClick.AddListener(() => CanGameStart());
    }

    void MakeReady1()
    {
        if (!NetworkManager.Singleton.IsServer) return;

        if (!isPlayer1Ready)
        {
            isPlayer1Ready = true;
            readyButtonPlayer1.image.color = Color.green;

            // Disable the host's ready button once clicked
        }
        else
        {
            isPlayer1Ready = false;
            readyButtonPlayer1.image.color = Color.red;

            // Enable the host's ready button again
        }
    }

    void MakeReady2()
    {
        if (NetworkManager.Singleton.IsServer) return;

        if (!isPlayer2Ready)
        {
            isPlayer2Ready = true;
            readyButtonPlayer2.image.color = Color.green;

            // Disable the guest's ready button once clicked
        }
        else
        {
            isPlayer2Ready = false;
            readyButtonPlayer2.image.color = Color.red;

        }
    }

    void CanGameStart()
    {
        if (isPlayer1Ready && isPlayer2Ready)
        {
            Debug.Log("Game can start");

            // Enable the guest's ready button
        }
    }
}