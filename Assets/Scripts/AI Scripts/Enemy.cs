using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace FlightSim.AI
{
    public class Enemy : AIType
    {
        [HideInInspector] public float moveSpeed = 100f;
        [HideInInspector] public float rotSpeed = 150f;

        private bool isWandering = false;
        private bool isRotatingLeft = false;
        private bool isRotatingRight = false;
        private bool isWalking = false;

        [HideInInspector] public float torque = 100f;
        [HideInInspector] public float thrust = 90f;
        [HideInInspector] public float speed = 90.0f;
        [HideInInspector] public float rotationSpeed = 110.0f;
        private float glide = 1f;
        [HideInInspector] public float pitch = 16.0f;
        float throttle = 100.0f;
        //Automatic AI Roll(to the right) & Automatic AI Pitch Down(STRAIGHT NO YAW)
        float roll = 16.0f;

        void Start()
        {
            rb = GetComponent<Rigidbody>();
            torque = planeData.torque;
            thrust = planeData.thrust;
            speed = planeData.speed;
            rotationSpeed = planeData.rotationSpeed;
            glide = planeData.glide;
            pitch = planeData.pitch;
            throttle = planeData.throttle;
        }

        public override void Fly()
        {
            glide = 1f;
            pitch = 16.0f;
            roll = 16.0f;
            if (isWandering == false)
            {
                StartCoroutine(Wander());
            }
            if (isRotatingRight == true)
            {
                rb.AddRelativeTorque(Vector3.right * torque * pitch);
                rb.AddRelativeTorque(Vector3.back * torque * roll);
            }
            if (isRotatingLeft == true)
            {
                rb.AddRelativeTorque(Vector3.right * torque * -pitch);
                rb.AddRelativeTorque(Vector3.back * torque * -roll);
            }
            if (isWalking == true)
            {
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

}
