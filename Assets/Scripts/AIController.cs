using System;
using UnityEngine;

public class AIController : MonoBehaviour, IController
{
    public event Action<Vector3> OnFlightControlInput;
    public event Action<bool> OnThrottleInput;
    public event Action<bool> OnBrakeInput;

    protected Vector3 targetPosition;
    protected Vector3 directionToTarget;
    protected Vector3 spawnPosition;
    protected float angleToTarget;

    protected event Action OnReachedTarget;
    
    [SerializeField] float targetRange = 25f;

    void Awake()
    {
        spawnPosition = transform.position;
        SetTarget(spawnPosition);
    }

    protected virtual void SetTarget(Vector3 targetPosition)
    {
        this.targetPosition = targetPosition;
    }

    void FixedUpdate()
    {
        directionToTarget = GetTargetDirection();
        angleToTarget = GetAngleToTarget();
        
        OrientTowardsTarget();
        AccelerateIfFacingTarget();
        BrakeIfFacingAwayFromTarget();

        if (TargetWithinRange()) OnReachedTarget?.Invoke();
    }

    Vector3 GetTargetDirection() => (targetPosition - transform.position).normalized;
    float GetAngleToTarget() => Vector3.Angle(transform.forward, directionToTarget);
    bool TargetWithinRange() => (targetPosition - transform.position).magnitude < targetRange;
    

    void OrientTowardsTarget()
    {
        Vector3 torqueVector = GetTorqueVector();
        OnFlightControlInput?.Invoke(torqueVector);
    }

    Vector3 GetTorqueVector()
    {
        Vector3 torqueVector = Vector3.Cross(transform.forward, directionToTarget);
        torqueVector.z = Vector3.Cross(transform.up, Vector3.down).z;
        torqueVector = transform.InverseTransformDirection(torqueVector);
        return torqueVector;
    }

    void AccelerateIfFacingTarget()
    {
        bool facingTarget = angleToTarget < 50f;
        OnThrottleInput?.Invoke(facingTarget);
    }
    
    void BrakeIfFacingAwayFromTarget()
    {
        //bool facingAway = angleToTarget > 120f;
        OnBrakeInput?.Invoke(false);
    }
}
