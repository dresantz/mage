using UnityEngine;

public class PicaxeSpawner : MonoBehaviour
{
    public GameObject picaxePrefab; // Prefab do Picaxe
    public Transform[] spawners; // Lista de Spawners
    public Vector2 spawnForce; // Força aplicada ao Picaxe ao ser instanciado
    public float disableDelay = 2f; // Tempo para desativar física

    public void SpawnPicaxeInAllSpawners()
    {
        foreach (Transform spawner in spawners)
        {
            // Instanciar o Picaxe em cada Spawner
            GameObject spawnedPicaxe = Instantiate(picaxePrefab, spawner.position, spawner.rotation);

            // Aplicar força inicial ao Picaxe
            Rigidbody2D picaxeRigidbody = spawnedPicaxe.GetComponent<Rigidbody2D>();
            if (picaxeRigidbody != null)
            {
                picaxeRigidbody.AddForce(spawnForce, ForceMode2D.Impulse);
            }

            // Desativar física após o delay
            StartCoroutine(DisablePhysicsAfterDelay(spawnedPicaxe, disableDelay));
        }
    }

    private System.Collections.IEnumerator DisablePhysicsAfterDelay(GameObject picaxe, float delay)
    {
        // Esperar pelo delay definido
        yield return new WaitForSeconds(delay);

        // Desativar Rigidbody2D
        Rigidbody2D picaxeRigidbody = picaxe.GetComponent<Rigidbody2D>();
        if (picaxeRigidbody != null)
        {
            picaxeRigidbody.velocity = Vector2.zero;
            picaxeRigidbody.angularVelocity = 0f;
            picaxeRigidbody.simulated = false;
        }

        // Desativar Collider
        PolygonCollider2D picaxeCollider = picaxe.GetComponent<PolygonCollider2D>();
        if (picaxeCollider != null)
        {
            picaxeCollider.enabled = false;
        }
    }
}
