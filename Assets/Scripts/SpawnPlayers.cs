using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviour
{
    public GameObject playerPrefab;

    public float minX;
    public float maxX;
    public float minY;  // This will be ignored for player spawning
    public float maxY;  // This will be ignored for player spawning

    // Ground level (Y-coordinate)
    public float groundY = 0f; // Change this value to match the height of your ground plane

    // Start is called before the first frame update
    void Start()
    {
        Vector2 randomPosition = new Vector2(Random.Range(minX, maxX), groundY); // Set Y to ground level
        PhotonNetwork.Instantiate(playerPrefab.name, new Vector3(randomPosition.x, groundY, randomPosition.y), Quaternion.identity);
    }
}