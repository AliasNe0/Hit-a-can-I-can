using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoBehaviour
{
    [Header("General")]
    [Tooltip("Maximum number of balls for this level")]
    public int ballLimit = 3;
    [Tooltip("Ball selection distance measured from the center of the ball (First touch)")]
    public float touchDistanceLimit = 150f;
    [Tooltip("y-distance measured from the center of the ball (Final touch)")]
    public float moveDistanceLimit = 1500f;
    public float ballSpeedFactor = 4000f;
    [Header("Test mode")]
    [Tooltip("Infinite balls and cans")]
    public bool testMode = false;
    public float canRespawnTimer = 3f;

    public static GameSettings Instance { get; private set; }
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
}
