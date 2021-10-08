using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace FlightSim.AI
{
    public class AIManager : MonoBehaviour
    {
        public AISpawner.AITypeEnum typeOfPlanes;
        public int K = 1;

        [HideInInspector] public float Radius = 0.1f;

        public bool DrawQueryNodes = true;

        [HideInInspector] public Vector3 IntervalSize = new Vector3(0.2f, 0.2f, 0.2f);

        public Vector3[] pointCloud = new Vector3[0];
        int currentHelpIndex = 0;
        public KdTree<AIType> helpUnit = new KdTree<AIType>();
        public KdTree<AIType> allNPCs = new KdTree<AIType>();
        private void Start()
        {
            if(typeOfPlanes == AISpawner.AITypeEnum.Enemy)
            {
                helpUnit.Add(GameObject.FindGameObjectWithTag("Player").GetComponent<AIType>());
            }
        }
        private void Update()
        {
            updateKdtreePos();
        }
        void updateKdtreePos()
        {
            helpUnit.UpdatePositions();
        }

        void kdtreeUpdate()
        {
            helpUnit.UpdatePositions();
            for (int i = 0; i < helpUnit.Count; i++)
            {
                AIType Targ = helpUnit.FindClosest(helpUnit[i].transform.position);
                helpUnit[i].giveTarget(Targ);
            }
        }
        public void findNewTarget(AIType unit)
        {
            AIType Targ = helpUnit.FindClosest(unit.transform.position);
            unit.giveTarget(Targ);
        }
    }
}
