using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallLauncher : MonoBehaviour
{
    [SerializeField] private GameObject ballParent;
    [SerializeField] private float ballspeed = 500f;
    [SerializeField] private float launchRotationFactor = 25f;
    [SerializeField] private float launchAngleFactor = 3f;

    private Vector3 worldPosition;

    private void Awake()
    {
        worldPosition = Camera.main.transform.position;
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                Touch touch = Input.GetTouch(i);
                //float touchX = (touch.position.x - Screen.width / 2) / Screen.width;
                //float touchY = (touch.position.y - Screen.height / 2) / Screen.height;
                if (touch.phase == TouchPhase.Began)
                {
                    Vector3 touchPosition = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 2.9f));
                    GameObject currentBall = FindNearestSphereToTouchPosition(touchPosition);
                    if (currentBall == null) return;
                    currentBall.GetComponent<Rigidbody>().AddForce(Vector3.forward * ballspeed);
                    //Quaternion ballRotation = Quaternion.Euler(new Vector3((touchPosition.y * launchAngleFactor) * launchRotationFactor, -touchPosition.x * launchRotationFactor, 0f));
                    //GameObject newBall = Instantiate(ballPrefab, worldPosition, ballRotation);
                    //newBall.GetComponent<Rigidbody>().AddForce(newBall.transform.rotation * Vector3.forward * ballspeed);
                }
            }
        }
    }

    private GameObject FindNearestSphereToTouchPosition(Vector3 touchPosition)
    {
        GameObject nearest = null;
        float distance = float.MaxValue;

        for (int i = 0; i < ballParent.transform.childCount; i++)
        {
            Transform ball = ballParent.transform.GetChild(i);
            float candidateDistance = Vector3.Distance(touchPosition, ball.position);
            if (candidateDistance < distance)
            {
                nearest = ball.gameObject;
                distance = candidateDistance;
            }
        }

        return nearest;
    }
}
