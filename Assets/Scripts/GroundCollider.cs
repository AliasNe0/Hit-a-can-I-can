using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCollider : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Ball"))
        {
<<<<<<< HEAD
            // eliminate multiple updates of the bool by removing the triggering tag
=======
>>>>>>> parent of f901bb5 (Code commented)
            collision.gameObject.tag = "Untagged";
            BallSpawner.Instance.onGround = true;
        }
        if (collision.transform.CompareTag("Can"))
        {
<<<<<<< HEAD
            // eliminate multiple updates of the bool by removing the triggering tag
=======
            Debug.Log("Can collision");
>>>>>>> parent of f901bb5 (Code commented)
            collision.gameObject.tag = "Untagged";
            collision.transform.parent.GetComponent<CanSpawner>().onGround = true;
        }
    }
}