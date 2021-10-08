using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnControl : MonoBehaviour
{
    public Transform[] spawnerLocations;
    public GameObject[] prefabsToSpawn;
    private GameObject[] prefabsToClone;

    // Start is called before the first frame update
    void Start()
    {
        //initialize
        prefabsToClone = new GameObject[prefabsToSpawn.Length];
        // Spawn objects
        Spawn ();
    }

    // Spawn objects
    // Update is called once per frame
    void Spawn ()
    {
        // Instantiate the airplanes
        for (int i = 0; i < prefabsToSpawn.Length; i++)
        {
            prefabsToClone[i] = Instantiate(prefabsToSpawn[i], spawnerLocations[i].transform.position, Quaternion.Euler(0, 90, 0)) as GameObject;
        }
    }

    // Destroy all cloned game objects
    void DestroyClonedGameObjects ()
    {
        for (int i = 0; i < prefabsToClone.Length; i++)
        {
            Destroy(prefabsToClone[i]);
        }
    }

    public void Respawn()
    {
        // First destroy all already cloned game objects
        DestroyClonedGameObjects();

        // Spawn all game objects
        Spawn();
    }
}
