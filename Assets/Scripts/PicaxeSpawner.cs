using UnityEngine;

public class PicaxeSpawner : MonoBehaviour
{
    public GameObject picaxePrefab; // Refer�ncia ao prefab do Picaxe
    public Transform picaxeSpawner; // Refer�ncia ao Picaxe_Spawner
    public Vector2 spawnForce; // For�a aplicada ao Picaxe ao ser instanciado
    public float disableDelay = 2f; // Tempo at� desativar Rigidbody2D e Collider

    public void SpawnPicaxe()
    {
        // Instanciar o prefab do Picaxe na posi��o do Picaxe_Spawner
        // A rota��o ser� a mesma do picaxeSpawner
        GameObject spawnedPicaxe = Instantiate(picaxePrefab, picaxeSpawner.position, picaxeSpawner.rotation);

        // Obter o Rigidbody2D do Picaxe
        Rigidbody2D picaxeRigidbody = spawnedPicaxe.GetComponent<Rigidbody2D>();
        if (picaxeRigidbody != null)
        {
            // Aplicar a for�a inicial ao Picaxe
            picaxeRigidbody.AddForce(spawnForce, ForceMode2D.Impulse);
        }

        // Iniciar a coroutine para desativar Rigidbody2D e Collider ap�s o delay
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

