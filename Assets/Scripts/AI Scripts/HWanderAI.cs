using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HWanderAI : MonoBehaviour
{
    [SerializeField] private Transform enemy3;

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
    public float reverse = 200f;
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
        //float rotation = 8.0f * rotationSpeed;
        float roll = 16.0f;
        float pitch = 16.0f;
        float throttle = 100.0f;

        ///Wandering
        if (isWandering == false)
        {
            StartCoroutine(Wander());
        }
        if (isRotatingRight == true)
        {
            //transform.Rotate(transform.up * Time.deltaTime * rotSpeed);
            //rb.AddRelativeTorque(Vector3.right * torque * pitch);
            rb.AddRelativeTorque(Vector3.back * torque * roll);
            rb.AddRelativeForce(Vector3.forward * moveSpeed * throttle);
        }
        if (isRotatingLeft == true)
        {
            //transform.Rotate(transform.up * Time.deltaTime * -rotSpeed);
            //rb.AddRelativeTorque(Vector3.back * torque * -roll);
            rb.AddRelativeTorque(Vector3.right * thrust * -pitch);
            rb.AddRelativeForce(Vector3.forward * moveSpeed * throttle);
        }
        if (isWalking == true)
        {
            //transform.position += transform.forward * moveSpeed * Time.deltaTime;
            rb.AddRelativeForce(Vector3.forward * moveSpeed * throttle);
            glide = throttle;
        }
        else if (isRotatingRight == true)
        {
            //rb.AddRelativeTorque(Vector3.right * torque * pitch);
            //rb.AddRelativeForce(Vector3.forward * moveSpeed * throttle);
            rb.AddRelativeTorque(Vector3.up * torque * roll);
        }
        else if (isRotatingLeft == true)
        {
            rb.AddRelativeForce(Vector3.forward * moveSpeed * throttle);
        }
        else
        {
            rb.AddRelativeForce(Vector3.forward * glide);
            glide *= 0.95f;
        }


        IEnumerator Wander()
        {
            int rotTime = Random.Range(1, 1);
            int rotateWait = Random.Range(0, 1);
            int rotateLorR = Random.Range(0, 3);
            int walkWait = Random.Range(0, 1);
            int walkTime = Random.Range(1, 2);
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

            if (transform.position.z <= 1000)
            {
                {
                    rb.AddRelativeForce(-Vector3.forward * reverse);
                    enemy3.transform.Rotate(180f, 0f, 180f);
                    //rb.MovePosition(transform.position * moveSpeed);
                }
            }
            if (transform.position.x <= 1000)
            {
                {
                    rb.AddRelativeForce(-Vector3.forward * reverse);
                    enemy3.transform.Rotate(180f, 0f, 180f);
                    //rb.MovePosition(transform.position * moveSpeed);
                }
            }
            if (transform.position.y <= -1000)
            {
                {
                    rb.AddRelativeForce(-Vector3.forward * reverse);
                    enemy3.transform.Rotate(180f, 0f, 180f);
                    //rb.MovePosition(transform.position * moveSpeed);
                }
            }
            if (transform.position.z <= -1000)
            {
                {
                    rb.AddRelativeForce(-Vector3.forward * reverse);
                    enemy3.transform.Rotate(180f, 0f, 180f);
                    //rb.MovePosition(transform.position * moveSpeed);
                }
            }
            if (transform.position.x <= -1000)
            {
                {
                    rb.AddRelativeForce(-Vector3.forward * reverse);
                    enemy3.transform.Rotate(180f, 0f, 180f);
                    //rb.MovePosition(transform.position * moveSpeed);
                }
            }
        }
    }
}
