using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFinal : MonoBehaviour
{
    public float torque = 100f;
    public float reverse = 50f;
    public float thrust = 100f;
    public float speed = 90.0f;
    public float rotationSpeed = 100.0f;
    private float glide;
    public float yaw = 90f;
    private Rigidbody rb;
    //private float movSpeed = 50f;



    void Start()
    {
        glide = 0f;
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {

        //Camera Follow
        Vector3 moveCamTo = transform.position - transform.forward * 10.0f + Vector3.up * 1.0f;
        float bias = 0.96f;
        Camera.main.transform.position = Camera.main.transform.position * bias +
                                         moveCamTo * (1.0f - bias);
        Camera.main.transform.LookAt(transform.position + transform.forward * 30.0f);


        speed -= transform.forward.y * Time.deltaTime * 10.0f;

        if (speed < 25.0f)
        {
            speed = 25.0f;
        }

        //Flight Movement
        float roll = Input.GetAxis("Horizontal") * 5.5f;
        float pitch = Input.GetAxis("Vertical") * 4.0f;
        float rotation = Input.GetAxis("Yaw") * rotationSpeed;
        bool throttle = Input.GetKey("space");
        bool brake = Input.GetKey("b");

        var opposite = -rb.velocity;
        var brakePower = 500;
        var brakeForce = opposite.normalized * brakePower;

        rotation *= Time.deltaTime;

        transform.Rotate(0, (rotation + Input.GetAxis("Yaw") * -1.5f), 0);

        rb.AddRelativeTorque(Vector3.back * torque * roll);
        rb.AddRelativeTorque(Vector3.right * torque * pitch);

        if (throttle)
        {
            rb.AddRelativeForce(Vector3.forward * thrust);
            glide = thrust;
        }
        if (brake)
        {
            rb.AddForce(opposite * Time.deltaTime);
            rb.AddForce(brakeForce * Time.deltaTime);
            //rb.AddRelativeForce(-Vector3.forward * reverse);
            glide = reverse;
        }
        else
        {
            rb.AddRelativeForce(Vector3.forward * glide);
            glide *= 0.995f;
        }
    }
    //private void OnTriggerEnter(Collider other)
    //{
    //transform.Translate(Vector3.forward * Time.deltaTime * movSpeed);
    //movSpeed = movSpeed * -1;
    //}
}