using UnityEngine;

public class CollisionAvoidanceState : AIState
{
    SimpleAI thisAI;
    
    public CollisionAvoidanceState(SimpleAI thisAI)
    {
        this.thisAI = thisAI;
        Debug.Log("Entered CollisionAvoidanceState...");
    }
    public override Vector3 GetNewTargetPosition(AIBoundary bounds)
    {
        if (!Physics.Raycast(new Ray(thisAI.transform.position, thisAI.transform.forward), 500))
        {
            thisAI.ActiveState = new WanderState();
            return thisAI.ActiveState.GetNewTargetPosition(bounds);
        }
        Debug.Log("Pull up!!!");
        return thisAI.transform.position + new Vector3(0, 400, 0);
    }
}