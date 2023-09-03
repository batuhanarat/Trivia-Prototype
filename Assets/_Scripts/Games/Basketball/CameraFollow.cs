using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; 
    public float smoothSpeed = 0.125f; 
    public Vector2 offset; 
    public float centerSpeed = 0.5f; 

    public float minX = -10f;
    public float maxX = 10f;
    public float minY = -10f;
    public float maxY = 10f;

    public bool isFollowing = false; 
    private bool centering = false; 

    private void Start()
    {
        offset = transform.position - target.position;

        Vector2 desiredCenterPosition = new Vector2(0, 0);

        offset = (Vector2)transform.position - desiredCenterPosition;
    }

    private void LateUpdate()
    {
        if (centering)
        {
            // Calculate the desired centered position
            Vector2 desiredCenterPosition = (Vector2)target.position + offset;

            // Interpolate the camera's position towards the desired position
            Vector2 newCameraPosition = Vector2.Lerp(transform.position, desiredCenterPosition, centerSpeed * Time.deltaTime);
            transform.position = new Vector3(newCameraPosition.x, newCameraPosition.y, transform.position.z);

            // If the camera is close enough to the desired position, stop centering
            if (Vector2.Distance(transform.position, desiredCenterPosition) < 0.01f)
            {
                centering = false;
            }

            return;
        }

        if (!isFollowing)
            return;

        // Calculate the desired 2D position based on target position and offset
        Vector2 desiredPosition2D = (Vector2)target.position + offset;

        // Clamp the 2D position within defined boundaries
        desiredPosition2D.x = Mathf.Clamp(desiredPosition2D.x, minX, maxX);
        desiredPosition2D.y = Mathf.Clamp(desiredPosition2D.y, minY, maxY);

        // Calculate the smoothed 2D position using Lerp
        Vector2 smoothedPosition2D = Vector2.Lerp(transform.position, desiredPosition2D, smoothSpeed);

        // Update the camera's position using the smoothed 2D position while maintaining its original Z position
        transform.position = new Vector3(smoothedPosition2D.x, smoothedPosition2D.y, transform.position.z);
    }

    public void StartFollowing()
    {
        isFollowing = true;
    }

    public void StopFollowing()
    {
        isFollowing = false;
    }

    public void CenterBasketball()
    {
        centering = true;
    }
}
