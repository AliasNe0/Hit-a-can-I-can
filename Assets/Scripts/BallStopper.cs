using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallStopper : MonoBehaviour
{
    [SerializeField] private float colliderDelay = 0.2f;

    // destroy the stopper to avoid further collisions with the ball
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("SpawningBall"))
        {
            // change the tag to activate user interaction
            collision.gameObject.tag = "Ball";
            StartCoroutine(DestroyStopper());
        }
    }

    // delay is needed to fully stop the ball
    IEnumerator DestroyStopper()
    {
        yield return new WaitForSeconds(colliderDelay);
        Destroy(gameObject);
    }
}
