using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoBehaviour
{
    public int ballLimit = 3;
    public float touchDistanceLimit = 150f;
    public float moveDistanceLimit = 1500f;
    public float ballSpeedFactor = 4000f;
    public float canRespawnTimer = 3f;
    public bool testMode = false;

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
