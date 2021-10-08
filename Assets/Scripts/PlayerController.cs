using System;
using UnityEngine;


public class PlayerController : MonoBehaviour, IController
{
    public event Action<Vector3> OnFlightControlInput;
    public event Action<bool> OnThrottleInput;
    public event Action<bool> OnBrakeInput;

    void FixedUpdate()
    {
        ReadFlightControlInput();
        ReadThrottleInput();
        ReadBrakeInput();
    }

    void ReadFlightControlInput()
    {
        Vector3 controlInput = Vector3.zero;

        controlInput.x = Input.GetAxis("Vertical");
        controlInput.y = Input.GetAxis("Yaw");
        controlInput.z = Input.GetAxis("Horizontal");

        OnFlightControlInput?.Invoke(controlInput);
    }

    void ReadThrottleInput()
    {
        bool throttleApplied = Input.GetKey("space");
        OnThrottleInput?.Invoke(throttleApplied);
    }
    
    void ReadBrakeInput()
    {
        bool brakeApplied = Input.GetKey("b");
        OnBrakeInput?.Invoke(brakeApplied);
    }
}
