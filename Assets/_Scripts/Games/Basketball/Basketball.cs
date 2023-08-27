using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basketball : MonoBehaviour, IShootable
{
    private Rigidbody2D rb;
    private Vector2 initialPosition;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        initialPosition = transform.position;

        // Freeze the Rigidbody to prevent any physics interactions
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    public void Shoot(Vector2 force)
    {
        // Unfreeze the Rigidbody to allow physics interactions
        rb.constraints = RigidbodyConstraints2D.None;

        rb.AddForce(force);
    }

    public void ResetBall(ref bool hasShot)
    {
        transform.position = initialPosition;
        rb.velocity = Vector2.zero; 
        rb.angularVelocity = 0f; 
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        hasShot = false;
    }
}
