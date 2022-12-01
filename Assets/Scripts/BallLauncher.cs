using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallLauncher : MonoBehaviour
{
    [SerializeField] private GameObject ballParent;
    [SerializeField] private float touchDistanceLimit = 150f;
    [SerializeField] private float moveDistanceLimit = 1000f;
    [SerializeField] private float ballSpeedFactor = 0.5f;

    private GameObject currentBall;

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                Touch touch = Input.GetTouch(i);
                if (touch.phase == TouchPhase.Began)
                {
                    currentBall = FindNearestBallToTouchPosition(touch);
                }
                if (touch.phase == TouchPhase.Ended)
                {
                    if (currentBall == null) return;
                    Vector3 ballLaunchVector = FindBallLaunchVector(touch);
                    currentBall.GetComponent<Rigidbody>().AddForce(ballLaunchVector * ballSpeedFactor);
                }
            }
        }
    }

    private GameObject FindNearestBallToTouchPosition(Touch touch)
    {
        GameObject nearest = null;
        float distance = touchDistanceLimit;

        for (int i = 0; i < ballParent.transform.childCount; i++)
        {
            Transform ball = ballParent.transform.GetChild(i);
            Vector3 ballScreenPosition = Camera.main.WorldToScreenPoint(new Vector3(ball.position.x, -2f * Camera.main.transform.position.y + ball.position.y, 0f));
            float candidateDistance = Vector3.Distance(ballScreenPosition, touch.position);
            if (candidateDistance < distance)
            {
                nearest = ball.gameObject;
                distance = candidateDistance;
            }
        }
        return nearest;
    }

    private Vector3 FindBallLaunchVector(Touch touch)
    {
        Vector3 ballScreenPosition = Camera.main.WorldToScreenPoint(new Vector3(currentBall.transform.position.x, -2f * Camera.main.transform.position.y + currentBall.transform.position.y, 0f));
        float deltaX = touch.position.x - ballScreenPosition.x;
        float deltaY = touch.position.y - ballScreenPosition.y;
        deltaY = deltaY > 0f ? deltaY : 0f;
        deltaY = deltaY < moveDistanceLimit ? deltaY : moveDistanceLimit;
        return new Vector3(deltaX, deltaY, deltaY);
    }
}
