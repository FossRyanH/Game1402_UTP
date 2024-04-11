using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailSpawner : MonoBehaviour
{
    [SerializeField]
    Transform spawnLocation;
    [SerializeField]
    ObstacleBall obstacleBall;
    [SerializeField]
    float spawnTime = 1;

    private void Start()
    {
        StartCoroutine(SpawnBall());
    }

    IEnumerator SpawnBall()
    {
        while (true)
        {
            //instantiate ball based on spawnLocation position and rotation
            Instantiate(obstacleBall, spawnLocation.position, Quaternion.Euler(0, 0, 0));
            yield return new WaitForSeconds(spawnTime);
        }
    }
}