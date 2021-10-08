using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneCrashDestruction : MonoBehaviour
{
    private void OntriggerEnter3D(BoxCollider other)
    {
        this.gameObject.SetActive(false);
    }
}