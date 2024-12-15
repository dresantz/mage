using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableSpawner : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> collectablePrefabs;

    public void SpawnCollectable(Vector2 position)
    {
        // Escolhe randomicamente um item da lista de coletáveis
        int index = Random.Range(0, collectablePrefabs.Count);
        var selectedCollectable = collectablePrefabs[index];

        // Posiciona o item escolhido, lembrando que identity determina a rotação
        Instantiate(selectedCollectable, position, Quaternion.identity);
    }
}
