using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BallLauncher : MonoBehaviour
{
    [Header("Camera punch")]
    [SerializeField] float punchFOV = 62f;
    [SerializeField] float punchLength = 0.5f;
    [SerializeField] float shakeStrength = 0.1f;
    private GameSettings gameSettings;
    private GameObject currentBall;

    private float ballSpeedMultiplicator;

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            gameSettings = GameSettings.Instance;
            // only the first touch is used
            // consequent simultaneous touches are ignored
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                // on start phase find a ball closest to the touch position (can be used if multiple balls need to be spawned at a time)
                currentBall = FindNearestBallToTouchPosition(touch);
            }
            if (touch.phase == TouchPhase.Ended)
            {
                // no effect if a ball is not spawned yet
                if (currentBall == null) return;
                // calculate the launching direction
                Vector3 ballLaunchVector = FindBallLaunchVector(touch);
                // apply a force obtained by multiplication of a normal vector (direction) with ballSpeedMultiplicator and ballSpeedFactor
                currentBall.GetComponent<Rigidbody>().AddForce(gameSettings.ballSpeedFactor * ballSpeedMultiplicator * ballLaunchVector);
                // change tag to disable player input midair
                currentBall.tag = "Ball";
                PunchCamera(ballLaunchVector);
            }
        }
    }

    private void PunchCamera(Vector3 ballLaunchVector)
    {
        Camera camera = Camera.main;
        float cameraFOV = camera.fieldOfView;
        camera.DOFieldOfView(punchFOV, punchLength / 2f).OnComplete(() => Camera.main.DOFieldOfView(cameraFOV, punchLength / 2f));
        Vector3 cameraShakeVector = new Vector3(ballLaunchVector.x, ballLaunchVector.y, 0f);
        camera.DOShakePosition(punchLength, shakeStrength * cameraShakeVector, 1, 1f, true, ShakeRandomnessMode.Harmonic);
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
            if (ball.CompareTag("CurrentBall") && candidateDistance < distance)
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
        // max y-movement limited to the middle of the screen
        float maxDeltaY = Screen.height / 2f - ballScreenPosition.y;
        // negative deltaY is ignored when final touch position on the screen is lower that the ball position on the screen
        // at least some force along y-axis should be applied in order to make the active ball collided with the ground, so the min value is 0.1f
        float deltaY = Mathf.Clamp(touch.position.y - ballScreenPosition.y, 0.1f, maxDeltaY);
        // yFactor smoothes ball launcher
        float yFactor = deltaY / maxDeltaY;
        // yFactor decreases speed at low deltaY
        ballSpeedMultiplicator = gameSettings.ballMinSpeedFactor + (1f - gameSettings.ballMinSpeedFactor) * yFactor;
        // the smaller is deltaY the greater it decreases x- and y-angles (yFactor)
        // it is done to avoid extreme x- and y-angles at low deltaY
        Vector3 ballLaunchVector = new Vector3(deltaX * yFactor, deltaY * yFactor, deltaY);
        // only the direction is returned, not the magnitude
        return Vector3.Normalize(ballLaunchVector);
    }
}