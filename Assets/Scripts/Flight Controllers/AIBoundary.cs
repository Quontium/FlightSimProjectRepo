using System;
using System.Diagnostics;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class AIBoundary
{
    [SerializeField] bool useSpawnPointAsCenter = true;
    [SerializeField] Vector3 boundaryCenter = Vector3.zero;
    
    [SerializeField] float minimumAltitude = 250;
    [SerializeField] float maximumAltitude = 1500;
    
    
    [SerializeField] float maximumRangeFromCenterX = 2500;
    [SerializeField] float maximumRangeFromCenterZ = 2500;

    public void SetSpawnPoint(Vector3 spawnPoint)
    {
        if (useSpawnPointAsCenter) boundaryCenter = spawnPoint;
    }
    
    public bool IsInsideBoundary(Vector3 position)
    {
        if (position.y > maximumAltitude || position.y < minimumAltitude) return false;
        if (position.x > boundaryCenter.x + maximumRangeFromCenterX ||
            position.x < boundaryCenter.x - maximumRangeFromCenterX) return false;
        if (position.z > boundaryCenter.z + maximumRangeFromCenterZ ||
            position.z < boundaryCenter.z - maximumRangeFromCenterZ) return false;
        return true;
    }

    public Vector3 GetRandomInsideBoundary()
    {
        Vector3 randomLocation;
        randomLocation.x = Random.Range(boundaryCenter.x - maximumRangeFromCenterX, boundaryCenter.x + maximumRangeFromCenterX);
        randomLocation.y = Random.Range(minimumAltitude, maximumAltitude);
        randomLocation.z = Random.Range(boundaryCenter.z - maximumRangeFromCenterZ, boundaryCenter.z + maximumRangeFromCenterZ);
        return randomLocation;
    }
}