using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private int ballLimit = 3;
    [SerializeField] private float spawnerDelay = 0.2f;
    private int ballCount = 0;
    private bool instantiated = true;
  

    private void Update()
    {
        if (instantiated)
        {
            instantiated = false;
            if (ballCount < ballLimit)
            {
                StartCoroutine(InstantiateBall());
            }
        }
    }

    IEnumerator InstantiateBall()
    {
        Instantiate(ballPrefab, transform.position, Quaternion.identity, transform);
        ballCount++;
        yield return new WaitForSeconds(spawnerDelay);
        instantiated = true;
    }
}
