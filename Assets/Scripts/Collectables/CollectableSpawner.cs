using System.Collections.Generic;
using UnityEngine;

public class CollectableSpawner : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> collectablePrefabs; // Lista de prefabs dos colet�veis

    [SerializeField]
    private GameObject particleEffectPrefab; // Prefab das part�culas

    public void SpawnCollectable(Vector2 position)
    {
        // Escolhe randomicamente um item da lista de colet�veis
        int index = Random.Range(0, collectablePrefabs.Count);
        var selectedCollectable = collectablePrefabs[index];

        // Instancia o colet�vel na posi��o especificada
        Instantiate(selectedCollectable, position, Quaternion.identity);

        // Instancia as part�culas na mesma posi��o do colet�vel
        if (particleEffectPrefab != null)
        {
            GameObject particles = Instantiate(particleEffectPrefab, position, Quaternion.identity);

            // Configura a dura��o das part�culas para destru�-las ap�s o tempo necess�rio
            ParticleSystem particleSystem = particles.GetComponent<ParticleSystem>();
            if (particleSystem != null)
            {
                Destroy(particles, particleSystem.main.duration);
            }
            else
            {
                Destroy(particles, 7.0f); // Tempo padr�o caso n�o seja um ParticleSystem
            }
        }
    }
}
