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

        // Dano aos objetos que podem ser destruídos
        if (collision.gameObject.CompareTag("Destructable"))
        {
            // Tenta aplicar dano ao objeto
            Destructable destructable = collision.GetComponent<Destructable>();
            if (destructable != null)
            {
                destructable.TakeDamage(10);
            }

            // Tenta aplicar força no objeto destrutível
            Rigidbody2D targetRigidbody = collision.GetComponent<Rigidbody2D>();
            if (targetRigidbody != null)
            {
                // Calcula a direção da força com base na posição da bala e do objeto
                Vector2 impactPoint = transform.position;
                Vector2 forceDirection = (targetRigidbody.position - impactPoint).normalized;

                // Aplica a força no ponto de impacto
                float forceMagnitude = 1000f; // Ajuste a intensidade da força conforme necessário
                targetRigidbody.AddForceAtPosition(forceDirection * forceMagnitude, impactPoint, ForceMode2D.Impulse);
            }

            Destroy(gameObject);
            TriggerDestructionEffect();
            return;
        }

        // Colisão com obstáculos
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
            // Instancia o efeito de partículas na posição e rotação do objeto
            GameObject effectInstance = Instantiate(bulletParticles, transform.position, transform.rotation);

            // Destroi o clone do efeito de partículas após 10 segundos
            Destroy(effectInstance, 1f);
        }
    }
}
