using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollectableDrop : MonoBehaviour
{

    // Lembrando que tudo isso será colocado no OnDied do Enemy
    [SerializeField]
    private float chanceOfCollectableDrop;

    private CollectableSpawner collectableSpawner;

    private void Awake()
    {
        collectableSpawner = FindAnyObjectByType<CollectableSpawner>();
    }

    public void RandomlyDropCollectable()
    {
        float random = Random.Range(0f, 1f);

        if (chanceOfCollectableDrop >= random)
        {
            collectableSpawner.SpawnCollectable(transform.position);
        }
    }
}
