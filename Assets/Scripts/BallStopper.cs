using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallStopper : MonoBehaviour
{
    [SerializeField] private float colliderDelay = 0.2f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Ball"))
        {
            StartCoroutine(DestroyStopper());
        }
    }

<<<<<<< HEAD
    // delay is needed to fully stop the ball
=======
>>>>>>> parent of f901bb5 (Code commented)
    IEnumerator DestroyStopper()
    {
        yield return new WaitForSeconds(colliderDelay);
        Destroy(gameObject);
    }
}
