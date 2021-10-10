using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundBoundary : MonoBehaviour
{
    [SerializeField] private Transform player;
    private Rigidbody rb;
    //public float moveSpeed = 180f;
    public float smooth = 2.0f;
    float pitch = 4.0f;
    public float torque = 100f;
    private float glide;
    public float rotationSpeed = 100.0f;

    private void FixedUpdate()
    {
        glide = 0f;
        rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        float rotation = rotationSpeed;
        rotation *= Time.deltaTime;

        if (collision.CompareTag("Terrain"))
        {
            player.transform.Rotate((rotation * 45f), 0f, 0f);
            //rb.MovePosition(transform.position * moveSpeed);
            //rb.AddRelativeForce(Vector3.forward * torque);
            rb.AddRelativeTorque(Vector3.forward * torque * pitch);
        }
    }
}
