using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanRespawner : MonoBehaviour
{
    [SerializeField] private float respawnTimer = 3f;

    private Vector3 awakePosition = Vector3.zero;
    private Quaternion awakeRotation = Quaternion.identity;
    private Rigidbody rb;

    private void Awake()
    {
        awakePosition = transform.position;
        awakeRotation = transform.rotation;
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("DestroyCollider"))
        {
            StartCoroutine(RespawnCan());
        }
        if (collision.transform.CompareTag("CanEnvironment"))
        {
            rb.useGravity = true;
            rb.isKinematic = false;
        }
    }

    IEnumerator RespawnCan()
    {
        rb.useGravity = false;
        rb.isKinematic = true;
        yield return new WaitForSeconds(respawnTimer);
        transform.position = awakePosition;
        transform.rotation = awakeRotation;
    }
}
