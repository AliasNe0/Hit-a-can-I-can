using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCollider : MonoBehaviour
{
    // on collision, send a signal that balls and cans are grounded
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Ball"))
        {
            // eliminate multiple updates of the bool by removing the tag trigger
            collision.gameObject.tag = "Untagged";
            BallSpawner.Instance.onGround = true;
        }
        if (collision.transform.CompareTag("Can"))
        {
            // eliminate multiple updates of the bool by removing the tag trigger
            collision.gameObject.tag = "Untagged";
            collision.transform.parent.GetComponent<CanSpawner>().onGround = true;
        }
    }
}