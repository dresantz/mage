using UnityEngine;

public class PicaxeSpawner : MonoBehaviour
{
    public GameObject picaxePrefab; // Referência ao prefab do Picaxe
    public Transform picaxeSpawner; // Referência ao Picaxe_Spawner
    public Vector2 spawnForce; // Força aplicada ao Picaxe ao ser instanciado
    public float disableDelay = 2f; // Tempo até desativar Rigidbody2D e Collider

    public void SpawnPicaxe()
    {
        // Instanciar o prefab do Picaxe na posição do Picaxe_Spawner
        // A rotação será a mesma do picaxeSpawner
        GameObject spawnedPicaxe = Instantiate(picaxePrefab, picaxeSpawner.position, picaxeSpawner.rotation);

        // Obter o Rigidbody2D do Picaxe
        Rigidbody2D picaxeRigidbody = spawnedPicaxe.GetComponent<Rigidbody2D>();
        if (picaxeRigidbody != null)
        {
            // Aplicar a força inicial ao Picaxe
            picaxeRigidbody.AddForce(spawnForce, ForceMode2D.Impulse);
        }

        // Iniciar a coroutine para desativar Rigidbody2D e Collider após o delay
        StartCoroutine(DisablePhysicsAfterDelay(spawnedPicaxe, disableDelay));
    }

    private System.Collections.IEnumerator DisablePhysicsAfterDelay(GameObject picaxe, float delay)
    {
        // Esperar pelo tempo definido no delay
        yield return new WaitForSeconds(delay);

        // Desativar Rigidbody2D e PolygonCollider2D
        Rigidbody2D picaxeRigidbody = picaxe.GetComponent<Rigidbody2D>();
        if (picaxeRigidbody != null)
        {
            picaxeRigidbody.velocity = Vector2.zero;
            picaxeRigidbody.angularVelocity = 0f;
            picaxeRigidbody.simulated = false; // Desativa o Rigidbody2D
        }

        PolygonCollider2D picaxeCollider = picaxe.GetComponent<PolygonCollider2D>();
        if (picaxeCollider != null)
        {
            picaxeCollider.enabled = false; // Desativa o Collider
        }
    }
}

