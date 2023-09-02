using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoopMove : MonoBehaviour
{
    [SerializeField] private Transform tr;
    [SerializeField] private float speed;

    // Start is called before the first frame update
    void Start()
    {
        tr = gameObject.GetComponent<Transform>();
    }

    // This function reverses the direction of movement
    public void turnAround()
    {
        speed = speed * -1;
    }

    void Update()
    {
        
        Vector3 currentPosition =  tr.position;

        // Calculate the new x-coordinate based on speed and time
        float newXPosition = currentPosition.x + (speed * Time.deltaTime);

        // Create a new Vector3 with the updated x-coordinate
        Vector3 newPosition = new Vector3(newXPosition, currentPosition.y, 0);

        // Update the GameObject's position
        tr.position = newPosition;
       }
}