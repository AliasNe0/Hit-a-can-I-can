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
            // only the first touch is used
            // consequent simultanious touches are ignored
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                // on start phase find a ball closest to the touch position (can be used if multiple balls are spawned at a time)
                currentBall = FindNearestBallToTouchPosition(touch);
            }
            if (touch.phase == TouchPhase.Ended)
            {
                // no effect if a ball is not spawned yet
                if (currentBall == null) return;
                // calculate the launching direction
                Vector3 ballLaunchVector = FindBallLaunchVector(touch);
                // apply a force obtained by multiplication of a normal vector (direction) and the speed factor
                currentBall.GetComponent<Rigidbody>().AddForce(gameSettings.ballSpeedFactor * ballLaunchVector);
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
            // "-2f * Camera.main.transform.position.y" is required to get a correct screen position
            Vector3 ballScreenPosition = Camera.main.WorldToScreenPoint(new Vector3(ball.position.x, -2f * Camera.main.transform.position.y + ball.position.y, 0f));
            float candidateDistance = Vector3.Distance(ballScreenPosition, touch.position);
            // the ball is only available for user interaction when it has the tag "Ball"
            // thus, the closest ball from a set of activated balls only is returned
            if (ball.CompareTag("Ball") && candidateDistance < distance)
            {
                nearest = ball.gameObject;
                distance = candidateDistance;
            }
        }
        return nearest;
    }

    private Vector3 FindBallLaunchVector(Touch touch)
    {
        // "-2f * Camera.main.transform.position.y" is required to get a correct screen position
        Vector3 ballScreenPosition = Camera.main.WorldToScreenPoint(new Vector3(currentBall.transform.position.x, -2f * Camera.main.transform.position.y + currentBall.transform.position.y, 0f));
        float deltaX = touch.position.x - ballScreenPosition.x;
        float deltaY = touch.position.y - ballScreenPosition.y;
        // negative deltaY is ignored when final touch position on the screen is lower that the ball position on the screen
        deltaY = deltaY < 0f ? 0f : deltaY;
        // limit final touch position to decrese touch movement required to reach the largest launching angle
        deltaY = deltaY > gameSettings.moveDistanceLimit ? gameSettings.moveDistanceLimit : deltaY;
        // the smaller deltaY is the greater it decreases x- and y-angles
        // it is done to avoid extreme x- and y-angles at low deltaY
        float launchFactor = deltaY / gameSettings.moveDistanceLimit;
        // y-component is doubled to increase max yz-angle up to 60 degrees
        Vector3 ballLaunchVector = new Vector3(deltaX * launchFactor, 2f * deltaY * launchFactor, deltaY);
        // only the direction is returned, not the magnitude
        return Vector3.Normalize(ballLaunchVector);
    }
}
