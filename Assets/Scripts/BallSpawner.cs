using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private GameObject ballStopper;

    public bool onGround = true;
    private GameSettings gameSettings;
    private int spawnedBallCount = 0;

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

    private void Start()
    {
        gameSettings = GameSettings.Instance;
    }

    private void Update()
    {
        if (onGround)
        {
            if (gameSettings.testMode || spawnedBallCount < gameSettings.ballLimit)
            {
                // stop spawning if the ball has not landed yet
                onGround = false;
                Instantiate(ballPrefab, transform.position, Quaternion.identity, transform);
                // create a stopper object to fix the ball on the screen
                Instantiate(ballStopper);
                spawnedBallCount++;
            }
        }
    }
}