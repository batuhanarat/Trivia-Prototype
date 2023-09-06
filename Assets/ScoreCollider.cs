using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCollider : MonoBehaviour
{
    [SerializeField] private BasketballManager _basketballManager;
    [SerializeField] private AudioClip swiftyEffect;
    private void OnTriggerEnter2D(Collider2D other)
    {
       
        if (other.CompareTag("Basketball"))
        {
            Rigidbody2D ballRigidbody = other.GetComponent<Rigidbody2D>();

            if (ballRigidbody.velocity.y < 0)
            {
                _basketballManager.ScoreBasket();
                SoundManager.Instance.PlayAudioEffect(swiftyEffect);
                _basketballManager.FireConfetti();
            }
        }
        
        
    }
}
