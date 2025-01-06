using System.Collections.Generic;
using UnityEngine;

public class CollectableSpawner : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> collectablePrefabs; // Lista de prefabs dos colet�veis

    // Sistema de cria��o de particulas foi comentado por n�o sei se vou usar
    /*[SerializeField]
    private GameObject particleEffectPrefab;*/

    public void SpawnCollectable(Vector2 position)
    {
        // Escolhe randomicamente um item da lista de colet�veis
        int index = Random.Range(0, collectablePrefabs.Count);
        var selectedCollectable = collectablePrefabs[index];

        // Instancia o colet�vel na posi��o especificada
        Instantiate(selectedCollectable, position, Quaternion.identity);


        /*if (particleEffectPrefab != null)
        {
            GameObject particles = Instantiate(particleEffectPrefab, position, Quaternion.identity);

            ParticleSystem particleSystem = particles.GetComponent<ParticleSystem>();
            if (particleSystem != null)
            {
                Destroy(particles, particleSystem.main.duration);
            }
            else
            {
                Destroy(particles, 7.0f);
            }
        }*/
    }
}
