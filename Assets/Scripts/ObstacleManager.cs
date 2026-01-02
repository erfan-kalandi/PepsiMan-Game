using System.Collections.Generic;
using UnityEngine;

/*
 * This script manages the procedural generation and recycling of obstacles. 
 * It spawns obstacles in random lanes with variable gaps and handles their 
 * destruction after the player passes them to maintain performance.
 */
public class ObstacleManager : MonoBehaviour
{
    public GameObject obstaclePrefab;
    public Transform player;

    [Header("Spawn Settings")]
    public float laneDistance = 9f;
    public int numberOfObstacles = 10;
    public float startDelay = 20f;

    private float spawnZ = 0f;
    private List<GameObject> activeObstacles = new List<GameObject>();

    void Start()
    {
        spawnZ = startDelay;
        for (int i = 0; i < numberOfObstacles; i++)
        {
            SpawnObstacle();
        }
    }

    void Update()
    {
        if (activeObstacles.Count > 0 && player.position.z - 10 > activeObstacles[0].transform.position.z)
        {
            DeleteOldObstacle();
            SpawnObstacle();
        }
    }

    void SpawnObstacle()
    {
        float[] lanes = { -laneDistance, 0, laneDistance };
        int randomLane = Random.Range(0, lanes.Length);

        float randomGap = Random.Range(3f, 7f);
        spawnZ += randomGap;

        Vector3 spawnPosition = new Vector3(lanes[randomLane], 0.5f, spawnZ);
        GameObject go = Instantiate(obstaclePrefab, spawnPosition, Quaternion.identity);

        activeObstacles.Add(go);
    }

    void DeleteOldObstacle()
    {
        Destroy(activeObstacles[0]);
        activeObstacles.RemoveAt(0);
    }
}