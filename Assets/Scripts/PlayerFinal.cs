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
        UpdateCameraFollow();
        AdjustSpeedBasedOnAttitude();
    }
    
    void UpdateCameraFollow()
    {
        Vector3 moveCamTo = transform.position - transform.forward * 10.0f + Vector3.up * 1.0f;
        float bias = 0.96f;
        mainCamera.transform.position = mainCamera.transform.position * bias +
                                        moveCamTo * (1.0f - bias);
        mainCamera.transform.LookAt(transform.position + transform.forward * 30.0f);
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

        transform.Rotate(0, (rotation + flightControlInput.y * -1.5f), 0);

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