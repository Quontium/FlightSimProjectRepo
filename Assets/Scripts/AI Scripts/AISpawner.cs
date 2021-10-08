using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace FlightSim.AI
{
    public class AISpawner : MonoBehaviour
    {
        public enum AITypeEnum
        {
            Ally,
            Enemy
        }
        private AIManager thisManager;
        public AITypeEnum aiType;
        [System.Serializable]
        public class AIData
        {
            public CreatePlane planeData;
            public int amount;
        }
        public List<AIData> allPlanesToSpawn = new List<AIData>();
        [Range(0, 100)]
        public float xRange, yRange, zRange;
        int totalAmount;
        private void Start()
        {
            spawnAtStart();
        }
        void spawnAtStart()
        {
            int count = 0;
            foreach (AIData ai in allPlanesToSpawn)
            {
                for (int i = 0; i < ai.amount; i++)
                {
                    float x = Random.Range(transform.position.x - xRange / 2, transform.position.x + xRange / 2);
                    float y = Random.Range(transform.position.y - yRange / 2, transform.position.y + yRange / 2);
                    float z = Random.Range(transform.position.x - zRange / 2, transform.position.x + zRange / 2);
                    Vector3 spawnPlace = new Vector3(x, y, z);
                    GameObject Instance = Instantiate(ai.planeData.prefab, spawnPlace, Quaternion.identity);
                    assignVarToPlanes(Instance, ai);
                    count++;
                    Instance.name = aiType.ToString() + " " + count.ToString();
                    foreach (GameObject t in GameObject.FindGameObjectsWithTag("AIManager"))
                    {
                        AIManager manager = t.GetComponent<AIManager>();
                        if(manager.typeOfPlanes != aiType)
                            manager.helpUnit.Add(Instance.GetComponent<AIType>());
                        
                    }
                }
            }
        }
        void assignVarToPlanes(GameObject ai, AIData data)
        {
            switch (aiType)
            {
                case AITypeEnum.Ally: ai.GetComponent<Ally>().enabled = true; ai.tag = "Ally"; ai.GetComponent<Ally>().planeData = data.planeData;
                    ai.GetComponent<Ally>().manager = GetComponent<AIManager>();
                    break;
                case AITypeEnum.Enemy: ai.GetComponent<Enemy>().enabled = true; ai.tag = "Enemy"; ai.GetComponent<Enemy>().planeData = data.planeData; ai.GetComponent<Enemy>().manager = GetComponent<AIManager>();
                    break;
            }
        }

    }

}
