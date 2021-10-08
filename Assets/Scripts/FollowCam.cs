using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    public Transform target = null;
    public Transform rig = null;
    public Transform rig2 = null;
    public Transform rig3 = null;

    public float distance = 10f;
    public float rotationSpeed = 10f;
    public float speed = 90f;

    Vector3 cameraPosition;
    Vector3 smoothPosition;
    float smoothTime = 0.125f;
    float angle;

    private int camType = 0;

    //Switch Camera Angles Using "C" Key On Keyboard
    void Update()
    {
        if (Input.GetKeyDown("c"))
        {
            camType += 1;
            if(camType >= 5)
            {
                camType = 0;
            }
        }
    }

    void FixedUpdate()
    {
        if (camType == 0) MainCam();
        else if (camType == 1) RigCam();
        else if (camType == 2) Rig2Cam();
        else if (camType == 3) Rig3Cam();
        else if (camType == 4) BackCam();
        else MainCam();
    }
    
    void MainCam()
    {
        Vector3 moveCamTo = transform.position - transform.forward * 10.0f + Vector3.up * 1.0f;
        float bias = 0.96f;
        Camera.main.transform.position = Camera.main.transform.position * bias +
                                         moveCamTo * (1.0f - bias);
        Camera.main.transform.LookAt(transform.position + transform.forward * 30.0f);


        speed -= transform.forward.y * Time.deltaTime * 10.0f;

        if (speed < 25.0f)
        {
            speed = 25.0f;
        }
    }

    void RigCam()
    {
        transform.position = rig.position;
        transform.rotation = rig.rotation;
    }

    void Rig2Cam()
    {
        transform.position = rig2.position;
        transform.rotation = rig2.rotation;
    }

    void Rig3Cam()
    {
        transform.position = rig3.position;
        transform.rotation = rig3.rotation;
    }

    void BackCam()
    {
        //Calculate Postion
        cameraPosition = target.position - (target.forward * distance) + target.up * distance * 0.5f;
        smoothPosition = Vector3.Lerp(transform.position, cameraPosition, smoothTime);
        transform.position = smoothPosition;

        //Calculate Rotation
        angle = Mathf.Abs(Quaternion.Angle(transform.rotation, target.rotation));
        transform.rotation = Quaternion.RotateTowards(transform.rotation, target.rotation, (rotationSpeed + angle) * Time.deltaTime);
    }

}