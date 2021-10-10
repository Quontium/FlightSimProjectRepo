using System.Linq;
using UnityEngine;

public class AttackState : AIState
{
    Rigidbody self;
    Rigidbody target;
    SimpleAI thisAI;

    public AttackState(SimpleAI thisAI, Rigidbody target)
    {
        this.thisAI = thisAI;
        self = thisAI.GetComponent<Rigidbody>();
        this.target = target;
    }

    public override Vector3 GetNewTargetPosition(AIBoundary bounds)
    {
        //Logic here to use KDTree to find closest enemy
        //If closest enemy is within bounds,
        //Get its rigidbody and set currentTarget

        if (target == null) thisAI.ActiveState = new WanderState();
        return GetPredictedTrajectory(self, target);
    }

}