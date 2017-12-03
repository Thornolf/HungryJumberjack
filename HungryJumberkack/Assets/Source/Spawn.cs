using UnityEngine;
using System.Collections;

public class Spawn : MonoBehaviour
{

    public GameObject enemy;
    public float spawnTime = 3f;
    public Transform[] spawnPoints;

    void Start()
    {
        InvokeRepeating("Spawning", spawnTime, spawnTime);
    }

    void Spawning()
    {
        if (GameObject.FindGameObjectsWithTag("Enemy").Length < 2) ;
        {
            Debug.Log("Number of enemies : " + GameObject.FindGameObjectsWithTag("Enemy").Length);
            int spawnPointIndex = Random.Range(0, spawnPoints.Length);
            Instantiate(enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
        }
    }
}
