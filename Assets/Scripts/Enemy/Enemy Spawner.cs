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

    public GameObject spawnParticles;

    void Update()
    {
        timeUntilSpawn -= Time.deltaTime;

        if (timeUntilSpawn <= 0)
        {
            // Identity significa nenhuma rota��o
            // tranform.position instancia o prefab na posi��o do Spawner
            GameObject.Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            SetTimeUntilSpawn();
            TriggerSpawnEffect();
        }
    }

    private void SetTimeUntilSpawn()
    {
        timeUntilSpawn = Random.Range(minimumSpawnTime, maximumSpawnTime);
    }
    
    private void TriggerSpawnEffect()
    {
        if (spawnParticles != null)
        {
            GameObject effectInstance = Instantiate(spawnParticles, transform.position, transform.rotation);
            Destroy(effectInstance, 1f);
        }
    }
}
