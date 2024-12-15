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

    private bool fireContinuously;
    // Feito para corrigir um bug que se você aperta duas vezes e segura na segunda,
    // Ele só dispara uma vez e para pois o segundo clique veio antes do delay acabar.
    private bool fireSingle;
    private float lastFireTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (fireContinuously || fireSingle)
        {
            // Calcula a quantidade de tempo que passou desde o último disparo
            float timeSinceLastFire = Time.time - lastFireTime;

            // Se o tempo do último disparo for maior ou igual ao delay, então pode disparar novamente
            if (timeSinceLastFire >= delayBetweenShoots)
            {
                FireBullet();
                // Pega o momento atual do último disparo
                lastFireTime = Time.time;
                fireSingle = false;
            }

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
    }

    // InputValue é o novo sistema de Input
    private void OnFire(InputValue inputValue)
    {
        fireContinuously = inputValue.isPressed;

        //Verifica se o botão foi apertado
        if (inputValue.isPressed)
        {
            fireSingle = true;
        }
    }
}
