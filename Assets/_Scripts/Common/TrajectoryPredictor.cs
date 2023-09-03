using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UIElements.UxmlAttributeDescription;

[RequireComponent(typeof(LineRenderer))]
public class TrajectoryPredictor : MonoBehaviour
{
    private LineRenderer lineRenderer;

    [SerializeField] private int resolution = 50;
    [SerializeField] private float simulationTime = 0.6f; // Duration to simulate forward
    [SerializeField] private LayerMask layerMask; // Layers that the ball might collide with
    [SerializeField] private float gravityScale = 1.0f;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    public void PredictTrajectory(Vector2 startVelocity)
    {
        List<Vector2> points = new List<Vector2>();
        Vector2 currentPoint = transform.position;
        Vector2 currentVelocity = startVelocity * 0.02f;// this was bullshit addjustments but it works, maybe its about scale of the ball?
        float timeStep = simulationTime / resolution;
        float timeRemaining = simulationTime;

        while (timeRemaining > 0)
        {
            // Calculate the next point
            Vector2 moveDelta = currentVelocity * timeStep + 0.5f * Physics2D.gravity * timeStep * timeStep;
            float rayDistance = moveDelta.magnitude * 0.1f; // this was bullshit addjustments
            RaycastHit2D hit = Physics2D.Raycast(currentPoint, moveDelta.normalized, rayDistance, layerMask);
            if (hit.collider != null)
            {
                currentVelocity = Vector2.Reflect(currentVelocity, hit.normal);
                float timeToCollision = hit.distance / moveDelta.magnitude * timeStep;
                timeStep -= timeToCollision;

                currentPoint = hit.point;
            }
            else
            {
                // If no collision, just move to the next point
                currentPoint += moveDelta;
                Vector2 adjustedGravity = Physics2D.gravity * gravityScale;  
                currentVelocity += adjustedGravity * timeStep;

                points.Add(currentPoint);
                timeRemaining -= timeStep;
                timeStep = simulationTime / resolution; // Reset timeStep 
            }

        }

        lineRenderer.positionCount = points.Count;
        lineRenderer.SetPositions(points.Select(p => new Vector3(p.x, p.y, 0)).ToArray());
    }


    public void ClearTrajectory()
    {
        lineRenderer.positionCount = 0;
    }
}
