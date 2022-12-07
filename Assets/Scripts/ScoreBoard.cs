using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreBoard : MonoBehaviour
{
    private GameSettings gameSettings;
    private LevelManager levelManager;

    public int ballCount = 0;
    public int ballLimit = 0;
    public int canCount = 0;
    public int canLimit = 0;

    public static ScoreBoard Instance { get; private set; }
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
        levelManager = LevelManager.Instance;
        gameSettings = GameSettings.Instance;

        ballLimit = gameSettings.ballLimit;
        // +1 to compensate for ballCount--
        ballCount = ballLimit + 1;
        UpdateBallCount();
        canLimit = GameObject.FindGameObjectWithTag("CanParent").transform.root.childCount;
        // +1 to compensate for canCount--
        canCount = canLimit + 1;
        UpdateCanCount();
    }

    public void UpdateBallCount()
    {
        ballCount--;
        levelManager.UpdateBallCount(ballCount, ballLimit, gameSettings.testMode);
    }

    public void UpdateCanCount()
    {
        canCount--;
        levelManager.UpdateCanCount(canCount, canLimit, gameSettings.testMode);
    }
}
