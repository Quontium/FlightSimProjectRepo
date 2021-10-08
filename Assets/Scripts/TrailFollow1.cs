using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailFollow1 : MonoBehaviour
{
    public GameObject trailRendererObject;

    Vector3 trailVectorPosition;

    bool trailActiveBool = false;


    void Start()
    {
        trailVectorPosition = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (trailActiveBool == true)
        {
            trailRendererObject.transform.position = trailVectorPosition;
        }
    }
}