using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableSpawner : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> collectablePrefabs;

    public void SpawnCollectable(Vector2 position)
    {
        // Escolhe randomicamente um item da lista de colet�veis
        int index = Random.Range(0, collectablePrefabs.Count);
        var selectedCollectable = collectablePrefabs[index];

        // Posiciona o item escolhido, lembrando que identity determina a rota��o
        Instantiate(selectedCollectable, position, Quaternion.identity);
    }
}
