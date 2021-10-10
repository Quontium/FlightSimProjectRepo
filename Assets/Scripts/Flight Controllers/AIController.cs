using System;
using System.Collections;
using UnityEngine;

public abstract class AIController : MonoBehaviour, IController
{
    public event Action<Vector3> OnFlightControlInput;
    public event Action<bool> OnThrottleInput;
    public event Action<bool> OnBrakeInput;

    protected Vector3 targetPosition;
    protected event Action OnReachedTarget;
    
    Vector3 directionToTarget;
    float angleToTarget;

    bool brakeApplied;
    
    
    [SerializeField] protected float targetRadius = 100f;
    [SerializeField] protected float angleFromTargetToAccelerate = 28f;

    
    void FixedUpdate()
    {
        directionToTarget = GetTargetDirection();
        angleToTarget = GetAngleToTarget();
        
        OrientTowardsTarget();
        AccelerateIfFacingTarget();
        OnBrakeInput?.Invoke(brakeApplied);

        if (TargetWithinRange()) OnReachedTarget?.Invoke();
    }

    Vector3 GetTargetDirection() => (targetPosition - transform.position).normalized;
    float GetAngleToTarget() => Vector3.Angle(transform.forward, directionToTarget);
    bool TargetWithinRange() => (targetPosition - transform.position).sqrMagnitude < targetRadius * targetRadius;
    

    void OrientTowardsTarget()
    {
        Vector3 torqueVector = GetTorqueVector();
        OnFlightControlInput?.Invoke(torqueVector);
    }

    Vector3 GetTorqueVector()
    {
        Vector3 torqueVector = Vector3.Cross(transform.forward, directionToTarget);
        torqueVector = transform.InverseTransformDirection(torqueVector);
        torqueVector.z = -(transform.rotation.eulerAngles.z / 180 - 1 - torqueVector.y * 2);

        return torqueVector;
    }

    void AccelerateIfFacingTarget()
    {
        bool facingTarget = angleToTarget < angleFromTargetToAccelerate;
        OnThrottleInput?.Invoke(facingTarget);
    }
    
    protected void Brake()
    {
        ApplyBrakes();
    }

    IEnumerator ApplyBrakes()
    {
        brakeApplied = true;
        yield return new WaitForSeconds(2);
        brakeApplied = false;
    }
    
    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(targetPosition, targetRadius);
    }
}