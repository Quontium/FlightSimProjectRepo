using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class CombatAI : SimpleAI
{
    [SerializeField] Rigidbody targetForTesting;
    
    [SerializeField] float targetSearchInterval = 5f;
    [SerializeField] float targetPositionUpdateInterval = 1f;
    
    Rigidbody target;
    float lastCheckTime = 0;

    void Update()
    {
        if (ActiveState.GetType() != typeof(WanderState)) return;
        if (Time.time - lastCheckTime < targetSearchInterval) return;
        
        Debug.Log("Hunting for targets...");
        //For testing
        if (boundary.IsInsideBoundary(targetForTesting.position))
        {
            Debug.Log($"Target: {targetForTesting.gameObject.name} is inside boundary!");
            target = targetForTesting;
            ActiveState = new AttackState(this, targetForTesting);
            Debug.Log("Entering AttackState");
            StartCoroutine(RefreshTargetPosition()); ;
        }

        lastCheckTime = Time.time;


        //KDTree logic here to search for nearest target
        //if target found:
        //ActiveState = new AttackState(this, target);
        //StartCoroutine(RefreshTargetPosition);

        //Can also add:
        //if no enemies found, only friendlies:
        //ActiveState = new FormationState(this, formationLeader);
    }

    IEnumerator RefreshTargetPosition()
    {
        Debug.Log("Pursuing target...");
        while (target != null || boundary.IsInsideBoundary(target.position))
        {
            SetNewTargetPosition();
            yield return new WaitForSeconds(targetPositionUpdateInterval);
        }
        Debug.Log($"Target lost... Entering WanderState");
        ActiveState = new WanderState();
    }
}