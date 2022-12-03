using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using UnityEditor.Build.Content;
using UnityEngine;

public class CanSpawner : MonoBehaviour
{
    [SerializeField] private GameObject canPrefab;
    public bool onGround = false;

    private void Start()
    {
        Instantiate(canPrefab, transform.position, Quaternion.identity, transform);
    }

    private void Update()
    {
        if (GameSettings.Instance.testMode && onGround)
        {
            onGround = false;
            StartCoroutine(SpawnCan());
        }
    }

    IEnumerator SpawnCan()
    {
        yield return new WaitForSeconds(GameSettings.Instance.canRespawnTimer);
        Instantiate(canPrefab, transform.position, Quaternion.identity, transform);
    }
}
