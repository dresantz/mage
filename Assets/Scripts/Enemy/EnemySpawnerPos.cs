using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerPos : MonoBehaviour
{
    public GameObject spawnerPrefab; // Prefab do Spawner
    public int spawnersToInstantiate; // N�mero de Spawners a instanciar

    private List<Transform> spawnerPositions; // Lista de posi��es dispon�veis

    // Se eu n�o colocar no Awake o EndGame n�o vai fazer a lista dos spawners
    void Awake()
    {
        // Encontra todos os objetos com a tag "SpawnerPos" e armazena suas transforma��es
        spawnerPositions = new List<Transform>();

        GameObject[] spawnerPosObjects = GameObject.FindGameObjectsWithTag("ESpawnerPos");

        foreach (GameObject obj in spawnerPosObjects)
        {
            spawnerPositions.Add(obj.transform);
        }

        // Instancia os Spawners em posi��es aleat�rias
        InstantiateSpawners();
    }


    void InstantiateSpawners()
    {
        for (int i = 0; i < spawnersToInstantiate; i++)
        {
            if (spawnerPositions.Count == 0)
            {
                Debug.LogError("N�o h� posi��es suficientes para todos os Spawners.");
                return;
            }

            // Escolhe uma posi��o aleat�ria da lista
            int randomIndex = Random.Range(0, spawnerPositions.Count);
            Transform chosenPosition = spawnerPositions[randomIndex];

            // Instancia o Spawner no local escolhido
            Instantiate(spawnerPrefab, chosenPosition.position, chosenPosition.rotation);

            // Remove a posi��o da lista para evitar repeti��es
            spawnerPositions.RemoveAt(randomIndex);
        }
    }
}

