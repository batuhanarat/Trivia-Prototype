using UnityEngine;

public class Hoop : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Basketball"))
        {
            BasketballManager.Instance.ScoreBasket();
            
        }
    }
}
