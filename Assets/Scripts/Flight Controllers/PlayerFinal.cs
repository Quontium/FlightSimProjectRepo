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
    private Rigidbody rb;
    //private float movSpeed = 50f;

    [SerializeField] AerodynamicProperties aerodynamicProperties;

    Camera mainCamera;
    IController controller;


    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        mainCamera = Camera.main;
        
        RegisterControlInput();
    }

    void RegisterControlInput()
    {
        controller = GetComponent<IController>();
        controller.OnFlightControlInput += ApplyFlightControlInput;
        controller.OnThrottleInput += ApplyThrottleInput;
        controller.OnBrakeInput += ApplyBrakeInput;
    }

    void FixedUpdate()
    {
        AdjustSpeedBasedOnAttitude();
    }

    void AdjustSpeedBasedOnAttitude()
    {
        speed -= transform.forward.y * Time.deltaTime * 10.0f;

        if (speed < 25.0f)
        {
            speed = 25.0f;
        }
    }
    
    void ApplyFlightControlInput(Vector3 flightControlInput)
    {
        float roll = flightControlInput.z * 5.5f;
        float pitch = flightControlInput.x * 4.0f;
        float rotation = flightControlInput.y * rotationSpeed;

        rotation *= Time.deltaTime;

        //transform.Rotate(0, (rotation + flightControlInput.y * -1.5f), 0);
        rb.AddRelativeTorque(Vector3.up * torque * rotation);
        rb.AddRelativeTorque(Vector3.back * torque * roll);
        rb.AddRelativeTorque(Vector3.right * torque * pitch);
    }

    void ApplyThrottleInput(bool throttle)
    {
        

        if (!throttle) return;
        
        rb.AddRelativeForce(Vector3.forward * thrust);
        glide = thrust;

    }

    void ApplyBrakeInput(bool brake)
    {
        Vector3 opposite = -rb.velocity;
        int brakePower = 500;
        Vector3 brakeForce = opposite.normalized * brakePower;
        
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
}