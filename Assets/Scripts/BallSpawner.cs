using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private GameObject ballStopper;
    [SerializeField] private int ballLimit = 3;
    private int ballCount = 0;
    public bool destroyed = true;

    private void Update()
    {
        if (destroyed)
        {
            if (ballCount < ballLimit)
            {
                destroyed = false;
                Instantiate(ballPrefab, transform.position, Quaternion.identity, transform);
                Instantiate(ballStopper);
                ballCount++;
            }
        }
    }
}
