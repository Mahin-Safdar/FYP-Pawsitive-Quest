using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClueManager : MonoBehaviour
{
    public GameObject apple1Prefab;
    public GameObject apple2Prefab;
    public GameObject apple3Prefab;
    public GameObject apple4Prefab;
    public GameObject apple5Prefab;
    public GameObject keyPrefab;               
    public GameObject[] apple1SpawnPoints;
    public GameObject[] apple2SpawnPoints;
    public GameObject[] apple3SpawnPoints;
    public GameObject[] apple4SpawnPoints;
    public GameObject[] apple5SpawnPoints;
    public GameObject[] keySpawnPoints;       

    void Start()
    {
        SpawnObjects();
    }

    void SpawnObjects()
    {
        // apple1
        if (apple1SpawnPoints.Length > 0)
        {
            int apple1Index = Random.Range(0, apple1SpawnPoints.Length);
            Instantiate(apple1Prefab, apple1SpawnPoints[apple1Index].transform.position, Quaternion.identity);
        }
        else
        {
            Debug.LogError("No apple1 spawn points available!");
        }
        //apple2
        if (apple2SpawnPoints.Length > 0)
        {
            int apple2Index = Random.Range(0, apple2SpawnPoints.Length);
            Instantiate(apple2Prefab, apple2SpawnPoints[apple2Index].transform.position, Quaternion.identity);
        }
        else
        {
            Debug.LogError("No apple2 spawn points available!");
        }

        // apple3
        if (apple3SpawnPoints.Length > 0)
        {
            int apple3Index = Random.Range(0, apple3SpawnPoints.Length);
            Instantiate(apple3Prefab, apple3SpawnPoints[apple3Index].transform.position, Quaternion.identity);
        }
        else
        {
            Debug.LogError("No apple1 spawn points available!");
        }

        // apple4
        if (apple4SpawnPoints.Length > 0)
        {
            int apple4Index = Random.Range(0, apple4SpawnPoints.Length);
            Instantiate(apple4Prefab, apple4SpawnPoints[apple4Index].transform.position, Quaternion.identity);
        }
        else
        {
            Debug.LogError("No apple1 spawn points available!");
        }

        // apple5
        if (apple5SpawnPoints.Length > 0)
        {
            int apple5Index = Random.Range(0, apple5SpawnPoints.Length);
            Instantiate(apple5Prefab, apple5SpawnPoints[apple5Index].transform.position, Quaternion.identity);
        }
        else
        {
            Debug.LogError("No apple5 spawn points available!");
        }


        // Check if there are spawn points for the key
        if (keySpawnPoints.Length > 0)
        {
            int keyIndex = Random.Range(0, keySpawnPoints.Length);
            Instantiate(keyPrefab, keySpawnPoints[keyIndex].transform.position, Quaternion.identity);
        }
        else
        {
            Debug.LogError("No key spawn points available!");
        }
    }
}
