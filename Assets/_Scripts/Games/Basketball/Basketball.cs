using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basketball : MonoBehaviour, IShootable
{
    public BasketballManager basketballManager;

    private Rigidbody2D rb;
    private Vector2 initialPosition;
    public ShotState currentState = ShotState.NotShot;
    private bool hitGround;
    private bool scored;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        initialPosition = transform.position;

        // Freeze the Rigidbody to prevent any physics interactions
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        hitGround = false;
        scored = false;
    }
    private void Start()
    {
        basketballManager = FindObjectOfType<BasketballManager>();
        basketballManager.OnScoreBasket += HandleScoreBasket;
        basketballManager.OnFailShot += HandleFailShot;
    }
    private void HandleScoreBasket()
    {
        // Code to run when a basket is scored
        Debug.Log("Scored a basket!");
    }

    private void HandleFailShot()
    {
        // Code to run when a shot fails
        Debug.Log("Failed a shot!");
    }
    public void Shoot(Vector2 force)
    {
        // Unfreeze the Rigidbody to allow physics interactions
        rb.constraints = RigidbodyConstraints2D.None;

        rb.AddForce(force);
        currentState = ShotState.InPlay;
    }

    public void ResetBall(ref bool hasShot)
    {
        hasShot = false;
        StartCoroutine(ResetBall2());

    }
    private IEnumerator ResetBall2()
    {
        yield return new WaitForSeconds(2);
        transform.position = initialPosition;
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        hitGround = false;
        scored = false;
        currentState = ShotState.NotShot;
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (currentState == ShotState.InPlay && collision.gameObject.CompareTag("Ground"))
        {
            currentState = ShotState.Failed;
            hitGround = true;
        }
    }
    public void Score()
    {
        if (currentState == ShotState.Scored)
        {
            currentState = ShotState.Scored;
            basketballManager.ScoreBasket();
            scored = true;
        }
    }
    public bool HasHitGround()
    {
        return hitGround;
    }
    public bool HasScored()
    {
        return scored;
    }

    private void OnDestroy()
    {
        basketballManager.OnScoreBasket -= HandleScoreBasket;
        basketballManager.OnFailShot -= HandleFailShot;
    }
}
