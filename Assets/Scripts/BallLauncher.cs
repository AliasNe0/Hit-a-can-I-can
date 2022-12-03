using UnityEngine;

public class BallLauncher : MonoBehaviour
{
    private GameSettings gameSettings;
    private GameObject currentBall;

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            gameSettings = GameSettings.Instance;

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
                    currentBall.GetComponent<Rigidbody>().AddForce(gameSettings.ballSpeedFactor * ballLaunchVector);
                }
            }
        }
    }

    private GameObject FindNearestBallToTouchPosition(Touch touch)
    {
        GameObject nearest = null;
        float distance = gameSettings.touchDistanceLimit;

        GameObject ballParent = BallSpawner.Instance.gameObject;
        for (int i = 0; i < ballParent.transform.childCount; i++)
        {
            Transform ball = ballParent.transform.GetChild(i);
            Vector3 ballScreenPosition = Camera.main.WorldToScreenPoint(new Vector3(ball.position.x, -2f * Camera.main.transform.position.y + ball.position.y, 0f));
            float candidateDistance = Vector3.Distance(ballScreenPosition, touch.position);
            // the ball is only available for user interaction when it has the tag "Ball"
            // thus, the closest ball from a set of activated balls only is returned
            if (ball.CompareTag("Ball") && candidateDistance < distance)
            if (ball.tag == "Ball" && candidateDistance < distance)
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
        deltaY = deltaY < 0f ? 0f : deltaY;
        deltaY = deltaY > gameSettings.moveDistanceLimit ? gameSettings.moveDistanceLimit : deltaY;
        float launchFactor = deltaY / gameSettings.moveDistanceLimit;
        Vector3 ballLaunchVector = Vector3.Normalize(new Vector3(deltaX * launchFactor, 2f * deltaY * launchFactor, deltaY));
        return ballLaunchVector;
    }
}
