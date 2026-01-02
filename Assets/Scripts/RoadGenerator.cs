using System.Collections.Generic;
using UnityEngine;

/*
 * This script handles infinite road generation by dynamically spawning new tiles 
 * in front of the player and destroying old ones behind to optimize performance.
 */
public class RoadGenerator : MonoBehaviour
{
    public GameObject roadPrefab;
    public Transform player;

    public float roadLength = 100f;
    public int numberOfTiles = 5;
    private float spawnZ = 0f;

    private List<GameObject> activeTiles = new List<GameObject>();

    void Start()
    {
        for (int i = 0; i < numberOfTiles; i++)
        {
            SpawnTile();
        }
    }

    void Update()
    {
        if (player.position.z - 70 > (spawnZ - numberOfTiles * roadLength))
        {
            SpawnTile();
            DeleteOldTile();
        }
    }

    void SpawnTile()
    {
        GameObject go = Instantiate(roadPrefab, new Vector3(0, 0, spawnZ), Quaternion.identity);
        activeTiles.Add(go);
        spawnZ += roadLength;
    }

    void DeleteOldTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }
}