using System;
using UnityEngine;

public interface IController
{
    public event Action<Vector3> OnFlightControlInput;
    public event Action<bool> OnThrottleInput;
    public event Action<bool> OnBrakeInput;
}
