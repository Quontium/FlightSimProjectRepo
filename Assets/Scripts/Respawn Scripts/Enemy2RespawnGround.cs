using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2RespawnGround : MonoBehaviour
{
    [SerializeField] private Transform enemy2;
    [SerializeField] private Transform respawnPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy2"))
        {
            enemy2.transform.position = respawnPoint.transform.position;
            Physics.SyncTransforms();
        }
    }
}