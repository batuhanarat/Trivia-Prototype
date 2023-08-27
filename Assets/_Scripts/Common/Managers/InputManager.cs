using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private Vector2 startPosition;
    private Vector2 endPosition;

    [SerializeField] private float forceMultiplier = 2.4f;

    public MonoBehaviour currentBallObject;  // Reference to the current ball GameObject

    private bool hasShot = false;  // Track if the ball has been shot

    private TrajectoryPredictor trajectoryPredictor;

    private void Awake()
    {
        trajectoryPredictor = currentBallObject.GetComponent<TrajectoryPredictor>();
    }

    void Update()
    {
        // Check for touches
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    startPosition = touch.position;
                    break;

                case TouchPhase.Moved:
                    if (!hasShot)
                    {
                        Vector2 currentTouchPosition = touch.position;
                        Vector2 forceDirection = startPosition - currentTouchPosition;
                        Vector2 force = forceDirection * forceMultiplier * -1;

                        trajectoryPredictor.PredictTrajectory(force);
                    }
                    break;

                case TouchPhase.Ended:
                    if (!hasShot)
                    {
                        endPosition = touch.position;
                        Vector2 forceDirection = startPosition - endPosition;
                        Vector2 force = forceDirection * forceMultiplier * -1;

                        trajectoryPredictor.ClearTrajectory();

                        IShootable ball = currentBallObject as IShootable;
                        if (ball != null)
                        {
                            ball.Shoot(force);
                            hasShot = true;
                            Camera.main.GetComponent<CameraFollow>().StartFollowing();
                            Camera.main.GetComponent<CameraFollow>().CenterBasketball();

                        }
                    }
                    break;
            }
        }

        // Reset functionality (for testing purposes on a keyboard)
        if (Input.GetKeyDown(KeyCode.R))
        {
            IShootable ballToReset = currentBallObject as IShootable;
            if (ballToReset != null)
            {
                ballToReset.ResetBall(ref hasShot);
                Camera.main.GetComponent<CameraFollow>().StopFollowing();
            }
        }
    }
}
