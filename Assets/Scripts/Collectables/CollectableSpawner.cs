using System.Collections.Generic;
using UnityEngine;

public class CollectableSpawner : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> collectablePrefabs; // Lista de prefabs dos coletáveis

    [SerializeField]
    private GameObject particleEffectPrefab; // Prefab das partículas

    public void SpawnCollectable(Vector2 position)
    {
        // Escolhe randomicamente um item da lista de coletáveis
        int index = Random.Range(0, collectablePrefabs.Count);
        var selectedCollectable = collectablePrefabs[index];

        // Instancia o coletável na posição especificada
        Instantiate(selectedCollectable, position, Quaternion.identity);

        // Instancia as partículas na mesma posição do coletável
        if (particleEffectPrefab != null)
        {
            GameObject particles = Instantiate(particleEffectPrefab, position, Quaternion.identity);

            // Configura a duração das partículas para destruí-las após o tempo necessário
            ParticleSystem particleSystem = particles.GetComponent<ParticleSystem>();
            if (particleSystem != null)
            {
                Destroy(particles, particleSystem.main.duration);
            }
            else
            {
                Destroy(particles, 7.0f); // Tempo padrão caso não seja um ParticleSystem
            }
        }
    }
}
