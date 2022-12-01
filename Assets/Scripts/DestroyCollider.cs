using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyCollider : MonoBehaviour
{
    [SerializeField] private GameObject ballSpawner;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Ball"))
        {
            Destroy(collision.gameObject);
            ballSpawner.GetComponent<BallSpawner>().destroyed = true;
        }
    }
}
