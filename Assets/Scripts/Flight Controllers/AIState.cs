using UnityEngine;

public abstract class AIState 
 {
     public abstract Vector3 GetNewTargetPosition(AIBoundary bounds);
     
     protected Vector3 GetPredictedTrajectory(Rigidbody interceptor, Rigidbody interceptionTarget)
     {
         return interceptionTarget.position;
         
         float targetSpeed = interceptionTarget.velocity.magnitude;
         float selfSpeed = interceptor.velocity.magnitude;

         float time = (interceptionTarget.position - interceptor.position).magnitude / (selfSpeed - targetSpeed);
         Vector3 interceptPosition = interceptionTarget.position + interceptionTarget.velocity * time;

         return interceptPosition;
     }
 }