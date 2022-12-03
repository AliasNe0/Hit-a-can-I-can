using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCollider : MonoBehaviour
{
    BallSpawner ballParent;
    GameSettings gameSettings;
    LevelManager levelManager;

    private int canCount = 0;

    private void Start()
    {
        levelManager = LevelManager.Instance;
        ballParent = BallSpawner.Instance;
        gameSettings = GameSettings.Instance;
    }

    // on collision, send a signal that balls and cans are grounded
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Ball"))
        {
            // eliminate multiple updates of the bool by removing the triggering tag
            collision.gameObject.tag = "Untagged";
            ballParent.onGround = true;
            if (!gameSettings.testMode && ballParent.gameObject.transform.childCount == gameSettings.ballLimit) levelManager.ShowLevelMenu();
        }
        if (collision.transform.CompareTag("Can"))
        {
            // eliminate multiple updates of the bool by removing the triggering tag
            collision.gameObject.tag = "Untagged";
            collision.transform.parent.GetComponent<CanSpawner>().onGround = true;
            canCount++;
            if (!gameSettings.testMode && collision.transform.root.childCount == canCount)
            {
                levelManager.levelIsCompleted = true;
                levelManager.ShowLevelMenu();
            }
        }
    }
}