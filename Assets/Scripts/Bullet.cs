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
        if (collision.GetComponent<Enemy>())
        {
            HealthController healthController = collision.GetComponent<HealthController>();
            healthController.TakeDamage(10);
            Destroy(gameObject);
            TriggerDestructionEffect();
        }

        // Dano aos objetos que podem ser destruidos
        if (collision.gameObject.CompareTag("Destructable"))
        {
            // Tenta acessar o script DestructableObject no objeto colidido
            Destructable destructable = collision.gameObject.GetComponent<Destructable>();
            if (destructable != null)
            {
                destructable.TakeDamage(10);
            }
            Destroy(gameObject);
            TriggerDestructionEffect();
        }

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
