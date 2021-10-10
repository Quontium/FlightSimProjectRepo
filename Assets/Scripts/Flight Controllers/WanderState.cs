using System.Collections;
using System;
using UnityEngine;

public class WanderState : AIState
{
    public override Vector3 GetNewTargetPosition(AIBoundary bounds)
    {
        return bounds.GetRandomInsideBoundary();
    }

}