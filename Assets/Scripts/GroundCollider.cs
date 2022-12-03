using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCollider : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Ball"))
        {
            collision.gameObject.tag = "Untagged";
            BallSpawner.Instance.onGround = true;
        }
        if (collision.transform.CompareTag("Can"))
        {
            Debug.Log("Can collision");
            collision.gameObject.tag = "Untagged";
            collision.transform.parent.GetComponent<CanSpawner>().onGround = true;
        }
    }
}