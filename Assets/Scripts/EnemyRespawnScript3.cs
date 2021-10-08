using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRespawnScript3 : MonoBehaviour
{
    [SerializeField] private Transform enemy3;
    [SerializeField] private Transform respawnPoint;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy3"))
        {
            enemy3.transform.position = respawnPoint.transform.position;
            Physics.SyncTransforms();
        }
    }
}