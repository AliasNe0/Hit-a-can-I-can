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

    IEnumerator DestroyStopper()
    {
        yield return new WaitForSeconds(colliderDelay);
        Destroy(gameObject);
    }
}
