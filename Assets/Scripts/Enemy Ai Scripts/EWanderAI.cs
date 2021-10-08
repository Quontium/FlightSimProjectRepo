using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EWanderAI : MonoBehaviour
{
    public float moveSpeed = 100f;
    public float rotSpeed = 150f;

    private bool isWandering = false;
    private bool isRotatingLeft = false;
    private bool isRotatingRight = false;
    private bool isWalking = false;

    public float torque = 100f;
    public float thrust = 90f;
    public float speed = 90.0f;
    public float rotationSpeed = 110.0f;
    private float glide = 1f;
    public float pitch = 8.0f;
    private Rigidbody rb;

    //Use this for initialization
    void Start()
    {
        glide = 1f;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Automatic AI Roll(to the right) & Automatic AI Pitch Down(STRAIGHT NO YAW)
        float roll = 16.0f;
        float pitch = 16.0f;
        //float rotation = 8.0f * rotationSpeed;
        float throttle = 100.0f;





        ///Wandering
        if (isWandering == false)
        {
            StartCoroutine(Wander());
        }
        if (isRotatingRight == true)
        {
            //transform.Rotate(transform.up * Time.deltaTime * rotSpeed);
            rb.AddRelativeTorque(Vector3.right * torque * pitch);
            rb.AddRelativeTorque(Vector3.back * torque * roll);
        }
        if (isRotatingLeft == true)
        {
            //transform.Rotate(transform.up * Time.deltaTime * -rotSpeed);
            rb.AddRelativeTorque(Vector3.right * torque * -pitch);
            rb.AddRelativeTorque(Vector3.back * torque * -roll);
        }
        if (isWalking == true)
        {
            //transform.position += transform.forward * moveSpeed * Time.deltaTime;
            rb.AddRelativeForce(Vector3.forward * moveSpeed * throttle);
            glide = throttle;
        }
        else
        {
            rb.AddRelativeForce(Vector3.forward * glide);
            glide *= 0.975f;
        }
    }


    IEnumerator Wander()
    {
        int rotTime = Random.Range(0, 3);
        int rotateWait = Random.Range(0, 0);
        int rotateLorR = Random.Range(0, 3);
        int walkWait = Random.Range(0, 0);
        int walkTime = Random.Range(0, 3);
        int throttle = Random.Range(1, 2);

        isWandering = true;

        yield return new WaitForSeconds(walkWait);
        isWalking = true;
        yield return new WaitForSeconds(walkTime);
        isWalking = false;
        yield return new WaitForSeconds(rotateWait);
        if (rotateLorR == 1)
        {
            isRotatingRight = true;
            yield return new WaitForSeconds(rotTime);
            isRotatingRight = false;
        }
        if (rotateLorR == 2)
        {
            isRotatingLeft = true;
            yield return new WaitForSeconds(rotTime);
            isRotatingLeft = false;
        }
        isWandering = false;
    }
}
