using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField]
    private GameObject enemyPrefab;

    [SerializeField]
    private float minimumSpawnTime;

    [SerializeField]
    private float maximumSpawnTime;

    private float timeUntilSpawn;


    void Start()
    {
        
    }


    void Update()
    {
        timeUntilSpawn -= Time.deltaTime;

        if (timeUntilSpawn <= 0)
        {
            // Identity significa nenhuma rotação
            // tranform.position instancia o prefab na posição do Spawner
            GameObject.Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            SetTimeUntilSpawn();
        }
    }

    private void SetTimeUntilSpawn()
    {
        timeUntilSpawn = Random.Range(minimumSpawnTime, maximumSpawnTime);
    }
}
