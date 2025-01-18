using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Camera _camera;

    public GameObject bulletParticles;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        DestroyWhenOffScreen();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Dano ao inimigo
        Enemy enemy = collision.GetComponent<Enemy>();
        if (enemy != null)
        {
            HealthController healthController = collision.GetComponent<HealthController>();
            if (healthController != null)
            {
                healthController.TakeDamage(10);
            }
            Destroy(gameObject);
            TriggerDestructionEffect();
            return;
        }

        // Dano aos objetos que podem ser destru�dos
        if (collision.gameObject.CompareTag("Destructable"))
        {
            // Tenta aplicar dano ao objeto
            Destructable destructable = collision.GetComponent<Destructable>();
            if (destructable != null)
            {
                destructable.TakeDamage(10);
            }

            // Tenta aplicar for�a no objeto destrut�vel
            Rigidbody2D targetRigidbody = collision.GetComponent<Rigidbody2D>();
            if (targetRigidbody != null)
            {
                // Calcula a dire��o da for�a com base na posi��o da bala e do objeto
                Vector2 impactPoint = transform.position;
                Vector2 forceDirection = (targetRigidbody.position - impactPoint).normalized;

                // Aplica a for�a no ponto de impacto
                float forceMagnitude = 1000f; // Ajuste a intensidade da for�a conforme necess�rio
                targetRigidbody.AddForceAtPosition(forceDirection * forceMagnitude, impactPoint, ForceMode2D.Impulse);
            }

            Destroy(gameObject);
            TriggerDestructionEffect();
            return;
        }

        // Colis�o com obst�culos
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
            TriggerDestructionEffect();
        }
    }



    private void DestroyWhenOffScreen()
    {
        Vector2 screenPosition = _camera.WorldToScreenPoint(transform.position);

        if (screenPosition.x < -300 ||
            screenPosition.x > _camera.pixelWidth + 300 ||
            screenPosition.y < -300 ||
            screenPosition.y > _camera.pixelHeight + 300)
        {
            Destroy(gameObject);
        }
    }

    private void TriggerDestructionEffect()
    {
        if (bulletParticles != null)
        {
            // Instancia o efeito de part�culas na posi��o e rota��o do objeto
            GameObject effectInstance = Instantiate(bulletParticles, transform.position, transform.rotation);

            // Destroi o clone do efeito de part�culas ap�s 10 segundos
            Destroy(effectInstance, 1f);
        }
    }
}
