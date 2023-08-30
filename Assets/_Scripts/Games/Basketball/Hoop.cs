using UnityEngine;
using UnityEngine.SceneManagement;

public class Hoop : MonoBehaviour
{
    public BasketballManager basketballManager; // A public variable to set the BasketballManager

    private void Start()
    {
        // If the basketballManager is not set in the inspector, find it
        if (basketballManager == null)
        {
            basketballManager = FindObjectOfType<BasketballManager>();
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Basketball"))
        {
            Rigidbody2D ballRigidbody = other.GetComponent<Rigidbody2D>();

            if (ballRigidbody.velocity.y < 0)
            {
                basketballManager.ScoreBasket();
            }
        }
    }
}
