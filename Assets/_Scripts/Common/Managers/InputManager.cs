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

    // Reference to the basketball manager
    public BasketballManager basketballManager;

    private void Awake()
    {
        trajectoryPredictor = currentBallObject.GetComponent<TrajectoryPredictor>();
        basketballManager = FindObjectOfType<BasketballManager>();  // Get a reference to the BasketballManager
    }

    void Update()
    {
        // If the game has ended, don't process any further input
        if (basketballManager.GameHasEnded)
        {
            return;
        }
        // Mouse input
        if (Input.GetMouseButtonDown(0))  // Simulates TouchPhase.Began
        {
            startPosition = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))  // Simulates TouchPhase.Moved
        {
            if (!hasShot)
            {
                Vector2 currentMousePosition = Input.mousePosition;
                Vector2 forceDirection = startPosition - currentMousePosition;
                Vector2 force = forceDirection * forceMultiplier;

                trajectoryPredictor.PredictTrajectory(force);
            }
        }

        if (Input.GetMouseButtonUp(0))  // Simulates TouchPhase.Ended
        {
            if (!hasShot)
            {
                endPosition = Input.mousePosition;
                Vector2 forceDirection = startPosition - endPosition;
                Vector2 force = forceDirection * forceMultiplier;

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
        }
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
                        Vector2 force = forceDirection * forceMultiplier;

                        trajectoryPredictor.PredictTrajectory(force);
                    }
                    break;

                case TouchPhase.Ended:
                    if (!hasShot)
                    {
                        endPosition = touch.position;
                        Vector2 forceDirection = startPosition - endPosition;
                        Vector2 force = forceDirection * forceMultiplier;

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
            ResetShot();
        }

        if (hasShot)
        {
            IShootable ball = currentBallObject as IShootable;
            if (ball.HasScored())
            {

                basketballManager.ScoreBasket();
                ResetShot();
            }
            else if (ball.HasHitGround())
            {
                basketballManager.FailShot();
                ResetShot();
            }
        }
    }

    // Reset the ball and related settings
    public void ResetShot()
    {
        IShootable ballToReset = currentBallObject as IShootable;
        if (ballToReset != null)
        {
            ballToReset.ResetBall(ref hasShot);
            Camera.main.GetComponent<CameraFollow>().StopFollowing();
        }
    }
}
