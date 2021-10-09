using System;
using UnityEngine;
using Random = UnityEngine.Random;

class AIWandererController : AIController
{

    [SerializeField] float maximumRangeFromSpawn = 2500;
    [SerializeField] float minimumAltitude = 250;
    [SerializeField] float maximumAltitude = 1500;
    
    void OnEnable()
    {
        OnReachedTarget += SetNewRandomLocation;
        SetNewRandomLocation();
    }
    
    Vector3 GetRandomTargetLocation()
    {
        Vector3 randomLocation;
        randomLocation.x = Random.Range(-maximumRangeFromSpawn, maximumRangeFromSpawn);
        randomLocation.y = Random.Range(minimumAltitude, maximumAltitude);
        randomLocation.z = Random.Range(-maximumRangeFromSpawn, maximumRangeFromSpawn);
        return randomLocation;
    }

    void SetNewRandomLocation()
    {
        SetTarget(GetRandomTargetLocation());
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(targetPosition, targetRadius);
    }
}