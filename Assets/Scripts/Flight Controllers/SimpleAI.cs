using UnityEngine;

public class SimpleAI : AIController
{
    [SerializeField] protected AIBoundary boundary;
    [SerializeField] float collisionAvoidanceCheckDistance = 250;
    
    AIState activeState;
    public AIState ActiveState
    {
        get => activeState;
        set
        {
            activeState = value; 
            SetNewTargetPosition();
        }
    }

    void Awake()
    {
        boundary.SetSpawnPoint(transform.position);
        ActiveState = new WanderState();
        OnReachedTarget += SetNewTargetPosition;
    }

    void Update()
    {
        if (Physics.Raycast(new Ray(transform.position, transform.forward), collisionAvoidanceCheckDistance) &&
            !(ActiveState is CollisionAvoidanceState))
        {
            Brake();
            ActiveState = new CollisionAvoidanceState(this);
        }
    }
    void SetTarget(Vector3 targetPos)
    {
        targetPosition = targetPos;
    }

    protected void SetNewTargetPosition()
    {
        SetTarget(ActiveState.GetNewTargetPosition(boundary));
    }
    
}