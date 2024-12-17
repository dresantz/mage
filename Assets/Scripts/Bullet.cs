using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Camera _camera;

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
        }
    }

    private void DestroyWhenOffScreen()
    {
        Vector2 screenPosition = _camera.WorldToScreenPoint(transform.position);

        if (screenPosition.x < 0 ||
            screenPosition.x > _camera.pixelWidth ||
            screenPosition.y < 0 ||
            screenPosition.y > _camera.pixelHeight)
        {
            Destroy(gameObject);
        }
    }
}
