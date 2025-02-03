using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerPos : MonoBehaviour
{
    public GameObject spawnerPrefab; // Prefab do Spawner
    public int spawnersToInstantiate; // Número de Spawners a instanciar
    public GameObject triggerObject; // Objeto que contém o Collider2D que ativa os Spawners

    private List<Transform> spawnerPositions; // Lista de posições disponíveis
    private bool hasTriggered = false; // Flag para verificar se o gatilho já foi acionado

    void Awake()
    {
        // Encontra todos os objetos com a tag "SpawnerPos" e armazena suas transformações
        spawnerPositions = new List<Transform>();

        GameObject[] spawnerPosObjects = GameObject.FindGameObjectsWithTag("ESpawnerPos");

        foreach (GameObject obj in spawnerPosObjects)
        {
            spawnerPositions.Add(obj.transform);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica se o objeto que entrou no gatilho é o player
        if (other.CompareTag("Player") && !hasTriggered)
        {

            Debug.Log("Teste");
            // Instancia os Spawners em posições aleatórias
            InstantiateSpawners();

            // Desativa o gatilho para que ele não seja acionado novamente
            triggerObject.SetActive(false);

            // Marca que o gatilho foi acionado
            hasTriggered = true;
        }
    }

    void InstantiateSpawners()
    {
        for (int i = 0; i < spawnersToInstantiate; i++)
        {
            if (spawnerPositions.Count == 0)
            {
                Debug.LogError("Não há posições suficientes para todos os Spawners.");
                return;
            }

            // Escolhe uma posição aleatória da lista
            int randomIndex = Random.Range(0, spawnerPositions.Count);
            Transform chosenPosition = spawnerPositions[randomIndex];

            // Instancia o Spawner no local escolhido
            Instantiate(spawnerPrefab, chosenPosition.position, chosenPosition.rotation);

            // Remove a posição da lista para evitar repetições
            spawnerPositions.RemoveAt(randomIndex);
        }
    }
}