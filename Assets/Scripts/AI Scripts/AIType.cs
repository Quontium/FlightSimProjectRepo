using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace FlightSim.AI
{    
    public abstract class AIType : MonoBehaviour
    {
        [HideInInspector] public CreatePlane planeData;

        [HideInInspector] public Rigidbody rb;
        [HideInInspector] public Transform player;
        public AIType Target;
         public AIManager manager;
        public bool isNPC = true;
        bool resettedTarget = false;

        public float health = 100;
        [HideInInspector] public bool isDying = false;

        void Start()
        {
            if (!isNPC)
                return;
            rb = GetComponent<Rigidbody>();
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
        private void Update()
        {
            if(Target == null)
                Target = GameObject.FindGameObjectWithTag("Player").GetComponent<AIType>();
            if (health <= 0)
            {
                isDying = true;

            }
            resetTargetVoid();
        }
        public virtual void resetTargetVoid()
        {
            if (!resettedTarget)
            {
                StartCoroutine(resetTarget(3));
                resettedTarget = true;
            }
        }
        IEnumerator resetTarget(float sec)
        {
            manager.findNewTarget(this);
            print(this.gameObject.name + " wants to kill " + Target.gameObject.name);
            yield return new WaitForSeconds(sec);
            resettedTarget = false;
        }

        public void changeHealth(float change)
        {
            health += change;
        }

        void FixedUpdate()
        {
            if (!isNPC)
                return;
            Fly();
        }
        public void giveTarget(AIType targ)
        {
            Target = targ;
        }
        public virtual void Fly()
        {
            Vector3 targetVector = Vector3.zero;
            if (isDying)
            {
                float y = Terrain.activeTerrain.SampleHeight(transform.position);
                targetVector = new Vector3(transform.position.x, y, transform.position.z);
            }
            else
            {
                if (Target == null)
                    return;
                targetVector = Target.transform.position;
            }

            float xyAngle = vector3AngleOnPlane(targetVector, transform.position, transform.forward, transform.up);
            float yzAngle = vector3AngleOnPlane(targetVector, transform.position, transform.right, transform.forward);

            if (Mathf.Abs(xyAngle) >= 1f && Mathf.Abs(yzAngle) >= 1f)
            {
                rb.AddRelativeTorque(Vector3.forward * -planeData.torque * (xyAngle / Mathf.Abs(xyAngle)));
            }
            else if (yzAngle >= 1f)
            {
                rb.AddRelativeTorque(Vector3.right * -planeData.torque);

                //weapon.fire();
            }

            rb.AddRelativeForce(Vector3.forward * planeData.thrust);
        }

        float vector3AngleOnPlane(Vector3 from, Vector3 to, Vector3 planeNormal, Vector3 toOrientation)
        {
            Vector3 projectedVector = Vector3.ProjectOnPlane(from - to, planeNormal);
            float projectedVectorAngle = Vector3.SignedAngle(projectedVector, toOrientation, planeNormal);

            return projectedVectorAngle;
        }
    }

}
