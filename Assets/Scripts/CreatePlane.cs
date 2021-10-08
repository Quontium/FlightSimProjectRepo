using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Create Plane", menuName = "Planes/Create Plane")]
public class CreatePlane : ScriptableObject
{
    public GameObject prefab;
    public string name;
    public float torque = 5f;
    public float thrust = 90000f;
    public float speed = 90.0f;
    public float rotationSpeed = 110.0f;
    public float glide = 1f;
    public float pitch = 16.0f;
    public float throttle = 100.0f;
}
