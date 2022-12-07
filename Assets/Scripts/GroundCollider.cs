using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCollider : MonoBehaviour
{
    private BallSpawner ballSpawner;
    private ScoreBoard scoreBoard;

    private void Start()
    {
        ballSpawner = BallSpawner.Instance;
        scoreBoard = ScoreBoard.Instance;
    }

    // on collision, send a signal that balls and cans are grounded
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision");
        if (collision.transform.CompareTag("Ball"))
        {
            // eliminate multiple updates of the bool by removing the triggering tag
            collision.gameObject.tag = "Untagged";
            ballSpawner.onGround = true;
            scoreBoard.UpdateBallCount();

        }
        if (collision.transform.CompareTag("Can"))
        {
            // eliminate multiple updates of the bool by removing the triggering tag
            collision.gameObject.tag = "Untagged";
            collision.transform.parent.GetComponentInParent<CanSpawner>().onGround = true;
            scoreBoard.UpdateCanCount();
        }
    }
}