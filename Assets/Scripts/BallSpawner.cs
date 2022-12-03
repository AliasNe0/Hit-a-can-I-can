using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private GameObject ballStopper;

    private GameSettings gameSettings;
    private int ballCount = 0;
    public bool onGround = true;

    public static BallSpawner Instance { get; private set; }
    private void Awake()
    {

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Update()
    {
        if (onGround)
        {
            gameSettings = GameSettings.Instance;
            int ballLimit = gameSettings.ballLimit;
            if (gameSettings.testMode || ballCount < ballLimit)
            {
                // stop spawning if the ball has not landed yet
                onGround = false;
                Instantiate(ballPrefab, transform.position, Quaternion.identity, transform);
                // create a stopper object to fix the ball on the screen
                Instantiate(ballStopper);
                ballCount++;
            }
        }
    }
}
