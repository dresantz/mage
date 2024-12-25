using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private Transform wandOffset;
    [SerializeField]
    private float delayBetweenShoots;
    [SerializeField]
    private float bulletSpeed;

    private bool canShoot = true; // Controla se o jogador pode disparar
    private float lastFireTime;

    void Update()
    {
        if (PauseMenu.IsPaused)
        {
            return;
        }
    }

    private void FireBullet()
    {
        // Cria um projétil na posição do Offset
        GameObject bullet = Instantiate(bulletPrefab, wandOffset.position, transform.rotation);
        // Faz o projétil andar
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        // A velocidade do projétil multiplicada pela direção que o personagem está apontando(up)
        rb.velocity = bulletSpeed * transform.up;

        // Atualiza o tempo do último disparo
        lastFireTime = Time.time;
        canShoot = false; // Impede novos disparos até que o delay seja respeitado
        StartCoroutine(Reload());
    }

    private IEnumerator Reload()
    {
        // Aguarda o delay antes de permitir outro disparo
        yield return new WaitForSeconds(delayBetweenShoots);
        canShoot = true;
    }

    private void OnFire(InputValue inputValue)
    {
        if (PauseMenu.IsPaused || !canShoot)
        {
            return;
        }

        // Verifica se o botão foi pressionado
        if (inputValue.isPressed)
        {
            FireBullet();
        }
    }
}
