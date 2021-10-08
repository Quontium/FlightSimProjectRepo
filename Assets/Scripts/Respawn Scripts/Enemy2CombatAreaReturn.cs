using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2CombatAreaReturn : MonoBehaviour
{
    [SerializeField] private Transform enemy2;
    private Rigidbody rb;
    public float moveSpeed = 180f;
    public float smooth = 1.5f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy2"))
        {
            enemy2.transform.Rotate(smooth * 180f, 0, 0f);
            rb.MovePosition(transform.position * moveSpeed);
        }
    }
}