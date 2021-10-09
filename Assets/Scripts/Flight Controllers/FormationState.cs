using UnityEngine;

public class FormationState : AIState
{
    Rigidbody self;
    Rigidbody target;
    SimpleAI thisAI;
    
    public FormationState(SimpleAI thisAI, Rigidbody formationLeader)
    {
        this.thisAI = thisAI;
        self = thisAI.GetComponent<Rigidbody>();
        this.target = formationLeader;
    }
    public override Vector3 GetNewTargetPosition(AIBoundary bounds)
    {
        //For now, just get a random position behind the formation leader
        Vector3 offsetPosition;
        offsetPosition.x = Random.Range(-50, 50);
        offsetPosition.y = 0;
        offsetPosition.z = Random.Range(-50, -10);

        return target.rotation * offsetPosition;

    }
}