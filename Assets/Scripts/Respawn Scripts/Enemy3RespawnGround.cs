using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3RespawnGround : MonoBehaviour
{
    [SerializeField] private Transform enemy3;
    [SerializeField] private Transform respawnPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy2"))
        {
            enemy3.transform.position = respawnPoint.transform.position;
            Physics.SyncTransforms();
        }
    }
}