using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1RespawnGround : MonoBehaviour
{
    [SerializeField] private Transform enemy;
    [SerializeField] private Transform respawnPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemy.transform.position = respawnPoint.transform.position;
            Physics.SyncTransforms();
        }
    }
}